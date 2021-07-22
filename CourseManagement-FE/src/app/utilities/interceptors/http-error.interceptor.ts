import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpErrorResponse,
  HttpResponse
} from '@angular/common/http';
import { retry, catchError, subscribeOn, tap } from "rxjs/operators";
import { Observable, throwError } from 'rxjs';
import { IAlert } from 'src/app/shared/contracts/alert';
import { AlertService } from 'src/app/services/alert.service';
import { AccountService } from 'src/app/services/account.service';

@Injectable()
export class HttpErrorInterceptor implements HttpInterceptor {

  constructor(private readonly alertService: AlertService, private readonly acountService: AccountService) { }

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {

    return next.handle(request).pipe(
      tap((event: HttpEvent<any>) => {
        if (event instanceof HttpResponse) {
          // do stuff with response if you want
        }
      }, (err: any) => {
        let alert = <IAlert>{};
        if (err instanceof HttpErrorResponse) {
          if (err.status === 400) {
            alert.message = err.error.ErrorMessage;
            alert.type = 'danger';
          }
          if (err.status === 401) {
            alert.message = "Unauthorized action!";
            alert.type = 'danger';
          }
        }

        this.alertService.addAlert(alert);
      }));
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