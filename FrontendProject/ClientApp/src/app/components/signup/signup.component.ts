import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { SignupModel } from 'src/app/@core/models/user/user.model';
import { UserService } from 'src/app/@core/services/implementation/user/user.service';

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.scss']
})
export class SignupComponent implements OnInit {
  head = 'Sign Up'
  title = "Welcome! Enter your details below to sign up."
  email: string = '';
  firstName: string = '';
  lastName: string = '';
  password: string = '';
  ValidationErrors: string = '';
  isProcessing: boolean = false;
  signupForm: FormGroup = this.formBuilder.group({
    firstName: [this.firstName, [Validators.required, Validators.minLength(5), Validators.maxLength(30)]],
    lastName: [this.lastName, [Validators.required, Validators.minLength(3), Validators.maxLength(30)]],
    email: [this.email, [Validators.required, Validators.email]],
    password: [this.password, [Validators.required, Validators.minLength(5), Validators.maxLength(8)]],
  });

  constructor(private formBuilder: FormBuilder, private userService: UserService) {}

  ngOnInit() {
  }

  onSubmit() {
    this.ValidationErrors = '';

    if (this.signupForm.valid) {
      const credentials = new SignupModel();
      credentials.firstName = this.signupForm.value.firstName;
      credentials.lastName = this.signupForm.value.lastName;
      credentials.username = this.signupForm.value.email;
      credentials.password = this.signupForm.value.password;

      this.userService.signup(credentials)
        .then((data) => {
          if (data) {
            this.ValidationErrors = "Signup Successful. You may login now.";
          }
          else {
            this.ValidationErrors = "Signup failed.";
          }
        })
        .catch((error: any) => {
          this.ValidationErrors = "Signup failed.";
          console.log(error.toString());
        })
        .finally(() => {
          this.isProcessing = false;
        });
    }
  }
}