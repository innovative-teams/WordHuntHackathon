import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AuthRoutingModule } from './auth-routing.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { LoginPageComponent } from 'src/app/pages/auth/login-page/login-page.component';
import { RegisterPageComponent } from 'src/app/pages/auth/register-page/register-page.component';
import { AuthPageComponent } from 'src/app/pages/auth/auth-page/auth-page.component';

@NgModule({
  declarations: [LoginPageComponent, RegisterPageComponent, AuthPageComponent],
  imports: [CommonModule, AuthRoutingModule, FormsModule, ReactiveFormsModule],
})
export class AuthModule {}
