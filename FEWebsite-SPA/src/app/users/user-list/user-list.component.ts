import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';

import { AlertifyService } from './../../_services/alertify.service';
import { UserService } from './../../_services/user.service';

import { StatusCodeResultReturnObject } from './../../_models/statusCodeResultReturnObject';
import { PageChanged } from './../../_models/pageChanged';
import { PaginatedResult, Pagination } from './../../_models/pagination';
import { User } from './../../_models/user';
import { UserParams } from './../../_models/userParams';
import { Gender } from './../../_models/gender';

@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.css']
})
export class UserListComponent implements OnInit {
  users: User[];
  user: User = JSON.parse(localStorage.getItem('user'));
  listOfGenders: Gender[];
  userParams: UserParams;
  pagination: Pagination;

  constructor(private route: ActivatedRoute,
              private userService: UserService,
              private alertify: AlertifyService) { }

  ngOnInit() {
    this.route.data.subscribe(data => {
      const users: PaginatedResult<User[]> = data.users;
      this.users = users.result;
      this.pagination = users.pagination;
    });

    this.getGenders();
    this.setUserParams();
  }

  private setUserParams() {
    let genderId: string;
    switch (this.user.genderId) {
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
      orderBy: 'lastActive'
    };
  }

  private getGenders() {
    this.userService.getGenders().subscribe(genders => {
      genders.unshift({
        id: '',
        description: 'All',
        name: undefined,
        selected: true
      });
      this.listOfGenders = genders;
    }, (error: StatusCodeResultReturnObject) => {
      this.alertify.error(error.response);
    });
  }

  resetFilters(filterForm: NgForm) {
    filterForm.resetForm(this.userParams);
    this.setUserParams();
    this.loadUsers();
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
    .subscribe((response) => {
      this.users = response.result;
      this.pagination = response.pagination;
    }, (error: StatusCodeResultReturnObject) => {
      this.alertify.error(error.response);
    });
  }
}
