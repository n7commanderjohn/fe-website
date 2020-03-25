import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { BsDropdownConfig } from 'ngx-bootstrap/dropdown';

import { AlertifyService } from './../_services/alertify.service';
import { AuthService } from './../_services/auth.service';

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
    this.authService.currentPhotoUrl.subscribe({next: photoUrl => this.photoUrl = photoUrl});
  }

  login() {
    this.authService.login(this.loginCredentials).subscribe({
      next: () => {
        this.alertify.success('Login successful.');
      },
      error: () => {
        this.alertify.error('Your login credentials are incorrect.');
      },
      complete: () => {
        this.router.navigate(['/home']);
    }});
  }

  logout() {
    this.authService.logout();
    this.alertify.message('Logged out.');
    this.router.navigate(['/home']);
  }

  enterResetPasswordMode() {
    this.authService.enterPWResetMode();
    this.alertify.message('User Password Reset started.');
  }

  isLoggedIn() {
    return this.authService.loggedIn();
  }
}
