import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { HomeRoutingModule } from './home-routing.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HomePageComponent } from 'src/app/pages/main/home-page/home-page.component';
import { FindTypoPageComponent } from 'src/app/pages/main/find-typo-page/find-typo-page.component';
import { NewWordPageComponent } from 'src/app/pages/main/new-word-page/new-word-page.component';
import { QuizPageComponent } from 'src/app/pages/main/quiz-page/quiz-page.component';
import { TranslatePageComponent } from 'src/app/pages/main/translate-page/translate-page.component';
import { LeaderBordComponent } from 'src/app/components/leader-bord/leader-bord.component';
import { FooterComponent } from 'src/app/components/footer/footer.component';

@NgModule({
  declarations: [
    HomePageComponent,
    TranslatePageComponent,
    FindTypoPageComponent,
    QuizPageComponent,
    NewWordPageComponent,
    LeaderBordComponent,
    FooterComponent,
  ],
  imports: [CommonModule, HomeRoutingModule, FormsModule, ReactiveFormsModule],
})
export class HomeModule {}
