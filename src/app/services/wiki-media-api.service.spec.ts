import { TestBed } from '@angular/core/testing';

import { WikiMediaApiService } from './wiki-media-api.service';

describe('WikiMediaApiService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: WikiMediaApiService = TestBed.get(WikiMediaApiService);
    expect(service).toBeTruthy();
  });
});
