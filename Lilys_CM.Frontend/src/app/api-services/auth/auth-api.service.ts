import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import {
  LoginCommand,
  LoginCommandDto,
  RegisterCommand,
  RegisterCommandDto,
  ForgotPasswordCommand,
  ForgotPasswordCommandDto,
  ResetPasswordCommand,
  ResetPasswordCommandDto,
  RefreshTokenCommand,
  RefreshTokenCommandDto,
  LogoutCommand
} from './auth-api.model';

@Injectable({
  providedIn: 'root'
})
export class AuthApiService {
  private readonly baseUrl = `${environment.apiUrl}/api/Auth`;
  private http = inject(HttpClient);

  login(payload: LoginCommand): Observable<LoginCommandDto> {
    return this.http.post<LoginCommandDto>(`${this.baseUrl}/login`, payload);
  }

  register(payload: RegisterCommand): Observable<RegisterCommandDto> {
    return this.http.post<RegisterCommandDto>(`${this.baseUrl}/register`, payload);
  }


  forgotPassword(payload: ForgotPasswordCommand): Observable<ForgotPasswordCommandDto> {
    return this.http.post<ForgotPasswordCommandDto>(`${this.baseUrl}/forgot-password`, payload);
  }

 
  resetPassword(payload: ResetPasswordCommand): Observable<ResetPasswordCommandDto> {
    return this.http.post<ResetPasswordCommandDto>(`${this.baseUrl}/reset-password`, payload);
  }

  refresh(payload: RefreshTokenCommand): Observable<RefreshTokenCommandDto> {
    return this.http.post<RefreshTokenCommandDto>(`${this.baseUrl}/refresh`, payload);
  }

  logout(payload: LogoutCommand): Observable<void> {
    return this.http.post<void>(`${this.baseUrl}/logout`, payload);
  }
}