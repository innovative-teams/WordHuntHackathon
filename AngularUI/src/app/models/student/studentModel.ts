import { UserModel } from '../user/userModel';

export interface StudentModel extends UserModel {
  firstName: string;
  lastName: string;
  age: number;
  point: number;
}
