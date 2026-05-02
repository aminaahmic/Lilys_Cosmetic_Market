import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment';

export interface SubcategoryDto {
  id: number;
  name: string;
}

@Injectable({
  providedIn: 'root'
})
export class SubcategoriesApiService {
  private http = inject(HttpClient);
  private readonly baseUrl = `${environment.apiUrl}/api/Subcategories`;

  getByCategory(categoryId: number) {
    return this.http.get<SubcategoryDto[]>(`${this.baseUrl}?categoryId=${categoryId}`);
  }
}