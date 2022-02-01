import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { apiUrl } from 'src/api';
import { ServiceRepository } from '../services/service-repository';
import { CheckWordTranslateDto } from '../models/word/checkWordTranslateDto';
import { DeleteModel } from '../models/deleteModel';
import { ListResponseModel } from '../models/response/listResponseModel';
import { ResponseModel } from '../models/response/responseModel';
import { SingleResponseModel } from '../models/response/singleResponseModel';
import { CheckWordTranslateRequestDto } from '../models/word/checkWordTranslateRequestDto';
import { VerifyTheWordRequestDto } from '../models/word/verifyTheWordRequestDto';
import { WordForExamDto } from '../models/word/wordForExamDto';
import { WordModel } from '../models/word/wordModel';
import { WordRequestDto } from '../models/word/wordRequestDto';
import { NewWordDto } from '../models/word/newWordDto';

@Injectable({
  providedIn: 'root',
})
export class WordService implements ServiceRepository<WordModel, string> {
  constructor(private httpClient: HttpClient) {}

  add(addModel: WordModel): Observable<ResponseModel> {
    return this.httpClient.post<ResponseModel>(apiUrl + 'words/add', addModel);
  }

  delete(deleteModel: DeleteModel): Observable<ResponseModel> {
    return this.httpClient.post<ResponseModel>(
      apiUrl + 'words/delete',
      deleteModel
    );
  }

  update(updateModel: WordModel): Observable<ResponseModel> {
    return this.httpClient.post<ResponseModel>(
      apiUrl + 'words/update',
      updateModel
    );
  }

  getById(id: string): Observable<SingleResponseModel<WordModel>> {
    return this.httpClient.get<SingleResponseModel<WordModel>>(
      apiUrl + 'words/getbyid?id=' + id
    );
  }

  getAll(): Observable<ListResponseModel<WordModel>> {
    return this.httpClient.get<ListResponseModel<WordModel>>(
      apiUrl + 'words/getall'
    );
  }

  queryTheWord(
    wordRequestDto: WordRequestDto
  ): Observable<SingleResponseModel<WordModel>> {
    return this.httpClient.post<SingleResponseModel<WordModel>>(
      apiUrl + 'words/querytheword',
      wordRequestDto
    );
  }

  getNewWord(): Observable<SingleResponseModel<NewWordDto>> {
    return this.httpClient.get<SingleResponseModel<NewWordDto>>(
      apiUrl + 'words/getnewword'
    );
  }

  getWordFromPool(): Observable<SingleResponseModel<WordForExamDto>> {
    return this.httpClient.get<SingleResponseModel<WordForExamDto>>(
      apiUrl + 'words/getwordfrompool'
    );
  }

  checkWordTranslate(
    word: CheckWordTranslateRequestDto
  ): Observable<SingleResponseModel<CheckWordTranslateDto>> {
    return this.httpClient.post<SingleResponseModel<CheckWordTranslateDto>>(
      apiUrl + 'words/checkwordtranslate',
      word
    );
  }

  getWordWithTypo(): Observable<SingleResponseModel<WordModel>> {
    return this.httpClient.get<SingleResponseModel<WordModel>>(
      apiUrl + 'words/getwordwithtypo'
    );
  }

  verifyWord(
    word: VerifyTheWordRequestDto
  ): Observable<SingleResponseModel<CheckWordTranslateDto>> {
    return this.httpClient.post<SingleResponseModel<CheckWordTranslateDto>>(
      apiUrl + 'words/verifyword',
      word
    );
  }

  getAllTranslateLanguages(): Observable<ListResponseModel<string>> {
    return this.httpClient.get<ListResponseModel<string>>(
      apiUrl + 'words/getalltranslatelanguages'
    );
  }
}
