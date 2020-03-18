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

  ngOnInit() {
  }

  enterRegisterMode() {
    this.authService.enterRegisterMode();
    this.alertify.message('Registration started.');
  }

  isRegisterMode() {
    return this.authService.registerMode;
  }

  isPWResetMode() {
    return this.authService.pwResetMode;
  }

  isLoggedIn() {
    return this.authService.loggedIn();
  }
}
