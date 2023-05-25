import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CustomHttpClientService {


  constructor(private http: HttpClient) {}

  getPublicIpAddress(): Observable<any> {
    let apiUrl = 'http://ip-api.com/json';

    return this.http.get<any>(apiUrl);
  }
  
}
