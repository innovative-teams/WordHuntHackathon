import { Component, OnInit } from '@angular/core';
import { baseUrlForImage } from 'src/api';

@Component({
  selector: 'app-error404-page',
  templateUrl: './error404-page.component.html',
  styleUrls: ['./error404-page.component.css'],
})
export class Error404PageComponent implements OnInit {
  imageUrl = baseUrlForImage;
  constructor() {}

  ngOnInit(): void {}
}
