import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IPagination } from '../shared/models/pagination';
import { IProduct } from '../shared/models/product';

@Injectable({
  providedIn: 'root'
})
export class ShopService {
  private baseUrl = 'https://localhost:5001/api/';

  constructor(private http: HttpClient) {

  }

  getProducts(){
    return this.http.get<IPagination>(this.baseUrl+'products');
  }
}
