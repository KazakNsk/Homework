import { TestBed } from '@angular/core/testing';

import { LocalWikiApiService } from './local-wiki-api.service';

describe('LocalWikiApiService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: LocalWikiApiService = TestBed.get(LocalWikiApiService);
    expect(service).toBeTruthy();
  });
});
