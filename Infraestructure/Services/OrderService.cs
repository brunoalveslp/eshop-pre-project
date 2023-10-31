using Core.Entities;
using Core.Entities.OrderAggregate;
using Core.Interfaces;
using Core.Specifications;

namespace Infraestructure.Services;

public class OrderService : IOrderService
{
    private readonly IBasketRepository _basketRepo;
    private readonly IUnitOfWork _unitOfWork;

    public OrderService(IBasketRepository basketRepo, IUnitOfWork unitOfWork)
    {
        _basketRepo = basketRepo;
        _unitOfWork = unitOfWork;
    }

        
    public async Task<Order> CreateOrderAsync(string buyerEmail, 
        int deliveryMethodId, string basketId, Address ShippingAddress)
    {
        // get basket from repo
        var basket = await _basketRepo.GetBasketAsync(basketId);
        // get items from products repo
        var items = new List<OrderItem>();
        foreach(var item in basket.Items)
        {
            var productItem = await _unitOfWork.Repository<Product>().GetByIdAsync(item.Id);
            var itemOrdered = new ProductItemOrdered(productItem.Id, 
                productItem.Name, productItem.PictureUrl);
            var orderItem = new OrderItem(itemOrdered, productItem.Price, item.Quantity);
            items.Add(orderItem);
        }

        // get delivery method
        var dm = await _unitOfWork.Repository<DeliveryMethod>().GetByIdAsync(deliveryMethodId);
        // calculate subtotal
        var subtotal = items.Sum(item => item.Price * item.Quantity);
        // create order

        var order = new Order(items,buyerEmail,ShippingAddress,dm,subtotal);
        _unitOfWork.Repository<Order>().Add(order);

        // TODO: save to db
        var result = await _unitOfWork.Complete();
        // return order

        if (result <= 0) return null;

        // delete basket
        await _basketRepo.DeleteBasketAsync(basketId);

        return order;
    }

    public async Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync()
    {
        return await _unitOfWork.Repository<DeliveryMethod>().GetAllAsync();
    }

    public async Task<Order> GetOrderByIdAsync(int id, string buyerEmail)
    {
        var spec = new OrdersWithItemsAndOrderingSpecification(id, buyerEmail);

        return await _unitOfWork.Repository<Order>().GetEntityWithSpecification(spec);
    }

    public async Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail)
    {
        var spec = new OrdersWithItemsAndOrderingSpecification(buyerEmail);

        return await _unitOfWork.Repository<Order>().ListAsync(spec);
    }
}
