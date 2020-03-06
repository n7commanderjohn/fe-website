import { Component, Input, OnInit } from '@angular/core';
import { AlertifyService } from './../../_services/alertify.service';
import { AuthService } from './../../_services/auth.service';
import { Message } from '../../_models/message';
import { StatusCodeResultReturnObject } from '../../_models/statusCodeResultReturnObject';
import { UserService } from '../../_services/user.service';

@Component({
  selector: 'app-user-messages',
  templateUrl: './user-messages.component.html',
  styleUrls: ['./user-messages.component.css']
})
export class UserMessagesComponent implements OnInit {
  @Input() recipientId: number;
  @Input() recipientName: string;
  messages: Message[];

  constructor(
    private userService: UserService,
    private authService: AuthService,
    private alertify: AlertifyService
  ) {}

  ngOnInit() {
    this.loadMessages();
  }

  loadMessages() {
    const userId = Number(this.authService.decodedToken.nameid);
    return this.userService
      .getMessageThread(userId, this.recipientId)
      .subscribe(
        response => {
          this.messages = response.reverse();
        },
        (error) => {
          this.alertify.error('There was an issue with retrieving the message thread.');
        }
      );
  }
}
