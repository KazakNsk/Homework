import { Injectable } from '@angular/core';
import {IApiService} from './iapi.service';
import {Observable} from 'rxjs';
import {map} from 'rxjs/operators';
import {ApiResponse, Page} from '../search/search.models';
import {environment} from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class WikiMediaApiService extends IApiService<ApiResponse>{
  search(value: string, offset: number, length: number): Observable<ApiResponse> {
    return this.http.get<any>(`${environment.wikiApi}/api.php?origin=*&action=query&list=search&srsearch=${value}&sroffset=${offset}&format=json`).pipe(map(data => {
      let pages_array:Page[] = data.query.search;
      let totalhits = data.query.searchinfo.totalhits;
      //WikiMedia Api could not return results if sroffset more then 10000. Up to 10000 search results are supported
      if (totalhits > 10000) {
        totalhits = 9999;
      }
      let response :ApiResponse = new ApiResponse();
      response.pages = pages_array.map(page => {
        let p: Page = {pageid:page.pageid,title:page.title,snippet:page.snippet,timestamp:new Date(page.timestamp).toLocaleString()};
        return p;
      });
      response.totalhits = totalhits;
      return response;
    }));
  }


}
