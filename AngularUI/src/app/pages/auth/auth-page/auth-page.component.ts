import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { StudentModel } from 'src/app/models/student/studentModel';
import { AuthService } from 'src/app/services/auth.service';
import { SeoService } from 'src/app/services/seo.service';
import { StudentService } from 'src/app/services/student.service';
import { TokenService } from 'src/app/services/token.service';
import { ValidationService } from 'src/app/services/validation.service';

@Component({
  selector: 'app-auth-page',
  templateUrl: './auth-page.component.html',
  styleUrls: ['./auth-page.component.css'],
})
export class AuthPageComponent implements OnInit {
  student: StudentModel;
  userUpdateForm: FormGroup;

  constructor(
    private formBuilder: FormBuilder,
    private tokenService: TokenService,
    private studentService: StudentService,
    private validationService: ValidationService,
    private authService: AuthService,
    private toastrService: ToastrService,
    private seoService: SeoService
  ) {}

  ngOnInit(): void {
    this.getDefaultUser();
    this.seoService.updateTitle('Profil');
    this.seoService.updateMeta('description', 'Profil sayfası');
  }

  createUserUpdateForm() {
    this.userUpdateForm = this.formBuilder.group({
      firstName: [this.student?.firstName],
      lastName: [this.student?.lastName],
      age: [this.student?.age],
      email: { value: [this.student?.email], disabled: true },
      password: [''],
      newPassword: [''],
    });
  }

  getDefaultUser() {
    var user = this.tokenService.getUserWithJWT();

    this.studentService.getById(user.userId).subscribe(
      (response) => {
        this.student = response.data;
        if (this.student != null) {
          this.createUserUpdateForm();
        }
      },
      (responseError) => {
        this.validationService.showErrors(responseError);
      }
    );
  }

  update() {
    if (this.userUpdateForm.valid) {
      let model = Object.assign({}, this.userUpdateForm.value);
      model.email = this.student.email;

      this.authService.updateStudent(model).subscribe(
        (response) => {
          this.toastrService.success(response.message);
          this.tokenService.setToken(response.data.token);
          this.tokenService.setRefreshToken(
            response.data.refreshToken.refreshTokenValue
          );
        },
        (responseError) => {
          this.validationService.showErrors(responseError);
          this.userUpdateForm.reset();
          this.getDefaultUser();
        },
        () => {
          this.userUpdateForm.reset();
          this.getDefaultUser();
        }
      );
    }
  }
}
