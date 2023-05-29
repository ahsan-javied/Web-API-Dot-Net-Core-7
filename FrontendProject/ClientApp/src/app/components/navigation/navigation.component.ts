import { Component, OnInit } from '@angular/core';
import { LocalStorageService } from 'src/app/@core/services/global/localStorage.service';
import { AuthService } from 'src/app/@core/services/implementation/user/auth.service';

@Component({
  selector: 'app-navigation',
  templateUrl: './navigation.component.html',
  styleUrls: ['./navigation.component.scss']
})
export class NavigationComponent implements OnInit {

  ifLogedIn: boolean = false;

  constructor(private localStorageService: LocalStorageService,
    private authService: AuthService) { }

  ngOnInit() {

    let userStr: string | null = this.localStorageService.getItem('x-user');

    if (userStr != null && userStr != '') {
      this.ifLogedIn = true;
    }
  }

  logoff() {
    this.authService.logout();
  }
}