import { Component, OnInit } from '@angular/core';
import { CheckWordTranslateDto } from 'src/app/models/word/checkWordTranslateDto';
import { WordForExamDto } from 'src/app/models/word/wordForExamDto';
import { SeoService } from 'src/app/services/seo.service';
import { ValidationService } from 'src/app/services/validation.service';
import { WordService } from 'src/app/services/word.service';

@Component({
  selector: 'app-quiz-page',
  templateUrl: './quiz-page.component.html',
  styleUrls: ['./quiz-page.component.css'],
})
export class QuizPageComponent implements OnInit {
  wordForExamDto: WordForExamDto;
  checkWordTranslateDto: CheckWordTranslateDto;
  data: string = '';
  alertText: string;
  isAnswerCorrect: boolean;
  isNext = false;
  isAnswered = false;
  isFinished: boolean;
  finishText: string = '';

  constructor(
    private wordService: WordService,
    private validationService: ValidationService,
    private seoService: SeoService
  ) {}

  ngOnInit(): void {
    this.getWordFromPool();
    this.seoService.updateTitle('Kelime Testi');
    this.seoService.updateMeta('description', 'Kelime Testi');
  }

  getWordFromPool() {
    this.wordService.getWordFromPool().subscribe(
      (response) => {
        console.log(response);

        this.wordForExamDto = response.data;
        if (this.wordForExamDto == null && response.message != null) {
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
        this.isAnswerCorrect = null;
        this.isAnswered = false;
      }
    );
  }

  checkWordTranslate() {
    if (this.data !== '') {
      this.isAnswered = true;

      this.wordService
        .checkWordTranslate({
          word: this.wordForExamDto.word,
          option: this.data,
        })
        .subscribe(
          (response) => {
            this.checkWordTranslateDto = response.data;
            this.isAnswerCorrect = true;
            this.alertText = 'Tebrikler! Doğru cevap!';
          },
          (responseError) => {
            this.isAnswerCorrect = false;
            this.alertText =
              'Yanlış Cevap! Doğru Cevap : ' +
              responseError.error.data.correctWord;
          },
          () => {}
        );
    }
  }

  changeStyleAlert() {
    if (this.isAnswerCorrect == null || this.isNext) {
      return 'display:none';
    }

    if (this.isAnswerCorrect) {
      return 'color: white;border: 1px solid green;border-radius: 50px;margin-top: 5vh;';
    } else {
      return 'color: white;border: 1px solid red;border-radius: 50px;margin-top: 5vh;';
    }
  }

  changeTextStyleAlert() {
    if (this.isAnswerCorrect) {
      return 'color: rgb(43, 206, 43);';
    } else {
      return 'color:#e82b09';
    }
  }
}
