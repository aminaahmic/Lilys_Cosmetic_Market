import {
  HttpInterceptorFn,
  HttpErrorResponse,
  HttpRequest,
  HttpHandlerFn
} from '@angular/common/http';
import { inject } from '@angular/core';
import { Observable, BehaviorSubject, throwError } from 'rxjs';
import { catchError, filter, switchMap, take } from 'rxjs/operators';
import { AuthFacadeService } from '../services/auth/auth-facade.service';

let refreshInProgress = false;
const refreshTokenSubject = new BehaviorSubject<string | null>(null);

export const authInterceptor: HttpInterceptorFn = (req, next) => {
  const auth = inject(AuthFacadeService);

  // LOGIN i REFRESH ne trebaju bearer token
  if (isLoginOrRefreshEndpoint(req.url)) {
    return next(req);
  }

  // LOGOUT i svi ostali zaštićeni requestovi trebaju bearer token
  const accessToken = auth.getAccessToken();
  let authReq = req;

  if (accessToken) {
    authReq = req.clone({
      setHeaders: {
        Authorization: `Bearer ${accessToken}`
      }
    });
  }

  return next(authReq).pipe(
    catchError((err) => {
      // Ne pokušavaj refresh za login/refresh/logout endpointove
      if (
        err instanceof HttpErrorResponse &&
        err.status === 401 &&
        !isAnyAuthEndpoint(req.url)
      ) {
        return handle401Error(authReq, next, auth);
      }

      return throwError(() => err);
    })
  );
};

function isLoginOrRefreshEndpoint(url: string): boolean {
  const lower = url.toLowerCase();
  return lower.includes('/auth/login') || lower.includes('/auth/refresh');
}

function isAnyAuthEndpoint(url: string): boolean {
  return url.toLowerCase().includes('/auth/');
}

function handle401Error(
  req: HttpRequest<unknown>,
  next: HttpHandlerFn,
  auth: AuthFacadeService
): Observable<any> {
  const refreshToken = auth.getRefreshToken();

  if (!refreshToken) {
    auth.redirectToLogin();
    return throwError(() => new Error('No refresh token'));
  }

  if (refreshInProgress) {
    return refreshTokenSubject.pipe(
      filter((token) => token !== null),
      take(1),
      switchMap((token) => {
        const cloned = token
          ? req.clone({ setHeaders: { Authorization: `Bearer ${token}` } })
          : req;
        return next(cloned);
      })
    );
  }

  refreshInProgress = true;
  refreshTokenSubject.next(null);

  return auth.refresh({ refreshToken, fingerprint: null }).pipe(
    switchMap((res) => {
      refreshInProgress = false;
      const newAccessToken = res.accessToken;
      refreshTokenSubject.next(newAccessToken);

      const clonedReq = req.clone({
        setHeaders: { Authorization: `Bearer ${newAccessToken}` }
      });

      return next(clonedReq);
    }),
    catchError((error) => {
      refreshInProgress = false;
      refreshTokenSubject.next(null);
      auth.redirectToLogin();
      return throwError(() => error);
    })
  );
}