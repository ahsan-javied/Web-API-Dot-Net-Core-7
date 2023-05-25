import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpEvent, HttpResponse, HttpRequest, HttpHandler, HttpErrorResponse } from '@angular/common/http';
import { catchError, map, Observable, retry, throwError } from 'rxjs';
import { LocalStorageService } from '../@core/services/global/localStorage.service';

@Injectable()
export class AppInterceptor implements HttpInterceptor {
  constructor(private localStorageService: LocalStorageService) { }

  intercept(
    request: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {

    // Modify the request or headers here
    request = request.clone({
      setHeaders: {
        'Content-Type': 'application/json',
        'Accept': 'application/json',
      }
    });

    let token: string | null = this.localStorageService.getItem('x-token');
    if (token) {
      request = request.clone({ headers: request.headers.set('Authorization', 'Bearer ' + token) });
    }
    // return next.handle(request).pipe(
    //   map((event: HttpEvent<any>) => {
    //     // Process the event and return the modified event or the original event
    //     return event;
    //   })
    // );

    // Pass the modified request to the next handler

    return next.handle(request).pipe(
      catchError((error: HttpErrorResponse) => {
        // if (error.status === 200) {}
        // else if (error.status === 201) {}
        //else  
        if (error.status === 400) {
          // Handle the 400 response here
          // You can access the error details using error.error
          console.log('Bad request:', error.error);
          // You can also throw a custom error or return a specific response
          return throwError(() => new Error(error.error || 'server error.'));
        }

        // For other error status codes, re-throw the error
        return throwError(() => new Error(error.message || 'server error.'));
      })
    );


    return next.handle(request)
      .pipe(
        map((event: HttpEvent<any>) => {
          if (event instanceof HttpResponse && event.status === 201) {
          }
          else if (event instanceof HttpResponse && event.status === 200) {
            // if (event.body.message === 'OK') {
            event.clone({ body: event.body.result });
            return event;

          }
          if (event instanceof HttpErrorResponse) {
            if (event.error instanceof ErrorEvent) {
              console.log("Error Event");
            } else {
              console.log(`error status : ${event.status} ${event.statusText}`);
              switch (event.status) {
                case 400:      //login

                  break;
                case 401:      //login
                  break;
                case 403:     //forbidden
                  break;
                case 404:     //forbidden
                  break;
              }
              return event;
            }
          } else {
            console.log("some thing else happened");
          }
          return event;
        }), retry(0),
        catchError(error => this.errorHandler(error))) as Observable<HttpEvent<any>>;
  }
  errorHandler(error: HttpErrorResponse) {
    if (error instanceof HttpErrorResponse) {
      if (error.error instanceof ErrorEvent) {
        console.log("Error Event");
      } else {
        console.log(`error status : ${error.status} ${error.statusText}`);
        switch (error.status) {
          case 400:      //Bad Request
            break;
          case 401:      //UnAuthorized
            break;
          case 403:     //Forbidden
            break;
          case 404:     //Not Found
            break;
          case 503:     //Service Unavailable
            break;
          case 408:     //Request Timeout
            break;
          case 500:     //Internal Server Error
            break;
        }
      }
    }
    return throwError(() => new Error(error.message || 'server error.'));
  }
}
