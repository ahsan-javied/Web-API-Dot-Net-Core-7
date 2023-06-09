import { Injectable } from '@angular/core';
import { AuthenticatedUserModel, LoginModel } from '../../../models/user/user.model';
import { LocalStorageService } from '../../global/localStorage.service';
import { HttpServiceInterface } from '../../interfaces/global/http.interface';
import { AuthenticationInterface } from '../../interfaces/user/auth.interface';

@Injectable({
  providedIn: 'root'
})
export class AuthService implements AuthenticationInterface {

  constructor(private httpService: HttpServiceInterface, private localStorageService: LocalStorageService) { }

  login(credentials: LoginModel): Promise<boolean> {
    return new Promise(resolve => {
      this.httpService.post<any>('users/authenticate', credentials)
        .subscribe((data) => {
          if (data) {
            if(data?.code == 201){
              this.localStorageService.setItem('x-token', data?.result?.token);
              let user = data.result;
              user.token = '';
              this.localStorageService.setItem('x-user', JSON.stringify(user));
              resolve(true);
              return;
            }
          }
          resolve(false);
        });
    });
  }

  logout(): void {
    this.localStorageService.removeItem('x-user');
    this.localStorageService.removeItem('x-token');
    window.location.replace('/');
  }
}