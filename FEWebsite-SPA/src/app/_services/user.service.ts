import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { Gender } from './../_models/gender';
import { Message } from './../_models/message';
import { MessageContainerArgs } from './../_models/messageParams';
import { PaginatedResult } from './../_models/pagination';
import { UpdateResponse } from './../_models/updateResponse';
import { User } from './../_models/user';
import { UserParams, UserParamsOptions as UPO } from './../_models/userParams';
import { environment } from './../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  baseUrl = environment.apiUrl;
  user = 'user';
  photo = 'photo';
  genders = 'genders';
  like = 'like';
  message = 'message';

  constructor(private http: HttpClient) { }

  getUsers(page?: number, itemsPerPage?: number, userParams?: UserParams): Observable<PaginatedResult<User[]>> {
    const paginatedResult = new PaginatedResult<User[]>();

    let params = new HttpParams();

    params = this.AddPageAndItemsPerPageParams(page, itemsPerPage, params);

    if (userParams) {
      params = params.append('minAge', userParams.minAge.toString());
      params = params.append('maxAge', userParams.maxAge.toString());
      params = params.append('genderId', userParams.genderId);
      params = params.append('orderBy', userParams.orderBy);
      if (userParams.likeFilter === UPO.LikeFilter.likees) {
        params = params.append('likees', 'true');
      }
      if (userParams.likeFilter === UPO.LikeFilter.likers) {
        params = params.append('likers', 'true');
      }
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

  getLikes(userId: number): Observable<number[]> {
    return this.http.get<number[]>(this.baseUrl + this.user + '/' + userId + '/' + this.like);
  }

  getUserMessages(userId: number, page?: number, itemsPerPage?: number, messageArgs?: number | string )
    : Observable<PaginatedResult<Message[]>> {
    const paginatedResult = new PaginatedResult<Message[]>();

    let params = new HttpParams();

    params = params.append('MessageContainer', messageArgs.toString());
    params = this.AddPageAndItemsPerPageParams(page, itemsPerPage, params);

    return this.http.get<Message[]>(this.baseUrl + this.user + '/' + userId + '/' + this.message, { observe: 'response', params })
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

  private AddPageAndItemsPerPageParams(page: number, itemsPerPage: number, params: HttpParams): HttpParams {
    if (page && itemsPerPage) {
      params = params.append('pageNumber', page.toString());
      params = params.append('pageSize', itemsPerPage.toString());
    }
    return params;
  }
}
