import { TestBed } from '@angular/core/testing';

import { IApiService } from './iapi.service';

describe('IApiService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: IApiService = TestBed.get(IApiService);
    expect(service).toBeTruthy();
  });
});
