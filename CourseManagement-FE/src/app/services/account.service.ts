import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from './../../environments/environment';
import { IUser } from '../shared/contracts/user';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  constructor(private readonly http: HttpClient, private readonly router: Router) { }

  registerAccount(data: JSON): Promise<Object> {
    return this.http.post(environment.apiUrl + 'account/register', data).toPromise();
  }

  async loginUser(data: JSON): Promise<void> {
    const res = await this.http.post<IUser>(environment.apiUrl + 'account/login', data).toPromise();
    localStorage.setItem('loggedUser', JSON.stringify(res));

    let date = new Date();
    date.setTime(date.getTime() + (7 * 24 * 60 * 60 * 1000))
    document.cookie = "refreshToken=" + res.refreshToken + "; expires=" + date.toUTCString() + "; path=/";
  }

  updateAccount(data: JSON) {
    let user: IUser;

    this.http.post<IUser>(environment.apiUrl + 'account/update', data)
      .subscribe((res) => {
        user = JSON.parse(localStorage.getItem('loggedUser'));
        user.firstName = res.firstName;
        user.lastName = res.lastName;

        localStorage.setItem('loggedUser', JSON.stringify(user));
      });
  }

  changeUserPassword(data: JSON): Promise<Object> {
    return this.http.post(environment.apiUrl + 'account/changepassword', data)
      .toPromise();
  }

  logout() {

    const data = {
      refreshToken: this.getCookieRefreshToken()
    };

    this.http.post(environment.apiUrl + 'account/revoketoken', data)
      .subscribe(() => {
        localStorage.removeItem('loggedUser');
        this.router.navigate(['/']);

      }, (error) => {
        localStorage.removeItem('loggedUser');
        this.router.navigate(['/']);
      });
  }

  refreshToken() {
    const user: IUser = JSON.parse(localStorage.getItem('loggedUser'));

    const data = {
      refreshToken: this.getCookieRefreshToken()
    };

    this.http.post<String>(environment.apiUrl + 'account/refreshtoken/' + user.id, data)
      .subscribe((res) => {
        user.token = res;
        localStorage.setItem('loggedUser', JSON.stringify(user));
      }, (error) => {
        console.log('error');
      });
  }

  getAll(): IUser[] {
    let users: IUser[] = [];

    this.http.get<IUser[]>(environment.apiUrl + 'account/getall')
      .subscribe((res: IUser[]) => {
        res.forEach(x => users.push(x));
      });

    return users;
  }

  unblockAccount(accountId: number): Promise<Object> {
    let data = { id: accountId };

    return this.http.post(environment.apiUrl + 'account/unblock', data)
      .toPromise();
  }

  blockAccount(accountId: number): Promise<Object> {
    let data = { id: accountId };

    return this.http.post(environment.apiUrl + 'account/block', data)
      .toPromise();
  }

  deleteAccount(accountId: number): Promise<Object> {
    let data = { id: accountId };

    return this.http.post(environment.apiUrl + 'account/delete', data)
      .toPromise();
  }

  private getCookieRefreshToken(): String {

    const value = `; ${document.cookie}`;
    const parts = value.split(`; refreshToken=`);

    if (parts.length === 2) {
      return parts.pop().split(';').shift();
    } else {
      return '';
    }
  }
}
