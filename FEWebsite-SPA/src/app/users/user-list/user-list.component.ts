import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';

import { AlertifyService } from './../../_services/alertify.service';
import { AuthService } from './../../_services/auth.service';
import { UserService } from './../../_services/user.service';

import { StatusCodeResultReturnObject } from './../../_models/statusCodeResultReturnObject';
import { PageChanged } from './../../_models/pageChanged';
import { PaginatedResult, Pagination } from './../../_models/pagination';
import { User } from './../../_models/user';
import { UserParams, UserParamsOptions } from './../../_models/userParams';
import { Gender } from './../../_models/gender';

@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.css']
})
export class UserListComponent implements OnInit {
  users: User[];
  loggedInUser: User = JSON.parse(localStorage.getItem('user'));
  loggedInUserId = Number(this.authService.decodedToken.nameid);
  listOfGenders: Gender[];
  userParams: UserParams;
  pagination: Pagination;
  upo = UserParamsOptions;

  constructor(private route: ActivatedRoute,
              private authService: AuthService,
              private userService: UserService,
              private alertify: AlertifyService) { }

  ngOnInit() {
    this.route.data.subscribe({
      next: data => {
        const users: PaginatedResult<User[]> = data.users;
        this.users = users.result;
        this.pagination = users.pagination;
      }
    });
    this.getUserLikes();

    this.getGenders();
    this.setUserParams();
  }

  isUserLiked(recepientId: number) {
    return this.loggedInUser.listOfLikees.includes(recepientId);
  }

  private getUserLikes() {
    this.userService.getLikes(this.loggedInUserId)
    .subscribe({
      next: likes => {
        this.loggedInUser.listOfLikees = likes;
      }
    });
  }

  private setUserParams() {
    let genderId: string;
    switch (this.loggedInUser.genderId) {
      case 'NA':
        genderId = ''; break;
      case 'F':
        genderId = 'M'; break;
      case 'M':
        genderId = 'F'; break;
      default:
        genderId = ''; break;
    }
    this.userParams = {
      genderId,
      minAge: 18,
      maxAge: 99,
      orderBy: this.upo.OrderBy.lastLogin,
      likeFilter: this.upo.LikeFilter.all
    };
  }

  private getGenders() {
    this.userService.getGenders().subscribe({
      next: genders => {
        genders.unshift({
          id: '',
          description: 'All',
          name: undefined,
          selected: true
        });
        this.listOfGenders = genders;
      },
      error: (error: StatusCodeResultReturnObject) => {
        this.alertify.error(error.response);
      }
    });
  }

  resetFilters(filterForm: NgForm) {
    filterForm.resetForm(this.userParams);
    this.setUserParams();
    this.loadUsers();
  }

  likeToggled() {
    const showingLikesOnly = this.userParams.likeFilter === this.upo.LikeFilter.likees;
    if (showingLikesOnly) {
      this.loadUsers();
    }
  }

  loadUsers(event?: PageChanged) {
    if (event) {
      if (event.page) {
        this.pagination.currentPage = event.page;
      }
      if (event.itemsPerPage) {
        this.pagination.itemsPerPage = event.itemsPerPage;
      }
    }
    this.userService.getUsers(this.pagination.currentPage, this.pagination.itemsPerPage, this.userParams)
    .subscribe({
      next: (response) => {
        this.users = response.result;
        this.pagination = response.pagination;
        this.getUserLikes();
      },
      error: (error: StatusCodeResultReturnObject) => {
        this.alertify.error(error.response);
      }
    });
  }
}
