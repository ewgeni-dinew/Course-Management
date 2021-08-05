import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpInterceptor,
  HttpErrorResponse,
} from '@angular/common/http';
import { catchError, switchMap } from "rxjs/operators";
import { Observable, throwError } from 'rxjs';
import { IAlert } from 'src/app/shared/contracts/alert';
import { AlertService } from 'src/app/services/alert.service';
import { AccountService } from 'src/app/services/account.service';
import { AuthService } from 'src/app/services/auth.service';

@Injectable()
export class HttpErrorInterceptor implements HttpInterceptor {

  constructor(private readonly alertService: AlertService, private readonly acountService: AccountService,
    private readonly authService: AuthService) { }

  handleResponseError(error: HttpErrorResponse, request?: HttpRequest<unknown>, next?: HttpHandler) {

    let alert = <IAlert>{};

    // handles the custom errors from the BE
    if (error.status === 400) {
      alert.message = error.error.ErrorMessage;
      alert.type = 'danger';
    }

    // handles 401 error code; JWT is *INVALID* or *EXPIRED*; 
    else if (error.status === 401) {
      return this.acountService.refreshToken() //call to refresh the JWT in case it is *EXPIRED*
        .pipe(
          switchMap(() => {

            request = this.updateAuthHeader(request); //updates the JWT Bearer inside the headers before the second call
            return next.handle(request);
          }),
          catchError(e => {
            if (e.status !== 401) {
              return this.handleResponseError(e);
            } else {
              // case JWT *INVALID*
              this.acountService.logout();
            }
          }));
    }

    // handles 403 error code; Unauthorized action
    else if (error.status === 403) {
      alert.message = "Unauthorized action!";
      alert.type = 'danger';
    }

    this.alertService.addAlert(alert);

    return throwError(error);
  }

  updateAuthHeader(request: HttpRequest<unknown>): HttpRequest<unknown> {
    return request.clone({
      setHeaders: { 'Authorization': 'Bearer ' + this.authService.getUserAccessToken }
    });
  }

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<any> {

    return next.handle(request)
      .pipe(catchError(error => {
        return this.handleResponseError(error, request, next);
      }));
  }
}