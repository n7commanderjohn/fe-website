import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Message } from './../_models/message';
import { MessageContainerArgs } from './../_models/messageParams';
import { PageChanged } from './../_models/pageChanged';
import { PaginatedResult, Pagination } from './../_models/pagination';
import { StatusCodeResultReturnObject } from './../_models/statusCodeResultReturnObject';
import { AlertifyService } from './../_services/alertify.service';
import { AuthService } from './../_services/auth.service';
import { UserService } from './../_services/user.service';

@Component({
  selector: 'app-messages',
  templateUrl: './messages.component.html',
  styleUrls: ['./messages.component.css']
})
export class MessagesComponent implements OnInit {
  messages: Message[];
  pagination: Pagination;
  mca = MessageContainerArgs;
  messageContainer: string;

  private readonly successMsg = 'Messages loaded successfully.';

  constructor(private userService: UserService,
              private authService: AuthService,
              private route: ActivatedRoute,
              private alertify: AlertifyService) { }

  ngOnInit() {
    this.route.data.subscribe({
      next: data => {
        const messages: PaginatedResult<Message[]> = data.messages;
        this.messages = messages.result;
        this.pagination = messages.pagination;
        this.messageContainer = this.mca.Unread;
        this.alertify.success(this.successMsg);
      },
    });
  }

  loadMessages(event?: PageChanged) {
    if (event) {
      if (event.page) {
        this.pagination.currentPage = event.page;
      }
      if (event.itemsPerPage) {
        this.pagination.itemsPerPage = event.itemsPerPage;
      }
    }
    const userId = Number(this.authService.decodedToken.nameid);
    return this.userService.getUserMessages(
      userId, this.pagination.currentPage, this.pagination.itemsPerPage, this.messageContainer)
      .subscribe({
        next: (response) => {
          this.messages = response.result;
          this.pagination = response.pagination;
        },
        error: (error: StatusCodeResultReturnObject) => {
          this.alertify.error(error.response);
        },
        complete: () => {
          this.alertify.success(this.successMsg);
        }
      });
}

}
