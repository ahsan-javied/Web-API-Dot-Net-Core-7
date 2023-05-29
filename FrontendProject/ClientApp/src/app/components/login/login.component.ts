import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { LoginModel } from 'src/app/@core/models/user/user.model';
import { AuthService } from 'src/app/@core/services/implementation/user/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  head = 'Log in';
  title = "Welcome back! Enter your email and password below to sign in.";
  email: string = '';
  password: string = '';
  ValidationErrors: string = '';
  isProcessing: boolean = false;
  loginForm: FormGroup = this.formBuilder.group({
    email: [this.email, [Validators.required, Validators.email]],
    password: [this.password, [Validators.required, Validators.minLength(5), Validators.maxLength(8)]],
  });

  constructor(private formBuilder: FormBuilder, private authService: AuthService,
    public router: Router
    ) { }

  ngOnInit() {
  }

  onSubmit() {
    this.ValidationErrors = '';

    if (this.loginForm.valid) {
      const credentials = new LoginModel();
      credentials.username = this.loginForm.value.email;
      credentials.password = this.loginForm.value.password;

      this.authService.login(credentials)
        .then((data) => {
          if (data) {
            this.router.navigate(['\home']);
          }
          else {
            this.ValidationErrors = "Login failed.";
          }
        })
        .catch((error: any) => {
          this.ValidationErrors = "Login failed.";
          console.log(error.toString());
        })
        .finally(() => {
          this.isProcessing = false;
        });
    }
  }
}