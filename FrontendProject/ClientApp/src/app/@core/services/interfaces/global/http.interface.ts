import { Observable } from 'rxjs';

export abstract class HttpServiceInterface {
  abstract getAll<T>(serviceEndpoint: string): Observable<T>;
  abstract getSingle<T>(serviceEndpoint: string, obj: any): Observable<T>;
  abstract post<T>(serviceEndpoint: string, param?: any): Observable<T>;
}
