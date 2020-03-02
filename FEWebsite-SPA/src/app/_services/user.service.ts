import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import { environment } from './../../environments/environment';

import { Gender } from '../_models/gender';
import { User } from '../_models/user';
import { UserParams } from './../_models/userParams';
import { UpdateResponse } from '../_models/updateResponse';
import { PaginatedResult } from './../_models/pagination';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  baseUrl = environment.apiUrl;
  user = 'user';
  photo = 'photo';
  genders = 'genders';
  like = 'like';

  constructor(private http: HttpClient) { }

  getUsers(page?: number, itemsPerPage?: number, userParams?: UserParams): Observable<PaginatedResult<User[]>> {
    const paginatedResult = new PaginatedResult<User[]>();

    let params = new HttpParams();

    if (page && itemsPerPage) {
      params = params.append('pageNumber', page.toString());
      params = params.append('pageSize', itemsPerPage.toString());
    }

    if (userParams) {
      params = params.append('minAge', userParams.minAge.toString());
      params = params.append('maxAge', userParams.maxAge.toString());
      params = params.append('genderId', userParams.genderId);
      params = params.append('orderBy', userParams.orderBy);
    }

    return this.http.get<User[]>(this.baseUrl + this.user, { observe: 'response', params })
      .pipe(
        map(response => {
          paginatedResult.result = response.body;
          const pagination = 'Pagination';
          if (response.headers.get(pagination)) {
            paginatedResult.pagination = JSON.parse(response.headers.get(pagination));
          }

          return paginatedResult;
        })
      );
  }

  getUser(id: number): Observable<User> {
    return this.http.get<User>(this.baseUrl + this.user + '/' + id);
  }

  getGenders(): Observable<Gender[]> {
    return this.http.get<Gender[]>(this.baseUrl + this.user + '/' +  this.genders);
  }

  updateUser(id: number, user: User): Observable<UpdateResponse> {
    return this.http.put<UpdateResponse>(this.baseUrl + this.user + '/' + id, user);
  }

  setMainPhoto(userId: number, photoId: number) {
    return this.http.put(this.baseUrl + this.user + '/' + userId + '/' + this.photo + '/' + photoId + '/setMain', null);
  }

  deletePhoto(userId: number, photoId: number) {
    return this.http.delete(this.baseUrl + this.user + '/' + userId + '/' + this.photo + '/' + photoId);
  }

  toggleLike(userId: number, recepientId: number) {
    return this.http.post(this.baseUrl + this.user + '/' + userId + '/' + this.like + '/' + recepientId, {});
  }

}
