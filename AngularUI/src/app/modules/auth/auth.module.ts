import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AuthRoutingModule } from './auth-routing.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { LoginPageComponent } from 'src/app/pages/auth/login-page/login-page.component';
import { RegisterPageComponent } from 'src/app/pages/auth/register-page/register-page.component';
import { AuthPageComponent } from 'src/app/pages/auth/auth-page/auth-page.component';
import { LogoutComponent } from 'src/app/components/logout/logout.component';
import { SkoreBoardComponent } from 'src/app/components/skore-board/skore-board.component';

@NgModule({
  declarations: [
    LoginPageComponent,
    RegisterPageComponent,
    AuthPageComponent,
    LogoutComponent,
    SkoreBoardComponent,
  ],
  imports: [CommonModule, AuthRoutingModule, FormsModule, ReactiveFormsModule],
})
export class AuthModule {}
