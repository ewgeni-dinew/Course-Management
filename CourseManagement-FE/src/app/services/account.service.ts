import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from './../../environments/environment';
import { IUser } from '../shared/contracts/user';
import { Observable, Subscription } from 'rxjs';

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
    let headers = this.setAuthHeader();

    this.http.post<IUser>(environment.apiUrl + 'account/update', data, { headers: headers })
      .subscribe(user => {
        this.loggedUser.firstName = user.firstName;
        this.loggedUser.lastName = user.lastName;
      });
  }

  logout() {
    this.loggedUser = null;
  }

  getAll(): IUser[] {
    let headers = this.setAuthHeader();

    let users: IUser[] = [];

    this.http.get<IUser[]>(environment.apiUrl + 'account/getall', { headers: headers })
      .subscribe((res: IUser[]) => {
        res.forEach(x => users.push(x));
      });

    return users;
  }

  unblockAccount(accountId: number): Promise<Object> {
    let data = { id: accountId };

    let headers = this.setAuthHeader();

    return this.http.post(environment.apiUrl + 'account/unblock', data, { headers: headers })
      .toPromise();
  }

  blockAccount(accountId: number): Promise<Object> {
    let data = { id: accountId };

    let headers = this.setAuthHeader();

    return this.http.post(environment.apiUrl + 'account/block', data, { headers: headers })
      .toPromise();
  }

  deleteAccount(accountId: number): Promise<Object> {
    let data = { id: accountId };

    let headers = this.setAuthHeader();

    return this.http.post(environment.apiUrl + 'account/delete', data, { headers: headers })
      .toPromise();
  }

  private setAuthHeader(): HttpHeaders {
    return new HttpHeaders({
      'Authorization': 'Bearer ' + this.loggedUser?.token
    });
  }
}
