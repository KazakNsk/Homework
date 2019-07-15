import {Component, OnInit, ViewChild} from '@angular/core';
import {IApiService} from '../services/iapi.service';
import {LocalWikiApiService} from '../services/local-wiki-api.service';
import {WikiMediaApiService} from '../services/wiki-media-api.service';
import {MatPaginator} from '@angular/material';
import {ApiResponse, Page} from './search.models';
import {HttpErrorResponse} from '@angular/common/http';
import {of} from 'rxjs';
import {ApiType} from '../services/ApiType';
@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.css'],
})
export class SearchComponent implements OnInit {
  pages: Page[];
  error_message:string;

  searchSuccessful:boolean = true;
  isSearchNow:boolean = false;

  @ViewChild(MatPaginator, {static: false}) paginator: MatPaginator;

  ApiType = ApiType;
  apiType: ApiType = ApiType.WikiMediaApi;

  defaultPageSize = 10;
  searchValue: string;
  totalhits: number = 0;
  columnsToDisplay = ['pageid','title','snippet','timestamp'];
  constructor(private localApi:LocalWikiApiService,private wikiApi:WikiMediaApiService) {
  }

  ngOnInit() {
  }

  getApiService():IApiService<ApiResponse>
  {
    switch (this.apiType) {
      case ApiType.LocalApi: return this.localApi;
      case ApiType.WikiMediaApi:return this.wikiApi;
    }
  }
  search(value: string): void {
    this.searchValue = value;
    if(value==="") return;
    this.isSearchNow = true;
    this.getApiService()!.search(value, 0,this.defaultPageSize).subscribe(response => {
      this.pages = response.pages;
      this.totalhits = response.totalhits;
    },(error) => this.handleError(error),()=>{
      this.searchSuccessful = this.totalhits != 0;
      this.isSearchNow = false;
      this.error_message = "";
      this.paginator.pageIndex = 0;
    });
  }
  change(): void {
    this.isSearchNow = true;
    this.getApiService().search(this.searchValue, (this.paginator.pageIndex) * 10,this.apiType)
      .subscribe(pages => {
        this.pages = pages.pages;
        this.isSearchNow = false;
        this.error_message = "";
      },(error)=> this.handleError(error));
  }

  private handleError(error: HttpErrorResponse) {
    this.searchSuccessful = false;
    this.totalhits = 0;
    this.isSearchNow = false;
    if(error.status === 0) {
      this.error_message = "Connection error";
    }
    console.log(`handleError: `+error.message);
    return of([]);
  };
}
