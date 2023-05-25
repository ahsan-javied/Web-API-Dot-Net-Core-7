export class SignupModel {
  username: string = '';
  password: string = '';
  firstName: string = '';
  lastName: string = '';
  device: string = '';
  ipaddress: string = '';
  browser: string = ''
}

export class LoginModel {
  username: string = '';
  password: string = '';
  device: string = '';
  ipaddress: string = '';
  browser: string = ''
}

export class AuthenticatedUserModel {
  firstName: string = '';
  lastName: string = '';
  token: string = '';
}

export class UserModel {
  balance: number = 0;
}