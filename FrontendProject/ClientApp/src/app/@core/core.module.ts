import { HttpClientModule } from "@angular/common/http";
import { APP_INITIALIZER, NgModule } from "@angular/core";
import { CustomHttpClientService } from "./services/global/customHttpClient.service";
import { GlobalService } from "./services/global/global.service";
import { LocalStorageService } from "./services/global/localStorage.service";
import { HttpService } from "./services/implementation/common/http.service";
import { AuthService } from "./services/implementation/user/auth.service";
import { UserService } from "./services/implementation/user/user.service";
import { HttpServiceInterface } from "./services/interfaces/global/http.interface";
import { AuthenticationInterface } from "./services/interfaces/user/auth.interface";

const DATA_SERVICES = [
    { provide: HttpServiceInterface, useClass:  HttpService },
    { provide: AuthenticationInterface, useClass: AuthService },
    GlobalService, LocalStorageService, CustomHttpClientService, UserService
];

const appInitializerFn = (globalService: GlobalService) => {
    return () => {
        return globalService.setConfig();
    };
};

@NgModule({
    declarations: [],
    imports: [
        HttpClientModule,
    ],
    exports: [],
    providers: [DATA_SERVICES,
        {
            provide: APP_INITIALIZER,
            useFactory: appInitializerFn,
            multi: true,
            deps: [GlobalService, LocalStorageService]
        }
    ],
})
export class CoreModule { }
