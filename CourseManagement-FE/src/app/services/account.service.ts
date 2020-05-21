import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from './../../environments/environment';
import { IUser } from '../shared/contracts/user';

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  constructor(private http: HttpClient) {
    this.loggedUser = null;
  }

  loggedUser: IUser;

  registerAccount(data: JSON) {
    this.http.post(environment.apiUrl + 'account/register', data).subscribe();
  }

  login(data: JSON) {
    this.http.post<IUser>(environment.apiUrl + 'account/login', data)
      .subscribe(user => {
        this.loggedUser = user;
      });
  }

  updateAccount(data: JSON) {
    this.http.post<IUser>(environment.apiUrl + 'account/update', data)
      .subscribe(user => {
        this.loggedUser.firstName = user.firstName;
        this.loggedUser.lastName = user.lastName;
      });
  }

  logout() {
    this.loggedUser = null;
  }
}
