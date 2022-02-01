import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AuthService } from 'src/app/services/auth.service';
import { SeoService } from 'src/app/services/seo.service';
import { TokenService } from 'src/app/services/token.service';
import { ValidationService } from 'src/app/services/validation.service';

@Component({
  selector: 'app-login-page',
  templateUrl: './login-page.component.html',
  styleUrls: ['./login-page.component.css'],
})
export class LoginPageComponent implements OnInit {
  loginForm: FormGroup;

  constructor(
    private formBuilder: FormBuilder,
    private toastrService: ToastrService,
    private validationService: ValidationService,
    private authService: AuthService,
    private tokenService: TokenService,
    private router: Router,
    private seoService: SeoService
  ) {}

  ngOnInit(): void {
    this.createLoginForm();
    this.seoService.updateTitle('Giriş Yap');
    this.seoService.updateMeta('description', 'Giriş Yap Sayfası');
  }

  createLoginForm() {
    this.loginForm = this.formBuilder.group({
      email: [''],
      password: [''],
    });
  }

  login() {
    let loginModel = Object.assign({}, this.loginForm.value);
    this.authService.login(loginModel).subscribe(
      (response) => {
        this.tokenService.setRefreshToken(
          response.data.refreshToken.refreshTokenValue
        );
        this.tokenService.setToken(response.data.token);

        window.location.href = '/';
      },
      (responseError) => {
        this.validationService.showErrors(responseError);
      }
    );
  }
}
