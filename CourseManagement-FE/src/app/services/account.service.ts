import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from './../../environments/environment';
import { IUser } from '../shared/contracts/user';

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  constructor(private readonly http: HttpClient) { }

  public get getLoggedUser(): IUser {
    return JSON.parse(localStorage.getItem('loggedUser'));
  }

  async registerAccount(data: JSON): Promise<void> {
    await this.http.post(environment.apiUrl + 'account/register', data).toPromise();
  }

  async login(data: JSON): Promise<void> {
    const user = await this.http.post<IUser>(environment.apiUrl + 'account/login', data).toPromise();
    localStorage.setItem('loggedUser', JSON.stringify(user));
  }

  updateAccount(data: JSON) {
    let headers = this.setAuthHeader();
    let user: IUser;

    this.http.post<IUser>(environment.apiUrl + 'account/update', data, { headers: headers })
      .subscribe(res => {
        user = JSON.parse(localStorage.getItem('loggedUser'));
        user.firstName = res.firstName;
        user.lastName = res.lastName;

        localStorage.setItem('loggedUser', JSON.stringify(user));
      });
  }

  logout() {
    localStorage.removeItem('loggedUser');
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
      'Authorization': 'Bearer ' + this.getLoggedUser?.token
    });
  }
}
