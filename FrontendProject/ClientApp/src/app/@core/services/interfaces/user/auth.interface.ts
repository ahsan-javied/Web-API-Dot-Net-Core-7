import { LoginModel } from "src/app/@core/models/user/user.model";

export abstract class AuthenticationInterface {
  abstract login(credentials: LoginModel): Promise<boolean>;
  abstract logout(): void;
}
