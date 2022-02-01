import { Injectable } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';
import { UserModel } from '../models/user/userModel';
import { StorageService } from './storage.service';

@Injectable({
  providedIn: 'root',
})
export class TokenService {
  private jwtHelperService: JwtHelperService = new JwtHelperService();

  constructor(private storageService: StorageService) {}

  decodeToken(token: string) {
    return this.jwtHelperService.decodeToken(token);
  }

  getToken(): string {
    return this.storageService.getItem('token');
  }

  setToken(token: string): void {
    this.storageService.setItem('token', token);
  }

  removeToken(): void {
    this.storageService.removeItem('token');
  }

  getRefreshToken(): string {
    return this.storageService.getItem('refresh-token');
  }

  setRefreshToken(refreshToken: string): void {
    this.storageService.setItem('refresh-token', refreshToken);
  }

  removeRefreshToken(): void {
    this.storageService.removeItem('refresh-token');
  }

  isTokenExpired(): boolean {
    let isExpired = this.jwtHelperService.isTokenExpired(this.getToken());

    return isExpired != null ? isExpired : true;
  }

  getTokenExpirationDate(): Date {
    return this.jwtHelperService.getTokenExpirationDate(this.getToken());
  }

  getUserWithJWT(): UserModel {
    let token = this.jwtHelperService.decodeToken(this.getToken());

    if (token != null) {
      let userModel = {
        userId:
          +token[
            Object.keys(token).filter((t) => t.endsWith('nameidentifier'))[0]
          ],
        email: token.email,
        status: Boolean(token.status),
      };

      return userModel;
    }

    return null;
  }
}
