import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from './../../environments/environment';
import { IUser } from '../shared/contracts/user';

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  constructor(private readonly http: HttpClient) { }

  registerAccount(data: JSON): Promise<Object> {
    return this.http.post(environment.apiUrl + 'account/register', data).toPromise();
  }

  async loginUser(data: JSON): Promise<void> {
    const res = await this.http.post<IUser>(environment.apiUrl + 'account/login', data).toPromise();
    localStorage.setItem('loggedUser', JSON.stringify(res));
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
    localStorage.removeItem('loggedUser');
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
}
