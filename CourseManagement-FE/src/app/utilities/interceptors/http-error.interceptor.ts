import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpErrorResponse
} from '@angular/common/http';
import { retry, catchError } from "rxjs/operators";
import { Observable, throwError } from 'rxjs';
import { IAlert } from 'src/app/shared/contracts/alert';
import { AlertService } from 'src/app/services/alert.service';
import { IHttpError } from 'src/app/shared/contracts/http-error';
import { AccountService } from 'src/app/services/account.service';

@Injectable()
export class HttpErrorInterceptor implements HttpInterceptor {

  constructor(private readonly alertService: AlertService, private readonly acountService: AccountService) { }

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {


    return next.handle(request)
      .pipe(
        retry(0),
        catchError((error: HttpErrorResponse) => {          

          let alert = <IAlert>{};

          //switch between error codes and modify alert types if needed
          if (error.status === 400) {
            alert.message = error.error.ErrorMessage;
            alert.type = 'danger';
          } 
          else if (error.status === 401) {
            alert.message = "Unauthorized action!";
            alert.type = 'danger';

            this.acountService.logout();
          }

          this.alertService.addAlert(alert);

          return throwError(error);
        })
      );
  }
}