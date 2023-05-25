import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable,throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { GlobalModel } from '../../../models/global.model';
import { GlobalService } from '../../global/global.service';
import { HttpServiceInterface } from '../../interfaces/global/http.interface';

@Injectable()
export class HttpService extends HttpServiceInterface {

  public apiUrl: string;
  public config: GlobalModel;

  constructor(private http: HttpClient, private globalService: GlobalService) {
    super();
    this.config = this.globalService.configuration;
    this.apiUrl = `${this.config.Server + this.config.ApiUrl}/`;
  }

  getAll<T>(serviceEndpoint: string): Observable<T> {
    // let data: any; 
    // data.push({ browser: this.config.Browser, ipAddress: this.config.IpAddress, deviceInfo: this.config.DeviceInfo });
    
    return this.http.get<T>(this.apiUrl + serviceEndpoint)
    .pipe(
      catchError(this.handleError)
    );
  }

  getSingle<T>(serviceEndpoint: string, data: any): Observable<T> {
    data.browser = this.config.Browser;
    data.ipAddress = this.config.IpAddress;
    data.device = this.config.Device;

    return this.http.get<T>(this.apiUrl + serviceEndpoint + '/' + data)
    .pipe(
      catchError(this.handleError)
    );
  }


  post<T>(serviceEndpoint: string, data: any): Observable<T> {
    data.browser = this.config.Browser;
    data.ipAddress = this.config.IpAddress;
    data.device = this.config.Device;
    
    return this.http.post<T>(this.apiUrl + serviceEndpoint, data)
    .pipe(catchError(this.handleError));
  }

  private handleError(err: any) {
   
    let errorMessage: string;
    if (err.error instanceof ErrorEvent) {
      errorMessage = `An error occurred: ${err?.error?.message}`;
    } else {
      errorMessage = `Backend returned code ${err?.status}: ${err?.body?.error}`;
    }
    return throwError(() => new Error(errorMessage));
  }

}
