import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpErrorResponse, HTTP_INTERCEPTORS } from '@angular/common/http';
import { catchError } from 'rxjs/operators';
import { throwError } from 'rxjs';

@Injectable()
export class ErrorInteceptor implements HttpInterceptor {
  intercept(
    req: import('@angular/common/http/http').HttpRequest<any>,
    next: import('@angular/common/http/http').HttpHandler
  ): import('rxjs').Observable<import('@angular/common/http/http').HttpEvent<any>> {
    return next.handle(req).pipe(
        catchError(httpErrorResponse => {
            if (httpErrorResponse.status === 401) {
                return throwError(httpErrorResponse.statusText);
            }

            if (httpErrorResponse instanceof HttpErrorResponse) {
                const applicationError = httpErrorResponse.headers.get('Application-Error');
                if (applicationError) {
                    return throwError(applicationError);
                }

                const serverError = httpErrorResponse.error;
                const modalStateErrors: string[] = [];
                if (serverError.errors && typeof serverError.errors === 'object') {
                    for (const key in serverError.errors) {
                        if (serverError.errors[key]) {
                            modalStateErrors.push(serverError.errors[key]);
                        }
                    }
                }
                const errorsString = modalStateErrors.toString().replace(',', '\n');

                return throwError(errorsString || serverError || 'Unknown Server Error');
            }
        })
    );
  }
}

export const ErrorInteceptorProvider = {
    provide: HTTP_INTERCEPTORS,
    useClass: ErrorInteceptor,
    multi: true,
};
