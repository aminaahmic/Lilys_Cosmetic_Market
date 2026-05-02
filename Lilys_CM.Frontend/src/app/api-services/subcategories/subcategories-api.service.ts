import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { PageResult } from '../../core/models/paging/page-result';
import { buildHttpParams } from '../../core/models/build-http-params';
import {
  CreateSubcategoryRequest,
  ListSubcategoriesRequest,
  SubcategoryDto,
  UpdateSubcategoryRequest
} from './subcategories-api.models';

@Injectable({
  providedIn: 'root'
})
export class SubcategoriesApiService {
  private readonly baseUrl = `${environment.apiUrl}/api/Subcategories`;
  private http = inject(HttpClient);

  list(request: ListSubcategoriesRequest): Observable<PageResult<SubcategoryDto>> {
    const params = buildHttpParams(request as any);

    return this.http.get<PageResult<SubcategoryDto>>(this.baseUrl, {
      params,
    });
  }

  getById(id: number): Observable<SubcategoryDto> {
    return this.http.get<SubcategoryDto>(`${this.baseUrl}/${id}`);
  }

  create(request: CreateSubcategoryRequest): Observable<number> {
    return this.http.post<number>(this.baseUrl, request);
  }

  update(id: number, request: UpdateSubcategoryRequest): Observable<void> {
    return this.http.put<void>(`${this.baseUrl}/${id}`, request);
  }

  delete(id: number): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`);
  }

  getByCategory(categoryId: number): Observable<SubcategoryDto[]> {
    const request = new ListSubcategoriesRequest();
    request.categoryId = categoryId;
    request.onlyEnabled = true;
    request.paging.pageSize = 100;

    return this.list(request).pipe(
      map(result => result.items)
    );
  }
}