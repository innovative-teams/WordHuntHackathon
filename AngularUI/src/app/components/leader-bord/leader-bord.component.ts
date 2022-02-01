import { Component, OnInit } from '@angular/core';
import { StudentModel } from 'src/app/models/student/studentModel';
import { StudentService } from 'src/app/services/student.service';
import { ValidationService } from 'src/app/services/validation.service';

@Component({
  selector: 'app-leader-bord',
  templateUrl: './leader-bord.component.html',
  styleUrls: ['./leader-bord.component.css'],
})
export class LeaderBordComponent implements OnInit {
  students: StudentModel[];

  constructor(
    private studentService: StudentService,
    private validationService: ValidationService
  ) {}

  ngOnInit(): void {
    this.getStudents();
  }

  getStudents() {
    this.studentService.getByPointForLeaderBord().subscribe(
      (response) => {
        this.students = response.data;
      },
      (responseError) => {
        this.validationService.showErrors(responseError);
      }
    );
  }
}
