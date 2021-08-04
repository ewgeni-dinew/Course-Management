import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable } from 'rxjs';
import { AuthService } from 'src/app/services/auth.service';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {

  constructor(private readonly authService: AuthService) { }

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {

    let jwt = this.authService.getLoggedUser?.accessToken;

    if (jwt) {
      const modified = request.clone({
        setHeaders: { 'Authorization': 'Bearer ' + jwt }
      });

      return next.handle(modified);
    }

    return next.handle(request);
  }
}
