import { Component, OnInit } from '@angular/core';
import { baseUrlForImage } from 'src/api';
import { SeoService } from 'src/app/services/seo.service';

@Component({
  selector: 'app-error404-page',
  templateUrl: './error404-page.component.html',
  styleUrls: ['./error404-page.component.css'],
})
export class Error404PageComponent implements OnInit {
  imageUrl = baseUrlForImage;
  constructor(private seoService: SeoService) {}

  ngOnInit(): void {
    this.seoService.updateTitle('404 - Sayfa Bulunamadı');
    this.seoService.updateMeta('description', '404 - Sayfa Bulunamadı');
  }
}
