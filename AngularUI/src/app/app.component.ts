import { Component, OnInit } from '@angular/core';
import { ValidationService } from './services/validation.service';
import { translates } from 'src/api';
import { AuthService } from './services/auth.service';
import { SeoService } from './services/seo.service';
import { TranslateService } from './services/translate.service';
import { RouterService } from './services/router.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent implements OnInit {
  translateKeys: any;

  constructor(
    private translateService: TranslateService,
    private validationService: ValidationService,
    private authService: AuthService,
    private seoService: SeoService,
    private routerService: RouterService
  ) {}

  ngOnInit(): void {
    this.getTranslates();

    this.authService.setRefreshTokenEvents(
      () => {
        this.routerService.loginRoute();
      },
      (token) => {
        console.log('Succeed' + token.expiration);
      }
    );
  }

  getTranslates() {
    var lang = localStorage.getItem('lang');

    if (lang == null) {
      lang = navigator.language;
      localStorage.setItem('lang', lang);
    }

    this.seoService.updateLang(lang);

    this.translateService.getTranslates(lang).subscribe(
      (response) => {
        translates.keys = response.data;
        this.translateKeys = translates;
      },
      (responseError) => {
        this.validationService.showErrors(responseError);

        lang = navigator.language;
        localStorage.setItem('lang', lang);

        this.seoService.updateLang(lang);
        this.translateService.getTranslates(lang).subscribe(
          (response) => {
            translates.keys = response.data;
            this.translateKeys = translates;
          },
          (responseError) => {
            this.validationService.showErrors(responseError);
            this.translateKeys = null;
          }
        );
      }
    );
  }
}
