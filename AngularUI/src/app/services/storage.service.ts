import { CookieService } from 'ngx-cookie';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class StorageService {
  localStorage: Storage;

  constructor(private cookieService: CookieService) {
    this.localStorage = window.localStorage;
  }

  getItem(key: string) {
    return this.cookieService.get(key);
  }

  setItem(key: string, value: string) {
    this.cookieService.put(key, value);
  }

  removeItem(key: string) {
    this.cookieService.remove(key);
  }

  clearAll() {
    this.cookieService.removeAll();
  }

  controlItem(key: string) {
    if (this.cookieService.get(key) != null) {
      return true;
    }
    return false;
  }
}
