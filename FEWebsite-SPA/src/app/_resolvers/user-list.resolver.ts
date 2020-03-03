import { Injectable } from '@angular/core';
import { Resolve, Router, ActivatedRouteSnapshot } from '@angular/router';

import { User } from '../_models/user';

import { AlertifyService } from '../_services/alertify.service';
import { UserService } from '../_services/user.service';

import { catchError } from 'rxjs/operators';
import { Observable, of } from 'rxjs';

@Injectable()
export class UserListResolver implements Resolve<User[]> {
    pageNumber = 1;
    pageSize = 6;
    showLikesOnly = true;

    constructor(private userService: UserService,
                private router: Router,
                private alertify: AlertifyService) {}

    resolve(route: ActivatedRouteSnapshot): Observable<User[]> {
        return this.userService.getUsers(this.pageNumber, this.pageSize, null).pipe(
            catchError(error => {
                this.alertify.error('Error occured while trying to retrieve user data.');
                this.router.navigate(['/home']);
                return of(null);
            })
        );
    }

}
