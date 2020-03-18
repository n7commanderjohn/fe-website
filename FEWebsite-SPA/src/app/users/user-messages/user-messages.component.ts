import { Component, DoCheck, Input, OnInit, } from '@angular/core';
import { MessageToSend } from './../../_models/messageToSend';
import { Message } from './../../_models/message';
import { StatusCodeResultReturnObject } from './../../_models/statusCodeResultReturnObject';
import { AlertifyService } from './../../_services/alertify.service';
import { AuthService } from './../../_services/auth.service';
import { UserService } from './../../_services/user.service';
import { tap } from 'rxjs/operators';

@Component({
  selector: 'app-user-messages',
  templateUrl: './user-messages.component.html',
  styleUrls: ['./user-messages.component.css']
})
export class UserMessagesComponent implements OnInit, DoCheck {
  @Input() recipientId: number;
  @Input() recipientName: string;
  currentUserId: number;
  messages: Message[];
  newMessage: MessageToSend;
  private chatbox: HTMLElement;
  private messageSent: boolean;
  private firstLoad = true;
  readonly defaultUserPic = '../../../assets/defaultUser.png';

  constructor(
    private userService: UserService,
    private authService: AuthService,
    private alertify: AlertifyService
  ) {}

  ngOnInit() {
    this.currentUserId = Number(this.authService.decodedToken.nameid);
    this.loadMessages();
    this.newMessage = {
      recipientId: this.recipientId,
      content: ''
    };
    this.chatbox = document.getElementById('chatbox');
  }

  ngDoCheck() {
    if (this.messageSent) {
      this.scrollToBottomOfChatbox();
    } else if (this.firstLoad) {
      setTimeout(() => {
        this.scrollToBottomOfChatbox('auto');
        this.firstLoad = false;
      }, 200);
    }
  }

  loadMessages() {
    return this.userService
      .getMessageThread(this.currentUserId, this.recipientId)
      .pipe(
        tap(messages => {
          messages.forEach(message => {
            if (!message.isRead && message.recipientId === this.currentUserId) {
              this.userService.markAsRead(this.currentUserId, message.id);
            }
          });
        })
      )
      .subscribe({
        next: response => {
          this.messages = response.reverse();
        },
        error: (error) => {
          this.alertify.error('There was an issue with retrieving the message thread.');
        },
      });
  }

  sendMessage() {
    this.userService.sendMessage(this.currentUserId, this.newMessage)
      .subscribe({
        next: (message) => {
          this.messages.push(message);
          this.messageSent = true;
        },
        error: (error: StatusCodeResultReturnObject) => {
          this.alertify.error(error.response);
        },
        complete: () => {
          // this.alertify.success('message sent');
          this.newMessage.content = '';
        }
      });
  }

  private scrollToBottomOfChatbox(behavior: ScrollBehavior = 'smooth') {
    setTimeout(() => {
      this.chatbox.scrollTo({
        behavior,
        top: this.chatbox.scrollHeight
      });
      this.messageSent = false;
    }, 100);
  }
}
