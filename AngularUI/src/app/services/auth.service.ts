import { TokenService } from './token.service';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { apiUrl } from 'src/api';
import { StorageService } from './storage.service';
import { SingleResponseModel } from '../models/response/singleResponseModel';
import { LoginModel } from '../models/user/loginModel';
import { RegisterModel } from '../models/user/registerModel';
import { TokenModel } from '../models/user/tokenModel';
import { StudentForUpdateDto } from '../models/student/studentForUpdateDto';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private _refreshTokenFailedEvent: () => any;
  private _refreshTokenSucceedEvent: (token: TokenModel) => any;

  constructor(
    private httpClient: HttpClient,
    private storageService: StorageService,
    private tokenService: TokenService
  ) {}

  login(user: LoginModel): Observable<SingleResponseModel<TokenModel>> {
    let newPath = apiUrl + 'auth/login';

    return this.httpClient.post<SingleResponseModel<TokenModel>>(newPath, user);
  }

  register(user: RegisterModel): Observable<SingleResponseModel<TokenModel>> {
    let newPath = apiUrl + 'auth/register';
    return this.httpClient.post<SingleResponseModel<TokenModel>>(newPath, user);
  }

  updateStudent(
    user: StudentForUpdateDto
  ): Observable<SingleResponseModel<TokenModel>> {
    let newPath = apiUrl + 'auth/updatestudent';

    return this.httpClient.post<SingleResponseModel<TokenModel>>(newPath, user);
  }

  logout() {
    if (this.isAuthenticated()) {
      this.tokenService.removeToken();
      this.tokenService.removeRefreshToken();
      return true;
    }

    return false;
  }

  isAuthenticated(): boolean {
    if (
      this.storageService.controlItem('token') &&
      this.storageService.controlItem('refresh-token')
    ) {
      return true;
    }

    return false;
  }

  onRefreshTokenFailed() {
    if (this._refreshTokenFailedEvent != null) this._refreshTokenFailedEvent();
  }

  onRefreshTokenSucceed(token: TokenModel) {
    if (this._refreshTokenSucceedEvent != null)
      this._refreshTokenSucceedEvent(token);
  }

  setRefreshTokenEvents(
    refreshTokenFailedEvent: () => any,
    refreshTokenSucceedEvent: (token: TokenModel) => any
  ): void {
    this._refreshTokenFailedEvent = refreshTokenFailedEvent;
    this._refreshTokenSucceedEvent = refreshTokenSucceedEvent;
  }
}
