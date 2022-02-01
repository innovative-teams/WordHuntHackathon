import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AuthService } from 'src/app/services/auth.service';
import { SeoService } from 'src/app/services/seo.service';
import { TokenService } from 'src/app/services/token.service';
import { ValidationService } from 'src/app/services/validation.service';

@Component({
  selector: 'app-register-page',
  templateUrl: './register-page.component.html',
  styleUrls: ['./register-page.component.css'],
})
export class RegisterPageComponent implements OnInit {
  registerForm: FormGroup;
  constructor(
    private formBuilder: FormBuilder,
    private authService: AuthService,
    private tokenService: TokenService,
    private validationService: ValidationService,
    private toastrService: ToastrService,
    private router: Router,
    private seoService: SeoService
  ) {}

  ngOnInit(): void {
    this.createRegisterForm();
    this.seoService.updateTitle('Kayıt Ol');
    this.seoService.updateMeta('description', 'Kayıt Ol Sayfası');
  }

  createRegisterForm() {
    this.registerForm = this.formBuilder.group({
      firstName: [''],
      lastName: [''],
      age: [''],
      email: [''],
      password: [''],
    });
  }

  register() {
    let registerModel = Object.assign({}, this.registerForm.value);
    this.authService.register(registerModel).subscribe(
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
