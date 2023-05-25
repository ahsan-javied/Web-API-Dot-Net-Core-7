import { Injectable } from '@angular/core';
import { SignupModel, UserModel } from '../../../models/user/user.model';
import { HttpServiceInterface } from '../../interfaces/global/http.interface';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private httpService: HttpServiceInterface) { }

  signup(details: SignupModel): Promise<any> {
    return new Promise(resolve => {
      this.httpService.post<any>('users/signup', details)
        .subscribe((data) => {
          if (data) {
            resolve(data);
          }
          else {
            resolve(null);
          }
        });
    });
  }

  getUserBalance(): Promise<UserModel | null> {
    return new Promise(resolve => {
      this.httpService.getSingle<UserModel>('users/auth/balance', '')
        .subscribe((data) => {
          if (data) {
            resolve(data);
          }
          else {
            resolve(null);
          }
        });
    });
  }
}

