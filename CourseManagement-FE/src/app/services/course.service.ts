import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { IUser } from '../shared/contracts/user';
import { environment } from 'src/environments/environment';
import { ICourse } from '../shared/contracts/course';

@Injectable({
  providedIn: 'root'
})
export class CourseService {

  constructor(private readonly http: HttpClient) { }

  public get getLoggedUser(): IUser {
    return JSON.parse(localStorage.getItem('loggedUser'));
  }

  createCourse(data: JSON) {
    let headers = this.setAuthHeader();

    data['authorId'] = this.getLoggedUser.id;

    this.http.post(environment.apiUrl + 'course/create', data, { headers: headers })
      .subscribe();
  }

  deleteCourse(data: JSON) {
    let headers = this.setAuthHeader();

    this.http.post(environment.apiUrl + 'course/delete', data, { headers: headers })
      .subscribe();
  }

  editCourse(data: JSON) {
    let headers = this.setAuthHeader();

    this.http.post(environment.apiUrl + 'course/edit', data, { headers: headers })
      .subscribe();

    //TODO to call course/details endpoint
  }

  getAll(): ICourse[] {
    let headers = this.setAuthHeader();

    let courses: ICourse[] = [];

    this.http.get<ICourse[]>(environment.apiUrl + 'course/getall', { headers: headers })
      .subscribe(res => {
        res.forEach(x => courses.push(x));
      });

    return courses;
  }

  private setAuthHeader(): HttpHeaders {
    return new HttpHeaders({
      'Authorization': 'Bearer ' + this.getLoggedUser?.token
    });
  }
}
