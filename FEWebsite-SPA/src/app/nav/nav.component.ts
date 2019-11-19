import { AlertifyService } from './../_services/alertify.service';
import { AuthService } from './../_services/auth.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  loginCredentials: any = {};
  nums: number[] = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10 ];

  constructor(public authService: AuthService, private alertify: AlertifyService) { }

  ngOnInit() {
  }

  login() {
    // console.log(this.loginCredentials);
    this.authService.login(this.loginCredentials).subscribe(next => {
      this.alertify.success('Login successful.');
    }, error => {
      this.alertify.error(error);
    });
  }

  loggedIn() {
    return this.authService.loggedIn();
  }

  logout() {
    localStorage.removeItem('token');
    this.alertify.message('Logged out.');
  }

}
