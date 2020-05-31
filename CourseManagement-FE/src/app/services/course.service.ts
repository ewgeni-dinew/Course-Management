import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { IUser } from '../shared/contracts/user';
import { environment } from 'src/environments/environment';
import { ICourse } from '../shared/contracts/course';
import { Observable } from "rxjs";
import { map, switchMap } from 'rxjs/operators';


@Injectable({
  providedIn: 'root'
})
export class CourseService {

  constructor(private readonly http: HttpClient) { }

  private selectedCourse: ICourse;

  test: ICourse;

  public get getSelectedCourse(): ICourse {
    return this.selectedCourse;
  }

  public selectCourse(course: ICourse) {
    this.selectedCourse = course;
  }

  public get getLoggedUser(): IUser {
    return JSON.parse(localStorage.getItem('loggedUser'));
  }

  async createCourse(data: JSON): Promise<void> {
    let headers = this.setAuthHeader();

    data['authorId'] = this.getLoggedUser.id;

    await this.http.post(environment.apiUrl + 'course/create', data, { headers: headers }).toPromise();
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

  getFavoriteCourses(): ICourse[] {
    let headers = this.setAuthHeader();

    let courses: ICourse[] = [];

    this.http.get<ICourse[]>(environment.apiUrl + 'course/getfavorites', { headers: headers })
      .subscribe(res => {
        res.forEach(x => courses.push(x));
      });

    return courses;
  }

  async addCourseToFavorites(courseId: number): Promise<void> {
    let headers = this.setAuthHeader();

    const data = {};
    data['courseId'] = courseId;

    await this.http.post(environment.apiUrl + 'course/addToFavorites', <JSON>data, { headers: headers }).toPromise();
  }

  async removeCourseFromFavorites(courseId: number): Promise<void> {
    let headers = this.setAuthHeader();

    const data = {};
    data['courseId'] = courseId;

    await this.http.post(environment.apiUrl + 'course/removeFromFavorites', <JSON>data, { headers: headers }).toPromise();    
  }

  getDetails(courseId: number): Promise<ICourse> {
    let headers = this.setAuthHeader();

    return this.http.get<ICourse>(environment.apiUrl + 'course/details/' + courseId, { headers: headers })
      .toPromise();
  }

  private setAuthHeader(): HttpHeaders {
    return new HttpHeaders({
      'Authorization': 'Bearer ' + this.getLoggedUser?.token
    });
  }
}
