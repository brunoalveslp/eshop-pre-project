import { Component, OnInit } from '@angular/core';
import { IProduct } from '../shared/models/product';
import { ShopService } from './shop.service';
import { IPagination } from '../shared/models/pagination';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.scss']
})
export class ShopComponent implements OnInit {
  public products: IProduct[];

  constructor(private shopService: ShopService){}

  ngOnInit(): void {
    this.shopService.getProducts().subscribe(res => {
      this.products = res.data;
      console.log(this.products)
    }, error => {
      console.assert(error);
    });
  }
}
