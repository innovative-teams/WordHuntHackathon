import { Observable } from 'rxjs';
import { DeleteModel } from '../models/deleteModel';
import { ListResponseModel } from '../models/response/listResponseModel';
import { ResponseModel } from '../models/response/responseModel';
import { SingleResponseModel } from '../models/response/singleResponseModel';

export interface ServiceRepository<TModel, TId> {
  add(addModel: TModel): Observable<ResponseModel>;

  delete(deleteModel: DeleteModel): Observable<ResponseModel>;

  update(updateModel: TModel): Observable<ResponseModel>;

  getById(id: TId): Observable<SingleResponseModel<TModel>>;

  getAll(): Observable<ListResponseModel<TModel>>;
}
