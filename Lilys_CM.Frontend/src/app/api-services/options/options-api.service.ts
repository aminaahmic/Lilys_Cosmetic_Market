import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { environment } from '../../../environments/environment';

export interface OptionDto {
  id: number;
  name: string;
  usageCount: number;
}

export interface CreateOptionCommand {
  name: string;
}

export interface UpdateOptionCommand {
  name: string;
}

@Injectable({
  providedIn: 'root'
})
export class OptionsApiService {
  private http = inject(HttpClient);
  private readonly baseUrl = `${environment.apiUrl}/api/Options`;

  getAll() {
    return this.http.get<OptionDto[]>(this.baseUrl);
  }

  create(command: CreateOptionCommand) {
    return this.http.post<number>(this.baseUrl, command);
  }

  update(id: number, command: UpdateOptionCommand) {
    return this.http.put<void>(`${this.baseUrl}/${id}`, command);
  }

  delete(id: number) {
    return this.http.delete<void>(`${this.baseUrl}/${id}`);
  }
}