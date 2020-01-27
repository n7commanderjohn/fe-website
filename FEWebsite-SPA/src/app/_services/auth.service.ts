import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { JwtHelperService } from '@auth0/angular-jwt';
import { BehaviorSubject } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from './../../environments/environment';
import { User } from './../_models/user';
import { RegisterUser } from './../_models/registerUser';
import { LoginResponse } from './../_models/loginResponse';
import { DecodedJWT } from '../_models/decodedJWT';
import { LoginCredentials } from '../_models/loginCredentials';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  dotNetAPIURL = environment.apiUrl + 'auth/';
  jwtHelper = new JwtHelperService();
  decodedToken: DecodedJWT;
  currentUser: User;
  photoUrl = new BehaviorSubject<string>('../../assets/defaultUser.png');
  currentPhotoUrl = this.photoUrl.asObservable();

  constructor(private http: HttpClient) { }

  changeUserPhoto(photoUrl: string) {
    this.photoUrl.next(photoUrl);
  }

  login(loginCredentials: LoginCredentials) {
    return this.http.post(this.dotNetAPIURL + 'login', loginCredentials)
      .pipe(
        map((loginResponse: LoginResponse) => {
          if (loginResponse) {
            localStorage.setItem('token', loginResponse.token);
            localStorage.setItem('user', JSON.stringify(loginResponse.user));
            this.decodedToken = this.jwtHelper.decodeToken(loginResponse.token);
            this.currentUser = loginResponse.user;
            this.changeUserPhoto(this.currentUser.photoUrl);
          }
        })
      );
  }

  register(model: RegisterUser) {
    return this.http.post(this.dotNetAPIURL + 'register', model);
  }

  loggedIn() {
    const token = localStorage.getItem('token');

    return !this.jwtHelper.isTokenExpired(token);
  }
}
