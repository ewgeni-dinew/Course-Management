import { Injectable } from '@angular/core';
import { Location } from '@angular/common';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { AuthService } from 'src/app/services/auth.service';

@Injectable({
  providedIn: 'root'
})
export class NegateAuthGuard implements CanActivate {

  constructor(private readonly authService: AuthService, private _location: Location) {
  }

  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {

    if (this.authService.getLoggedUser) {

      this._location.back();

      return false;
    }

    return true;
  }
}
