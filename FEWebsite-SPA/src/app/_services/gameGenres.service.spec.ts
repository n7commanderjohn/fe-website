/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { GameGenresService } from './gameGenres.service';

describe('Service: Genres', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [GameGenresService]
    });
  });

  it('should ...', inject([GameGenresService], (service: GameGenresService) => {
    expect(service).toBeTruthy();
  }));
});
