import { Component, OnInit } from '@angular/core';

import { AuthService } from './../_services/auth.service';
import { AlertifyService } from './../_services/alertify.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  constructor(private alertify: AlertifyService,
              private authService: AuthService) { }

  isRegisterMode = () => this.authService.registerMode;
  isPWResetMode = () => this.authService.pwResetMode;
  isLoggedIn = () => this.authService.loggedIn();

  ngOnInit() {
  }

  enterRegisterMode() {
    this.authService.enterRegisterMode();
    this.alertify.message('Registration started.');
  }

}
