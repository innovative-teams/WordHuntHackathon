import { TokenService } from './../services/token.service';
import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpResponse,
  HttpErrorResponse,
} from '@angular/common/http';
import { Observable } from 'rxjs';
import { apiUrl } from 'src/api';
import { AuthService } from '../services/auth.service';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {
  token: string;
  refreshToken: string;

  constructor(
    private tokenService: TokenService,
    private authService: AuthService
  ) {}

  intercept(
    request: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    this.token = this.tokenService.getToken();
    this.refreshToken = this.tokenService.getRefreshToken();

    let newRequest: HttpRequest<any>;

    newRequest = request.clone({
      headers: request.headers
        .set('Authorization', 'Bearer ' + this.token ?? '')
        .append('RefreshToken', this.refreshToken ?? '')
        .append('lang', localStorage.getItem('lang') ?? ''),
    });

    if (this.refreshToken != null && this.tokenService.isTokenExpired()) {
      let refreshTokenRequest = request.clone({
        method: 'POST',
        url: apiUrl + 'auth/refreshtoken',
        headers: request.headers
          .set('Authorization', 'Bearer ' + this.token ?? '')
          .append('RefreshToken', this.refreshToken ?? '')
          .append('lang', localStorage.getItem('lang') ?? ''),
      });

      next.handle(refreshTokenRequest).subscribe(
        (response) => {
          if (response instanceof HttpResponse) {
            this.tokenService.setToken(response.body.data.token);
            this.tokenService.setRefreshToken(
              response.body.data.refreshToken.refreshTokenValue
            );
            this.authService.onRefreshTokenSucceed(response.body.data);
          } else if (response instanceof HttpErrorResponse) {
            this.tokenService.removeToken();
            this.tokenService.removeRefreshToken();
            this.authService.onRefreshTokenFailed();
          }
        },
        (responseError) => {
          this.tokenService.removeToken();
          this.tokenService.removeRefreshToken();
          this.authService.onRefreshTokenFailed();
        }
      );
    }

    return next.handle(newRequest);
  }
}
