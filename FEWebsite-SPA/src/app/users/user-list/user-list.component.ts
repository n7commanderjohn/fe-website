import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { AlertifyService } from './../../_services/alertify.service';
import { UserService } from './../../_services/user.service';

import { StatusCodeResultReturnObject } from './../../_models/statusCodeResultReturnObject';
import { PageChanged } from './../../_models/pageChanged';
import { PaginatedResult, Pagination } from './../../_models/pagination';
import { User } from '../../_models/user';

@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.css']
})
export class UserListComponent implements OnInit {
  users: User[];
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
  }

  loadUsers(event: PageChanged) {
    if (event.page != null) {
      this.pagination.currentPage = event.page;
    }
    if (event.itemsPerPage != null) {
      this.pagination.itemsPerPage = event.itemsPerPage;
    }
    this.userService.getUsers(this.pagination.currentPage, this.pagination.itemsPerPage)
    .subscribe((response) => {
      this.users = response.result;
      this.pagination = response.pagination;
    }, (error: StatusCodeResultReturnObject) => {
      this.alertify.error(error.response);
    });
  }
}
