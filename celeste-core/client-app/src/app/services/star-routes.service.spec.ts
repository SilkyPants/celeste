/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { StarRoutesService } from './star-routes.service';

describe('Service: Temp', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [StarRoutesService]
    });
  });

  it('should ...', inject([StarRoutesService], (service: StarRoutesService) => {
    expect(service).toBeTruthy();
  }));
});
