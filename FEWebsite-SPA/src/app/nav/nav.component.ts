import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { BsDropdownConfig } from 'ngx-bootstrap/dropdown';

import { AlertifyService } from './../_services/alertify.service';
import { AuthService } from './../_services/auth.service';

import { StatusCodeResultReturnObject } from './../_models/statusCodeResultReturnObject';
import { LoginCredentials } from './../_models/loginCredentials';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css'],
  providers: [{ provide: BsDropdownConfig, useValue: { isAnimated: true, autoClose: true } }]
})
export class NavComponent implements OnInit {
  loginCredentials: LoginCredentials;
  photoUrl: string;
  nums: number[] = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10 ];

  constructor(public authService: AuthService,
              private alertify: AlertifyService,
              private router: Router) { }

  ngOnInit() {
    this.loginCredentials = new LoginCredentials();
    this.authService.currentPhotoUrl.subscribe(photoUrl => this.photoUrl = photoUrl);
  }

  login() {
    this.authService.login(this.loginCredentials).subscribe(next => {
      this.alertify.success('Login successful.');
    }, (error: StatusCodeResultReturnObject) => {
      this.alertify.error(error.response);
    }, () => {
      this.router.navigate(['/home']);
    });
  }

  loggedIn() {
    return this.authService.loggedIn();
  }

  logout() {
    localStorage.removeItem('token');
    localStorage.removeItem('user');
    this.authService.decodedToken = null;
    this.authService.currentUser = null;
    this.alertify.message('Logged out.');
    this.router.navigate(['/home']);
  }

  resetPassword() {
    this.authService.pwResetMode = true;
    this.alertify.message('User Password Reset started.');
  }
}
