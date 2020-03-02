import { Component, OnInit, Input } from '@angular/core';

import { UserService } from './../../_services/user.service';
import { AuthService } from './../../_services/auth.service';
import { AlertifyService } from './../../_services/alertify.service';

import { User } from './../../_models/user';
import { StatusCodeResultReturnObject } from './../../_models/statusCodeResultReturnObject';

@Component({
  selector: 'app-user-card',
  templateUrl: './user-card.component.html',
  styleUrls: ['./user-card.component.css']
})
export class UserCardComponent implements OnInit {
  @Input() user: User;
  currentUserId = Number(this.authService.decodedToken.nameid);


  constructor(private authService: AuthService,
              private userService: UserService,
              private alertify: AlertifyService) { }

  ngOnInit() {
  }

  toggleLike(recepientId: number) {
    this.userService.toggleLike(this.currentUserId, recepientId)
    .subscribe((next: StatusCodeResultReturnObject) => {
      this.alertify.success(next.response);
    }, (error: StatusCodeResultReturnObject) => {
      this.alertify.error(error.response);
    });
  }

}
