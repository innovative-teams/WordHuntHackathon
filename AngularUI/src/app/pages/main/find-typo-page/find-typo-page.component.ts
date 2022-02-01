import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';
import { CheckWordTranslateDto } from 'src/app/models/word/checkWordTranslateDto';
import { WordModel } from 'src/app/models/word/wordModel';
import { SeoService } from 'src/app/services/seo.service';
import { ValidationService } from 'src/app/services/validation.service';
import { WordService } from 'src/app/services/word.service';

@Component({
  selector: 'app-find-typo-page',
  templateUrl: './find-typo-page.component.html',
  styleUrls: ['./find-typo-page.component.css'],
})
export class FindTypoPageComponent implements OnInit {
  findTypoForm: FormGroup;
  wordModel: WordModel;
  checkWordTranslateDto: CheckWordTranslateDto;
  isSuccess: boolean;
  alertMessage = '';
  isNext: boolean = false;
  isAnswered: boolean = false;
  isFinished: boolean;
  finishText = '';

  constructor(
    private formBuilder: FormBuilder,
    private wordService: WordService,
    private validationService: ValidationService,
    private seoService: SeoService
  ) {}

  ngOnInit(): void {
    this.getWordWithTypo();
    this.createFindTypoForm();
    this.seoService.updateTitle('Yazım Hatasını Bul');
    this.seoService.updateMeta('description', 'Yazım Hatasını Bul');
  }

  createFindTypoForm() {
    this.findTypoForm = this.formBuilder.group({
      newWord: [''],
    });
  }

  getWordWithTypo() {
    this.wordService.getWordWithTypo().subscribe(
      (response) => {
        this.wordModel = response.data;
        if (this.wordModel == null && response.message != null) {
          this.isFinished = true;
          this.finishText = response.message;
        } else {
          this.isFinished = false;
          this.finishText = '';
        }
      },
      (responseError) => {
        this.validationService.showErrors(responseError);
      },
      () => {
        this.isNext = false;
        this.isSuccess = null;
        this.isAnswered = false;
        this.findTypoForm.reset();
      }
    );
  }

  verifyWord() {
    if (this.findTypoForm.value.newWord != '') {
      this.isAnswered = true;
      this.wordService
        .verifyWord({
          word: this.wordModel,
          newWord: this.findTypoForm.value.newWord,
        })
        .subscribe(
          (response) => {
            this.checkWordTranslateDto = response.data;
            this.isSuccess = true;
            this.alertMessage =
              'Teşekkürler! Benim Doğru Cevabı Bulmama Yardımcı Oldun!';
          },
          (responseError) => {
            this.isSuccess = false;
            this.alertMessage = responseError.error.data.correctWord;
          },
          () => {}
        );
    }
  }

  changeStyleAlert() {
    if (this.isSuccess == null || this.isNext) {
      return 'display:none';
    }

    if (this.isSuccess) {
      return 'color: white;border: 1px solid green;border-radius: 50px;margin-top: 5vh;';
    } else {
      return 'color: white;border: 1px solid red;border-radius: 50px;margin-top: 5vh;';
    }
  }

  changeTextStyleAlert() {
    if (this.isSuccess) {
      return 'color: rgb(43, 206, 43);';
    } else {
      return 'color:#e82b09';
    }
  }
}
