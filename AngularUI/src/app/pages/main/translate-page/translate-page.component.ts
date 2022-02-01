import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { SeoService } from 'src/app/services/seo.service';
import { ValidationService } from 'src/app/services/validation.service';
import { WordService } from 'src/app/services/word.service';

@Component({
  selector: 'app-translate-page',
  templateUrl: './translate-page.component.html',
  styleUrls: ['./translate-page.component.css'],
})
export class TranslatePageComponent implements OnInit {
  wordForm: FormGroup;
  isSuccess: boolean = false;
  word: string;

  constructor(
    private wordService: WordService,
    private formBuilder: FormBuilder,
    private validationService: ValidationService,
    private seoService: SeoService
  ) {}

  ngOnInit(): void {
    this.createWordForm();
    this.seoService.updateTitle('Çeviri Aracı');
    this.seoService.updateMeta('description', 'Kelime çevirme aracı');
  }

  createWordForm() {
    this.wordForm = this.formBuilder.group({
      word: [''],
      targetCode: ['en'],
    });
  }

  translate() {
    if (this.wordForm.value.word != '') {
      let translateModel = Object.assign({}, this.wordForm.value);
      this.wordService.queryTheWord(translateModel).subscribe(
        (response) => {
          this.isSuccess = true;
          this.word = response.data.resultWord;
        },
        (responseError) => {
          this.isSuccess = false;
          this.validationService.showErrors(responseError);
        }
      );
    }
  }
}
