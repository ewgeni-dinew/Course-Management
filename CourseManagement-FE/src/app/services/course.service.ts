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


  public get isUserAdmin(): Boolean {
    return this.getLoggedUser.role.toLowerCase() === "admin";
  }


  async createCourse(data: JSON): Promise<void> {
    let headers = this.setAuthHeader();

    data['authorId'] = this.getLoggedUser.id;

    await this.http.post(environment.apiUrl + 'course/create', data, { headers: headers }).toPromise();
  }

  async deleteCourse(courseId: number): Promise<void> {
    let headers = this.setAuthHeader();

    const data = {};
    data['id'] = courseId;

    await this.http.post(environment.apiUrl + 'course/delete', <JSON>data, { headers: headers }).toPromise();
  }

  async editCourse(data: JSON): Promise<void> {
    let headers = this.setAuthHeader();

    await this.http.post(environment.apiUrl + 'course/edit', data, { headers: headers })
      .toPromise();
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

  async getDetails(courseId: number): Promise<ICourse> {
    let headers = this.setAuthHeader();

    return await this.http.get<ICourse>(environment.apiUrl + 'course/details/' + courseId, { headers: headers })
      .toPromise();
  }

  async rateCourse(rating: number, courseId: number): Promise<ICourse> {
    let headers = this.setAuthHeader();

    const data = {};
    data['courseId'] = courseId;
    data['rating'] = rating;

    return await this.http.post<ICourse>(environment.apiUrl + 'course/rate', <JSON>data, { headers: headers })
      .toPromise();
  }

  private setAuthHeader(): HttpHeaders {
    return new HttpHeaders({
      'Authorization': 'Bearer ' + this.getLoggedUser?.token
    });
  }
}
