import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthService } from 'src/app/@core/services/implementation/auth.service';

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
  loginForm: FormGroup = this.formBuilder.group({
    //email: [ this.tmpMail, [Validators.required, Validators.pattern(EmailPattern)]],
    email: ['', Validators.compose([Validators.required, Validators.pattern('^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}$')])],
    password: ['', [Validators.required, Validators.minLength(8), Validators.maxLength(30)]],
  });

  constructor(private formBuilder: FormBuilder, private authService: AuthService) { }

  ngOnInit(): void {
    

  }

  login(): void {

    if (this.loginForm.invalid) {
      return;
    }

    this.authService.login(this.email, this.password)
    .subscribe(
      isAuthenticated => {
        if (isAuthenticated) {
          console.log('Login successful');
          // Redirect to the desired page or perform any necessary actions upon successful login
        } else {
          console.log('Login failed');
          // Show an error message or perform any necessary actions upon failed login
        }
      }
    );
  }
}

