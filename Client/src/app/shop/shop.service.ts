import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Pagination } from '../shared/models/pagination';
import { Type } from '../shared/models/type';
import { Brand } from '../shared/models/brand';
import { ShopParams } from '../shared/models/shopParams';

@Injectable({
  providedIn: 'root'
})
export class ShopService {
  private baseUrl = 'https://localhost:5001/api/';

  constructor(private http: HttpClient) {

  }

  getProducts(shopParams: ShopParams){
    let params = new HttpParams;

    if(shopParams.brandId > 0){
      params = params.append('brandId', shopParams.brandId);
    }

    if(shopParams.typeId > 0){
      params = params.append('typeId', shopParams.typeId);
    }

    params = params.append('sort', shopParams.sort);
    params = params.append('pageIndex', shopParams.pageNumber);
    params = params.append('pageSize', shopParams.pageSize);

    if(shopParams.search) params = params.append('search', shopParams.search);

    return this.http.get<Pagination>(this.baseUrl+'products', { params });
  }

  getTypes(){
    return this.http.get<Type[]>(this.baseUrl+'producttypes');
  }

  getBrands(){
    return this.http.get<Brand[]>(this.baseUrl+'productbrands');
  }
}
