import { Injectable } from '@angular/core';
import {Observable} from 'rxjs';
import {HttpClient} from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export abstract class IApiService<T> {
  constructor(protected http: HttpClient){}
  abstract search(value: string, offset: number, length: number ) : Observable<T>;
}
