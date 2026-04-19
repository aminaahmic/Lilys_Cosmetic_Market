import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import {
  ListProductsRequest,
  ListProductsResponse,
  GetProductByIdQueryDto,
  CreateProductCommand,
  UpdateProductCommand,
  AdjustProductStockCommand,
  ListProductStockMovementsResponse,
  ListProductStockMovementsRequest,
  ProductFilterOptionsDto
} from './products-api.models';
import { buildHttpParams } from '../../core/models/build-http-params';

@Injectable({
  providedIn: 'root'
})
export class ProductsApiService {
  private readonly baseUrl = `${environment.apiUrl}/api/Products`;
  private http = inject(HttpClient);

  list(request?: ListProductsRequest): Observable<ListProductsResponse> {
    const params = request ? buildHttpParams(request as any) : undefined;

    return this.http.get<ListProductsResponse>(this.baseUrl, {
      params,
    });
  }

  getById(id: number): Observable<GetProductByIdQueryDto> {
    return this.http.get<GetProductByIdQueryDto>(`${this.baseUrl}/${id}`);
  }

  create(payload: CreateProductCommand): Observable<number> {
    return this.http.post<number>(this.baseUrl, payload);
  }

  update(id: number, payload: UpdateProductCommand): Observable<void> {
    return this.http.put<void>(`${this.baseUrl}/${id}`, payload);
  }

  delete(id: number): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`);
  }

  adjustStock(id: number, payload: AdjustProductStockCommand): Observable<void> {
    return this.http.put<void>(`${this.baseUrl}/${id}/stock/adjust`, payload);
  }

  getStockMovements(
    id: number,
    request?: ListProductStockMovementsRequest
  ): Observable<ListProductStockMovementsResponse> {
    const params = request ? buildHttpParams(request as any) : undefined;
    return this.http.get<ListProductStockMovementsResponse>(`${this.baseUrl}/${id}/stock-movements`, {
      params,
    });
  }

  getFilterOptions(categoryId?: number | null): Observable<ProductFilterOptionsDto> {
    const params = categoryId ? buildHttpParams({ categoryId }) : undefined;
    return this.http.get<ProductFilterOptionsDto>(`${this.baseUrl}/filter-options`, { params });
  }
}
