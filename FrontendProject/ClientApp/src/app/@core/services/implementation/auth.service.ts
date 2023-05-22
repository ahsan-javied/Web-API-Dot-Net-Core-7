import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private isAuthenticated: boolean = false;

  constructor(private http: HttpClient) { }

  // Simulate a login request
  login(email: string, password: string): Promise<any> {
    const loginData = { email, password };


    return new Promise((resolve) => {
        setTimeout(() => {
          this.dataInterface
            .post<any>(APIRoutes.Account.ValidateUserNameAndPassword, account)
            .subscribe((data) => {
              resolve(data);
            });
        }, 1000);
      });
      
    // Make an API call to authenticate the user

    return this.http.post<boolean>('api/users/authenticate', loginData);

    // Simulate successful login
    if (email === 'user@example.com' && password === 'password') {
      this.isAuthenticated = true;
      return of(true);
    }

    // Simulate failed login
    this.isAuthenticated = false;
    return of(false);
  }

  validateAccount(account: Account): Promise<any> {
    
  }




    
  }


  // Simulate a logout
  logout(): void {
    // Perform any necessary cleanup or API calls for logout
    this.isAuthenticated = false;
  }

  // Check if the user is authenticated
  isAuthenticatedUser(): boolean {
    return this.isAuthenticated;
  }
}
