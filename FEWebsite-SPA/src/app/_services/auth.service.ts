import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  dotNetAPIURL = 'http://localhost:5000/api/auth/';

  constructor(private http: HttpClient) { }

  login(loginCredentials: any) {
    return this.http.post(this.dotNetAPIURL + 'login', loginCredentials)
      .pipe(
        map((response: any) => {
          const user = response;
          if (user) {
            localStorage.setItem('token', user.token);
          }
        })
      );
  }

  register(model: any) {
    return this.http.post(this.dotNetAPIURL + 'register', model);
  }
}
