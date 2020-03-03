import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { environment } from 'src/environments/environment';

import { GameGenre } from '../_models/gameGenre';

@Injectable({
  providedIn: 'root'
})
export class GameGenresService {
  baseUrl = environment.apiUrl;
  gameGenre = 'gameGenre';

  constructor(private http: HttpClient) { }

  getGameGenres(): Observable<GameGenre[]> {
    return this.http.get<GameGenre[]>(this.baseUrl + this.gameGenre);
  }

  getGameGenre(id: number): Observable<GameGenre> {
    return this.http.get<GameGenre>(this.baseUrl + this.gameGenre + '/' + id);
  }

}
