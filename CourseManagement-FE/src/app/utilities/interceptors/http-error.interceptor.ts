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
import { HttpErrorService } from 'src/app/services/http-error.service';
import { IHttpError } from 'src/app/shared/contracts/http-error';

@Injectable()
export class HttpErrorInterceptor implements HttpInterceptor {

  constructor(private readonly errorService: HttpErrorService) { }

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {


    return next.handle(request)
      .pipe(
        retry(0),
        catchError((error: HttpErrorResponse) => {

          //TODO this should be refactored as it does not depend on the IHttpError interface
          //tried different approaches but did not succeed
          //currently works >>>
          let httpError: IHttpError = {
            errorMessage: error.error.ErrorMessage,
            errorCode: error.error.ErrorCode
          };
          //<<<

          let alert = <IAlert>{};

          //switch between error codes and modify alert types if needed
          if (error.status === 400) {

            alert.message = httpError.errorMessage;
            alert.type = 'danger';

            this.errorService.addAlert(alert);
          }

          return throwError(error);
        })
      );
  }
}