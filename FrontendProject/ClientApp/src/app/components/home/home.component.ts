import { Component, OnInit } from '@angular/core';
import { AuthenticatedUserModel } from 'src/app/@core/models/user/user.model';
import { LocalStorageService } from 'src/app/@core/services/global/localStorage.service';
import { UserService } from 'src/app/@core/services/implementation/user/user.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {

  title = "Welcome";
  name = '';
  score: number = 0;

  constructor(private localStorageService: LocalStorageService, private userService: UserService) { }

  ngOnInit() {

    let userStr: string | null = this.localStorageService.getItem('x-user');

    if (userStr != null && userStr != '') {
      let user: AuthenticatedUserModel = JSON.parse(userStr);
      this.name = user.firstName + ' ' + user.lastName;
      
      if (this.name != null && this.name != '') {
        this.getUserBalance();
      }  
    }
  }

  getUserBalance() {
    this.score = 0;
    this.userService.getUserBalance()
      .then((data) => {
        if (data) {
          this.score = data.balance;
        }
      })
      .catch((error: any) => {
        console.log(error.toString());
      })
      .finally(() => {
      });
  }
}