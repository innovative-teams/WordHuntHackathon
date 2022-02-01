import { Component, OnInit } from '@angular/core';
import { StudentModel } from 'src/app/models/student/studentModel';
import { StudentService } from 'src/app/services/student.service';
import { TokenService } from 'src/app/services/token.service';

@Component({
  selector: 'app-skore-board',
  templateUrl: './skore-board.component.html',
  styleUrls: ['./skore-board.component.css'],
})
export class SkoreBoardComponent implements OnInit {
  student: StudentModel;

  constructor(
    private tokenService: TokenService,
    private studentService: StudentService
  ) {}

  ngOnInit(): void {
    this.getStudent();
  }

  getStudent() {
    var user = this.tokenService.getUserWithJWT();
    this.studentService.getById(user.userId).subscribe(
      (response) => {
        this.student = response.data;
      },
      (responseError) => {}
    );
  }
}
