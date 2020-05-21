import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from './../../environments/environment';
import { IUser } from '../shared/contracts/user';

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  constructor(private http: HttpClient) {

  }

  loggedUser: IUser;

  registerAccount(data: JSON) {
    this.http.post(environment.apiUrl + 'account/register', data).subscribe();
  }

  login(data: JSON) {
    this.http.post<IUser>(environment.apiUrl + 'account/login', data)
      .subscribe(user => this.loggedUser = user);
  }

  logout() {
    this.loggedUser = null;
  }
}
