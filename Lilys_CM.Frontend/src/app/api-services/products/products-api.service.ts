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
  ProductFilterOptionsDto,
  CreateProductVariantCommand,
  ProductVariantDto
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
  getVariants(productId: number) {
    return this.http.get<ProductVariantDto[]>(
      `${environment.apiUrl}/api/products/${productId}/variants`
    );
  }

  createVariant(productId: number, command: CreateProductVariantCommand) {
    return this.http.post<number>(
      `${environment.apiUrl}/api/products/${productId}/variants`,
      command
    );
  }

  deleteVariant(productId: number, variantId: number) {
    return this.http.delete<void>(
      `${environment.apiUrl}/api/products/${productId}/variants/${variantId}`
    );
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

  uploadImage(productId: number, file: File) {
    const formData = new FormData();
    formData.append('file', file);

    return this.http.post<any>(`${environment.apiUrl}/api/products/${productId}/images`, formData);
  }

  getImages(productId: number) {
    return this.http.get<any[]>(`${environment.apiUrl}/api/products/${productId}/images`);
  }

  setMainImage(productId: number, imageId: number) {
    return this.http.put(`${environment.apiUrl}/api/products/${productId}/images/${imageId}/main`, {});
  }

  deleteImage(productId: number, imageId: number) {
    return this.http.delete(`${environment.apiUrl}/api/products/${productId}/images/${imageId}`);
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
