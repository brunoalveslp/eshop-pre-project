import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { environment } from 'src/environments/environment.development';
import { Basket, BasketItem, BasketTotals } from '../shared/models/basket';
import { HttpClient } from '@angular/common/http';
import { Product } from '../shared/models/product';

@Injectable({
  providedIn: 'root'
})

export class BasketService {
  baseUrl = environment.apiUrl;
  private basketSource = new BehaviorSubject<Basket | null>(null);
  private basketTotalSource = new BehaviorSubject<BasketTotals | null>(null);

  basketSource$ = this.basketSource.asObservable();
  basketTotalSource$ = this.basketTotalSource.asObservable();

//continue tomorrow

  constructor(private http: HttpClient  ) { }

  getBasket(id: string){
    return this.http.get<Basket>(this.baseUrl+'basket?id='+id).subscribe({
      next: basket => {
        this.basketSource.next(basket);
        this.calculateTotals();
      }
    });
  }

  setBasket(basket: Basket){
    return this.http.post<Basket>(this.baseUrl+'basket', basket).subscribe({
      next: basket => {
        this.basketSource.next(basket);
        this.calculateTotals();
      }
    });
  }

  getCurrentBasketValue(){
    return this.basketSource.value;
  }

  addItemToBasket(item: Product | BasketItem, quantity = 1){
    // check it its a Product or a basket item
    if(this.isProduct(item)) item = this.mapProductToBasketItem(item);

    // gets the current basket or create one
    const basket = this.getCurrentBasketValue() ?? this.createBasket();

    // add an item or insert one
    basket.items = this.addOrUpdateItem(basket.items, item, quantity);
    // persists it
    this.setBasket(basket);
  }

  removeItemFromBasket(id: number, quantity = 1){
    const basket = this.getCurrentBasketValue();

    if(!basket) return;

    const item = basket.items.find(p => p.id === id);

    if(item){
      item.quantity -= quantity;
      if(item.quantity === 0){
        basket.items = basket.items.filter(p => p.id !== id);
      }

      if(basket.items.length > 0){
        this.setBasket(basket);
      } else {
        this.deleteBasket(basket);
      }
    }

  }

  deleteBasket(basket: Basket) {
    return this.http.delete(this.baseUrl+'basket?id='+basket.id).subscribe({
      next: () => {
        this.basketSource.next(null);
        this.basketTotalSource.next(null);
        localStorage.removeItem('basket_id');
      }
    });
  }

  private addOrUpdateItem(items: BasketItem[], itemToAdd: BasketItem, quantity: number): BasketItem[] {
    const item = items?.find(x => x.id === itemToAdd.id);
    if(item){
      item.quantity += quantity;
    } else {
      itemToAdd.quantity = quantity;
      items?.push(itemToAdd);
    }

    return items;
  }

  private createBasket(): Basket {
    const basket = new Basket();
    localStorage.setItem('basket_id', basket.id);
    return basket;
  }

  private mapProductToBasketItem(item: Product) : BasketItem {
    return {
      id: item.id,
      productName: item.name,
      price: item.price,
      quantity: 0,
      pictureUrl: item.pictureUrl,
      type: item.productType,
      brand: item.productBrand
    }
  }

  private calculateTotals(){
      const basket = this.getCurrentBasketValue();
      if(!basket) return;
      const shipping = 0;
      const subtotal = basket.items.reduce((prev, current) => (current.price * current.quantity) + prev, 0);
      const total = shipping + subtotal;

      this.basketTotalSource.next({shipping, subtotal, total});
   }

   private isProduct(item: Product | BasketItem): item is Product {
      return (item as Product).productBrand !== undefined;
   }
}
