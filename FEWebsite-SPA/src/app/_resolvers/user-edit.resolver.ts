import { Injectable } from '@angular/core';
import { Resolve, Router, ActivatedRouteSnapshot } from '@angular/router';

import { User } from '../_models/user';

import { AuthService } from './../_services/auth.service';
import { AlertifyService } from '../_services/alertify.service';
import { UserService } from '../_services/user.service';

import { catchError } from 'rxjs/operators';
import { Observable, of } from 'rxjs';

@Injectable()
export class UserEditResolver implements Resolve<User> {
    constructor(private userService: UserService,
                private authService: AuthService,
                private router: Router,
                private alertify: AlertifyService) {}

    resolve(route: ActivatedRouteSnapshot): Observable<User> {
        const currentUserId: number = Number(this.authService.decodedToken.nameid);
        return this.userService.getUser(currentUserId).pipe(
            catchError(error => {
                this.alertify.error('Error occured while trying to retrieve your editable user data.');
                this.router.navigate(['/users']);
                return of(null);
            })
        );
    }

}
