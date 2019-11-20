import { AlertifyService } from './../_services/alertify.service';
import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  registerMode = false;
  games: any;
  baseUrl = 'http://localhost:5000/api/games/';


  constructor(private http: HttpClient, private alertify: AlertifyService) { }

  ngOnInit() {
    this.getGames();
  }

  registerToggle() {
    this.alertify.message('Registration started.');
    this.registerMode = true;
  }

  getGames() {
    this.http.get(this.baseUrl)
      .subscribe((response: any) => {
        this.games = response; }, (error: any) => {
          console.log(error);
        }
      );
  }

  cancelRegisterMode(registerMode: boolean) {
    this.registerMode = registerMode;
  }
}
