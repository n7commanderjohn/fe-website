import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { JwtHelperService } from '@auth0/angular-jwt';
import { map } from 'rxjs/operators';
import { User } from './../_models/user';
import { environment } from './../../environments/environment';
import { LoginResponse } from './../_models/loginResponse';
import { DecodedJWT } from '../_models/decodedJWT';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  dotNetAPIURL = environment.apiUrl + 'auth/';
  jwtHelper = new JwtHelperService();
  decodedToken: DecodedJWT;
  currentUser: User;

  constructor(private http: HttpClient) { }

  login(loginCredentials: any) {
    return this.http.post(this.dotNetAPIURL + 'login', loginCredentials)
      .pipe(
        map((loginResponse: LoginResponse) => {
          if (loginResponse) {
            localStorage.setItem('token', loginResponse.token);
            localStorage.setItem('user', JSON.stringify(loginResponse.user));
            this.decodedToken = this.jwtHelper.decodeToken(loginResponse.token);
            this.currentUser = loginResponse.user;
            console.log(this.decodedToken);
          }
        })
      );
  }

  register(model: any) {
    return this.http.post(this.dotNetAPIURL + 'register', model);
  }

  loggedIn() {
    const token = localStorage.getItem('token');

    return !this.jwtHelper.isTokenExpired(token);
  }
}
