import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import { environment } from './../../environments/environment';

import { Gender } from '../_models/gender';
import { User } from '../_models/user';
import { UpdateResponse } from '../_models/updateResponse';
import { PaginatedResult } from './../_models/pagination';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getUsers(page?: number, itemsPerPage?: number): Observable<PaginatedResult<User[]>> {
    const paginatedResult = new PaginatedResult<User[]>();

    let params = new HttpParams();

    if (page != null && itemsPerPage != null) {
      params = params.append('pageNumber', page.toString());
      params = params.append('pageSize', itemsPerPage.toString());
    }

    return this.http.get<User[]>(this.baseUrl + 'users', { observe: 'response', params })
      .pipe(
        map(response => {
          paginatedResult.result = response.body;
          const pagination = 'Pagination';
          if (response.headers.get(pagination) != null) {
            paginatedResult.pagination = JSON.parse(response.headers.get(pagination));
          }

          return paginatedResult;
        })
      );
  }

  getUser(id: number): Observable<User> {
    return this.http.get<User>(this.baseUrl + 'users/' + id);
  }

  getGenders(): Observable<Gender[]> {
    return this.http.get<Gender[]>(this.baseUrl + 'users/genders');
  }

  updateUser(id: number, user: User): Observable<UpdateResponse> {
    return this.http.put<UpdateResponse>(this.baseUrl + 'users/' + id, user);
  }

  setMainPhoto(userId: number, photoId: number) {
    return this.http.put(this.baseUrl + 'users/' + userId + '/photos/' + photoId + '/setMain', null);
  }

  deletePhoto(userId: number, photoId: number) {
    return this.http.delete(this.baseUrl + 'users/' + userId + '/photos/' + photoId);
  }

}
