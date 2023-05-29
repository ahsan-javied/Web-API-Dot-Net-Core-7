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
            if(data?.code == 200){
              resolve(data.result);
              return;
            }
          }
          resolve(null);
        });
    });
  }

  getUserBalance(): Promise<UserModel | null> {
    return new Promise(resolve => {
      this.httpService.getSingle<any>('users/auth/balance', '')
        .subscribe((data) => {
          if (data) {
            if(data?.code == 201){
              let user: UserModel = new UserModel();
              user.balance = data?.result;
              
              resolve(user);
              return;
            }
          }
          resolve(null);
        });
    });
  }
}

