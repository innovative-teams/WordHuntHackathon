import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { NewWordDto } from 'src/app/models/word/newWordDto';
import { SeoService } from 'src/app/services/seo.service';
import { ValidationService } from 'src/app/services/validation.service';
import { WordService } from 'src/app/services/word.service';

@Component({
  selector: 'app-new-word-page',
  templateUrl: './new-word-page.component.html',
  styleUrls: ['./new-word-page.component.css'],
})
export class NewWordPageComponent implements OnInit {
  word: NewWordDto;

  constructor(
    private wordService: WordService,
    private validationService: ValidationService,
    private seoService: SeoService,
    private toastrService: ToastrService
  ) {}

  ngOnInit(): void {
    this.getNewWord();
    this.seoService.updateTitle('Yeni Kelime Öğren');
    this.seoService.updateMeta('description', 'Yeni Kelime Öğren');
  }

  getNewWord() {
    this.wordService.getNewWord().subscribe(
      (response) => {
        this.word = response.data;
      },
      (responseError) => {
        this.validationService.showErrors(responseError);
      }
    );
  }

  add() {
    let x: any = {
      searchedWord: this.word.word,
      searchedWordCode: 'en',
      resultWord: this.word.result,
      resultWordCode: 'tr',
    };

    this.wordService.add(x).subscribe(
      (response) => {
        this.toastrService.success('Kelime başarıyla eklendi.');
      },
      (responseError) => {
        console.log(responseError);

        this.validationService.showErrors(responseError);
      }
    );
  }
}
