import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Game } from '../_models/game';
import { Gender } from '../_models/gender';
import { environment } from './../../environments/environment';
import { AuthService } from './../_services/auth.service';
import { AlertifyService } from './../_services/alertify.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  constructor(private http: HttpClient,
              private alertify: AlertifyService,
              public authService: AuthService) { }

  ngOnInit() {
  }

  registerToggle() {
    this.alertify.message('Registration started.');
    this.authService.registerMode = true;
  }


  loggedIn() {
    return this.authService.loggedIn();
  }
}
