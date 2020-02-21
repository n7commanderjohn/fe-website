import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { JwtHelperService } from '@auth0/angular-jwt';
import { BehaviorSubject } from 'rxjs';
import { map } from 'rxjs/operators';

import { environment } from './../../environments/environment';

import { User } from './../_models/user';
import { DecodedJWT } from '../_models/decodedJWT';
import { LoginCredentials } from './../_models/loginCredentials';
import { LoginResponse } from './../_models/loginResponse';
import { UpdateResponse } from '../_models/updateResponse';

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
  registerMode = false;
  pwResetMode = false;

  constructor(private http: HttpClient) { }

  changeUserPhoto(photoUrl: string) {
    this.photoUrl.next(photoUrl);
  }

  login(user: User | LoginCredentials) {
    return this.http.post<LoginResponse>(this.dotNetAPIURL + 'login', user)
      .pipe(
        map((loginResponse) => this.updateTokenAndUserDetails(loginResponse))
      );
  }

  updateTokenAndUserDetails(response: LoginResponse | UpdateResponse) {
    if (response) {
      localStorage.setItem('token', response.token);
      this.decodedToken = this.jwtHelper.decodeToken(response.token);

      if (this.isLoginResponse(response)) {
        const loginResponse = response as LoginResponse;
        localStorage.setItem('user', JSON.stringify(loginResponse.user));
        this.currentUser = loginResponse.user;
        this.changeUserPhoto(this.currentUser.photoUrl);
      }
    }

  }

  private isLoginResponse(response: LoginResponse | UpdateResponse) {
    return (response as LoginResponse).user !== undefined;
  }

  enterRegisterMode() {
    this.registerMode = true;
    this.pwResetMode = false;
  }

  enterPWResetMode() {
    this.registerMode = false;
    this.pwResetMode = true;
  }

  logout() {
    localStorage.removeItem('token');
    localStorage.removeItem('user');
    this.decodedToken = null;
    this.currentUser = null;
  }

  register(user: User) {
    return this.http.post(this.dotNetAPIURL + 'register', user);
  }

  resetPassword(user: User) {
    return this.http.put(this.dotNetAPIURL + 'resetpassword', user);
  }

  loggedIn() {
    const token = localStorage.getItem('token');

    return !this.jwtHelper.isTokenExpired(token);
  }
}
