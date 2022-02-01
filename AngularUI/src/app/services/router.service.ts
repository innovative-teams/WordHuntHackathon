import { Router } from '@angular/router';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class RouterService {
  constructor(private router: Router) {}

  homeRoute() {
    this.router.navigate(['/']);
  }

  loginRoute() {
    this.router.navigate(['/auth/login']);
  }
}
