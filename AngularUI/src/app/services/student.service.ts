import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { apiUrl } from 'src/api';
import { DeleteModel } from '../models/deleteModel';
import { ListResponseModel } from '../models/response/listResponseModel';
import { ResponseModel } from '../models/response/responseModel';
import { SingleResponseModel } from '../models/response/singleResponseModel';
import { StudentModel } from '../models/student/studentModel';
import { ServiceRepository } from './service-repository';

@Injectable({
  providedIn: 'root',
})
export class StudentService implements ServiceRepository<StudentModel, number> {
  constructor(private httpClient: HttpClient) {}

  add(addModel: StudentModel): Observable<ResponseModel> {
    return this.httpClient.post<ResponseModel>(
      apiUrl + 'students/add',
      addModel
    );
  }

  delete(deleteModel: DeleteModel): Observable<ResponseModel> {
    return this.httpClient.post<ResponseModel>(
      apiUrl + 'students/delete',
      deleteModel
    );
  }

  update(updateModel: StudentModel): Observable<ResponseModel> {
    return this.httpClient.post<ResponseModel>(
      apiUrl + 'students/update',
      updateModel
    );
  }

  getById(id: number): Observable<SingleResponseModel<StudentModel>> {
    return this.httpClient.get<SingleResponseModel<StudentModel>>(
      apiUrl + 'students/getbyid?id=' + id
    );
  }

  getAll(): Observable<ListResponseModel<StudentModel>> {
    return this.httpClient.get<ListResponseModel<StudentModel>>(
      apiUrl + 'students/getall'
    );
  }

  getByPointForLeaderBord(): Observable<ListResponseModel<StudentModel>> {
    return this.httpClient.get<ListResponseModel<StudentModel>>(
      apiUrl + 'students/getbypointforleaderbord'
    );
  }
}
