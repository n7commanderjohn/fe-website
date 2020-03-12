import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { environment } from 'src/environments/environment';

import { Game } from '../_models/game';

@Injectable({
  providedIn: 'root'
})
export class GamesService {
  baseUrl = environment.apiUrl;
  game = 'game';

  constructor(private http: HttpClient) { }

  getGames(): Observable<Game[]> {
    return this.http.get<Game[]>(this.baseUrl + this.game);
  }

  getGame(id: number): Observable<Game> {
    return this.http.get<Game>(this.baseUrl + this.game + '/' + id);
  }

}
