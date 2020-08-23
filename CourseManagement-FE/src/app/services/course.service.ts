import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { ICourse } from '../shared/contracts/course';
import { AuthService } from './auth.service';


@Injectable({
  providedIn: 'root'
})
export class CourseService {

  constructor(private readonly http: HttpClient, private readonly authService: AuthService) { }

  private selectedCourse: ICourse;

  test: ICourse;

  public get getSelectedCourse(): ICourse {
    return this.selectedCourse;
  }

  public selectCourse(course: ICourse) {
    this.selectedCourse = course;
  }

  async createCourse(data: JSON): Promise<void> {

    data['authorId'] = this.authService.getLoggedUser.id;

    await this.http.post(environment.apiUrl + 'course/create', data).toPromise();
  }

  async deleteCourse(courseId: number): Promise<void> {

    const data = {};
    data['id'] = courseId;

    await this.http.post(environment.apiUrl + 'course/delete', <JSON>data).toPromise();
  }

  async editCourse(data: JSON): Promise<void> {

    await this.http.post(environment.apiUrl + 'course/edit', data)
      .toPromise();
  }

  getAll(): ICourse[] {

    let courses: ICourse[] = [];

    this.http.get<ICourse[]>(environment.apiUrl + 'course/getall')
      .subscribe(res => {
        res.forEach(x => courses.push(x));
      });

    return courses;
  }

  getFavoriteCourses(): ICourse[] {

    let courses: ICourse[] = [];

    this.http.get<ICourse[]>(environment.apiUrl + 'course/getfavorites')
      .subscribe(res => {
        res.forEach(x => courses.push(x));
      });

    return courses;
  }

  async addCourseToFavorites(courseId: number): Promise<void> {

    const data = {};
    data['courseId'] = courseId;

    await this.http.post(environment.apiUrl + 'course/addToFavorites', <JSON>data).toPromise();
  }

  async removeCourseFromFavorites(courseId: number): Promise<void> {

    const data = {};
    data['courseId'] = courseId;

    await this.http.post(environment.apiUrl + 'course/removeFromFavorites', <JSON>data).toPromise();
  }

  async getDetails(courseId: number): Promise<ICourse> {

    return await this.http.get<ICourse>(environment.apiUrl + 'course/details/' + courseId)
      .toPromise();
  }

  async rateCourse(rating: number, courseId: number): Promise<ICourse> {

    const data = {};
    data['courseId'] = courseId;
    data['rating'] = rating;

    return await this.http.post<ICourse>(environment.apiUrl + 'course/rate', <JSON>data)
      .toPromise();
  }
}
