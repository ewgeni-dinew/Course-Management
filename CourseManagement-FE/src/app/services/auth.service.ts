import { Injectable } from '@angular/core';
import { IUser } from '../shared/contracts/user';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor() { }

  public get getLoggedUser(): IUser {
    return JSON.parse(localStorage.getItem('loggedUser'));
  }

  public get isUserAdmin(): Boolean {
    return this.getLoggedUser.role.toLowerCase() === "admin";
  }

  public get getUserAccessToken(): String {
    return this.getLoggedUser?.accessToken;
  }
}
