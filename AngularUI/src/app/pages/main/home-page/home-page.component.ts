import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/services/auth.service';
import { SeoService } from 'src/app/services/seo.service';

@Component({
  selector: 'app-home-page',
  templateUrl: './home-page.component.html',
  styleUrls: ['./home-page.component.css'],
})
export class HomePageComponent implements OnInit {
  isAuthenticated: boolean;
  constructor(
    private authService: AuthService,
    private seoService: SeoService
  ) {}

  ngOnInit(): void {
    this.isAuthenticated = this.authService.isAuthenticated();
    this.seoService.updateTitle('Word Hunt Hackathon');
    this.seoService.updateMeta('description', 'Word Hunt Hackathon');
  }
}
