import { Injectable } from '@angular/core';
import {IApiService} from './iapi.service';
import {Observable} from 'rxjs';
import { map} from 'rxjs/operators';
import {ApiResponse, Page} from '../search/search.models';
import {environment} from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class LocalWikiApiService extends IApiService<ApiResponse>{
  search(value: string, offset: number, length: number): Observable<ApiResponse> {
    return this.http.get<any>(`${environment.localApi}/api/Page/Get?srsearch=${value}&offset=${offset}&len=${length}`).pipe(map(data => {
        let response : ApiResponse = new ApiResponse();
        response.pages = data.search.map(p =>{
          let page: Page = {pageid:p.id,title:p.title,snippet:p.snippet,timestamp:new Date(p.timestamp).toLocaleString()};
          return page;
        });
        response.totalhits = data.totalhits;
        return response;
    }));
  }

}
