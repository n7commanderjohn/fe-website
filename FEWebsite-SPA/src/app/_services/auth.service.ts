import { environment } from './../../environments/environment';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { JwtHelperService } from '@auth0/angular-jwt';
import { DecodedJWT } from '../_models/decodedJWT';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  dotNetAPIURL = environment.apiUrl + 'auth/';
  jwtHelper = new JwtHelperService();
  decodedToken: DecodedJWT;

  constructor(private http: HttpClient) { }

  login(loginCredentials: any) {
    return this.http.post(this.dotNetAPIURL + 'login', loginCredentials)
      .pipe(
        map((response: any) => {
          const user = response;
          if (user) {
            localStorage.setItem('token', user.token);
            this.decodedToken = this.jwtHelper.decodeToken(user.token);
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
