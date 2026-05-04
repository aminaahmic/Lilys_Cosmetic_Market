import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { environment } from '../../../environments/environment';
import {
  BrandDto,
  CreateBrandCommand,
  UpdateBrandCommand
} from './brands-api.models';

@Injectable({
  providedIn: 'root'
})
export class BrandsApiService {
  private http = inject(HttpClient);
  private readonly baseUrl = `${environment.apiUrl}/api/Brands`;

  getAll(onlyEnabled?: boolean | null, search?: string | null) {
    let params = new HttpParams();

    if (onlyEnabled !== null && onlyEnabled !== undefined) {
      params = params.set('onlyEnabled', onlyEnabled);
    }

    if (search && search.trim()) {
      params = params.set('search', search.trim());
    }

    return this.http.get<BrandDto[]>(this.baseUrl, { params });
  }

  getById(id: number) {
    return this.http.get<BrandDto>(`${this.baseUrl}/${id}`);
  }

  create(command: CreateBrandCommand) {
    return this.http.post<number>(this.baseUrl, command);
  }

  update(id: number, command: UpdateBrandCommand) {
    return this.http.put<void>(`${this.baseUrl}/${id}`, command);
  }

  delete(id: number) {
    return this.http.delete<void>(`${this.baseUrl}/${id}`);
  }
  uploadLogo(id: number, file: File) {
    const formData = new FormData();
    formData.append('file', file);

    return this.http.post<{ logoUrl: string }>(
      `${this.baseUrl}/${id}/logo`,
      formData
    );
  }
}