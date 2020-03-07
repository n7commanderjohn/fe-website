import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, Resolve, Router } from '@angular/router';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Message } from './../_models/message';
import { MessageContainerArgs } from './../_models/messageParams';
import { AuthService } from './../_services/auth.service';
import { AlertifyService } from '../_services/alertify.service';
import { UserService } from '../_services/user.service';

@Injectable()
export class MessagesResolver implements Resolve<Message[]> {
    pageNumber = 1;
    pageSize = 6;
    messageContainer = MessageContainerArgs.Unread;

    constructor(private userService: UserService,
                private authService: AuthService,
                private router: Router,
                private alertify: AlertifyService) {}

    resolve(route: ActivatedRouteSnapshot): Observable<Message[]> {
        const userId = Number(this.authService.decodedToken.nameid);
        return this.userService.getUserMessages(userId, this.pageNumber, this.pageSize, this.messageContainer).pipe(
            catchError(error => {
                this.alertify.error('Error occured while trying to retrieve user messages.');
                this.router.navigate(['/home']);
                return of(null);
            })
        );
    }

}
