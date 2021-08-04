import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpErrorResponse,
  HttpResponse
} from '@angular/common/http';
import { retry, catchError, subscribeOn, tap, switchMap } from "rxjs/operators";
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

    if (error.status === 400) {
      alert.message = error.error.ErrorMessage;
      alert.type = 'danger';
    }

    // Invalid token error
    else if (error.status === 401) {
      return this.acountService.refreshToken().pipe(
        switchMap(() => {

          request = this.updateAuthHeader(request);
          return next.handle(request);
        }),
        catchError(e => {
          if (e.status !== 401) {
            return this.handleResponseError(e);
          } else {
            this.acountService.logout();
          }
        }));
    }

    else if (error.status === 403) {
      alert.message = "Unauthorized action!";
      alert.type = 'danger';
    }

    this.alertService.addAlert(alert);

    return throwError(error);
  }

  updateAuthHeader(request: HttpRequest<unknown>): HttpRequest<unknown> {
    return request.clone({
      setHeaders: { 'Authorization': 'Bearer ' + this.authService.getLoggedUser?.accessToken }
    });
  }

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<any> {

    return next.handle(request).pipe(catchError(error => {
      return this.handleResponseError(error, request, next);
    }));

    // tap((event: HttpEvent<any>) => {
    //   if (event instanceof HttpResponse) {
    //     // do stuff with response if you want
    //   }
    // }, (err: any) => {
    //   let alert = <IAlert>{};
    //   if (err instanceof HttpErrorResponse) {
    //     if (err.status === 400) {
    //       alert.message = err.error.ErrorMessage;
    //       alert.type = 'danger';
    //     }
    //     else if (err.status === 401) {
    //       this.acountService.refreshToken();

    //       next.handle(request).pipe(tap(() => { }, (err: any) => {
    //         this.acountService.logout();
    //         console.log("Error from inner")
    //       }))
    //       //alert.message = "401!";
    //       //alert.type = 'danger';
    //     }
    //     else if (err.status === 403) {
    //       alert.message = "Unauthorized action!";
    //       alert.type = 'danger';
    //     }
    //   }

    //   this.alertService.addAlert(alert);
    // }));


    // .pipe(
    //   retry(0),
    //   catchError((error: HttpErrorResponse, caught) => {


    //     let alert = <IAlert>{};

    //     //switch between error codes and modify alert types if needed
    //     if (error.status === 400) {
    //       alert.message = error.error.ErrorMessage;
    //       alert.type = 'danger';
    //     }
    //     else if (error.status === 401) {
    //       alert.message = "Unauthorized action!";
    //       alert.type = 'danger';

    //       //TODO decide if user should be logged out; as he can only be unauthorized
    //       //this.acountService.logout();
    //     }

    //     this.alertService.addAlert(alert);

    //     return throwError(error);
    //   })
    // );
  }
}