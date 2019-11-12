import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-game',
  templateUrl: './game.component.html',
  styleUrls: ['./game.component.css']
})
export class GameComponent implements OnInit {
  games: any;

  baseUrl = 'http://localhost:5000/api/games/';

  constructor(private http: HttpClient) { }

  ngOnInit() {
    this.getGames();
  }

  getGames() {
    this.http.get(this.baseUrl)
      .subscribe(response => {
        this.games = response; }, error => {
          console.log(error);
        }
      );
  }

}
