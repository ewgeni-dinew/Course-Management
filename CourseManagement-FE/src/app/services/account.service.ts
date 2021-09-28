import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from './../../environments/environment';
import { IUser } from '../shared/contracts/user';
import { Router } from '@angular/router';
import { map } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { SignalRService } from './signal-r.service';

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  constructor(private readonly http: HttpClient, private readonly router: Router, private readonly signalrService: SignalRService) { }

  registerAccount(data: JSON): Promise<Object> {
    return this.http.post(environment.apiURL + 'account/register', data).toPromise();
  }

  async loginUser(data: JSON): Promise<void> {
    const res = await this.http.post<IUser>(environment.apiURL + 'account/login', data).toPromise();
    localStorage.setItem('loggedUser', JSON.stringify(res));

    this.signalrService.connect();
  }

  updateAccount(data: JSON) {
    let user: IUser;

    this.http.post<IUser>(environment.apiURL + 'account/update', data)
      .subscribe((res) => {
        user = JSON.parse(localStorage.getItem('loggedUser'));
        user.firstName = res.firstName;
        user.lastName = res.lastName;

        localStorage.setItem('loggedUser', JSON.stringify(user));
      });
  }

  changeUserPassword(data: JSON): Promise<Object> {
    return this.http.post(environment.apiURL + 'account/changePassword', data)
      .toPromise();
  }

  logout() {
    const user: IUser = JSON.parse(localStorage.getItem('loggedUser'));

    const data = {
      userId: user.id,
      refreshToken: user.refreshToken
    };

    this.http.post(environment.apiURL + 'account/revokeToken', data)
      .subscribe(() => {
        localStorage.removeItem('loggedUser');
        this.router.navigate(['/']);

      }, (error) => {
        localStorage.removeItem('loggedUser');
        this.router.navigate(['/']);
      });

      this.signalrService.disconnect();
  }

  refreshToken() {
    const user: IUser = JSON.parse(localStorage.getItem('loggedUser'));

    if (user) {
      const data = {
        userId: user.id,
        refreshToken: user.refreshToken
      };

      return this.http.post<IUser>(environment.apiURL + 'account/refreshToken', data)
        .pipe(map((res: IUser) => {
          user.accessToken = res.accessToken;
          user.refreshToken = res.refreshToken;
          localStorage.setItem('loggedUser', JSON.stringify(user));
        }));
    }
  }

  getAll(): IUser[] {
    let users: IUser[] = [];

    this.http.get<IUser[]>(environment.apiURL + 'account/getAll')
      .subscribe((res: IUser[]) => {
        res.forEach(x => users.push(x));
      });

    return users;
  }

  getAllContributors(): Observable<IUser[]> {
    return this.http.get<IUser[]>(environment.apiURL + 'account/getContributors');
  }

  unblockAccount(accountId: number): Promise<Object> {
    let data = { id: accountId };

    return this.http.post(environment.apiURL + 'account/unblock', data)
      .toPromise();
  }

  blockAccount(accountId: number): Promise<Object> {
    let data = { id: accountId };

    return this.http.post(environment.apiURL + 'account/block', data)
      .toPromise();
  }

  deleteAccount(accountId: number): Promise<Object> {
    let data = { id: accountId };

    return this.http.post(environment.apiURL + 'account/delete', data)
      .toPromise();
  }

  setGeoLocation(lng: number, lat: number) {
    const user: IUser = JSON.parse(localStorage.getItem('loggedUser'));

    let data = {};
    data['geoLng'] = lng;
    data['geoLat'] = lat;
    data['userId'] = user.id;

    this.http.post<IUser>(environment.apiURL + 'account/setGeoLocation', data).subscribe((res) => {
      user.geoLat = res.geoLat;
      user.geoLng = res.geoLng;
      localStorage.setItem('loggedUser', JSON.stringify(user));
    });
  }
}
