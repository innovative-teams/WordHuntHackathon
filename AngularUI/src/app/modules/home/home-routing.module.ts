import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginGuard } from 'src/app/guards/login.guard';
import { FindTypoPageComponent } from 'src/app/pages/main/find-typo-page/find-typo-page.component';
import { HomePageComponent } from 'src/app/pages/main/home-page/home-page.component';
import { NewWordPageComponent } from 'src/app/pages/main/new-word-page/new-word-page.component';
import { QuizPageComponent } from 'src/app/pages/main/quiz-page/quiz-page.component';
import { TranslatePageComponent } from 'src/app/pages/main/translate-page/translate-page.component';

const routes: Routes = [
  {
    path: '',
    component: HomePageComponent,
  },
  {
    path: 'new-word',
    component: NewWordPageComponent,
    canActivate: [LoginGuard],
  },
  {
    path: 'translate',
    component: TranslatePageComponent,
    canActivate: [LoginGuard],
  },
  {
    path: 'quiz',
    component: QuizPageComponent,
    canActivate: [LoginGuard],
  },
  {
    path: 'find-typo',
    component: FindTypoPageComponent,
    canActivate: [LoginGuard],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class HomeRoutingModule {}
