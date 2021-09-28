import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { ICourse } from '../shared/contracts/course';
import { AuthService } from './auth.service';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CourseService {

  constructor(private readonly http: HttpClient, private readonly authService: AuthService) { }

  private selectedCourse: ICourse;

  public get getSelectedCourse(): ICourse {
    return this.selectedCourse;
  }

  public selectCourse(course: ICourse) {
    this.selectedCourse = course;
  }

  createCourse(data: JSON): Promise<Object> {

    data['authorId'] = this.authService.getLoggedUser.id;

    return this.http.post(environment.apiURL + 'course/create', data).toPromise();
  }

  deleteCourse(courseId: number): Promise<Object> {
    const data = {};
    data['id'] = courseId;

    return this.http.post(environment.apiURL + 'course/delete', <JSON>data).toPromise();
  }

  editCourse(data: JSON): Promise<Object> {
    return this.http.post(environment.apiURL + 'course/edit', data)
      .toPromise();
  }

  getAll(): Observable<ICourse[]> {
    return this.http.get<ICourse[]>(environment.apiURL + 'course/getAll');
  }

  getAllUserCourses(): Observable<ICourse[]> {
    return this.http.get<ICourse[]>(environment.apiURL + 'course/getAllUserCourses');
  }

  getFavoriteCourses(): ICourse[] {
    let courses: ICourse[] = [];

    this.http.get<ICourse[]>(environment.apiURL + 'course/getFavorites')
      .subscribe(res => {
        res.forEach(x => courses.push(x));
      });

    return courses;
  }

  addCourseToFavorites(courseId: number): Promise<Object> {
    const data = {};
    data['courseId'] = courseId;

    return this.http.post(environment.apiURL + 'course/addToFavorites', <JSON>data).toPromise();
  }

  removeCourseFromFavorites(courseId: number): Promise<Object> {
    const data = {};
    data['courseId'] = courseId;

    return this.http.post(environment.apiURL + 'course/removeFromFavorites', <JSON>data).toPromise();
  }

  getDetails(courseId: number): Promise<ICourse> {
    return this.http.get<ICourse>(environment.apiURL + 'course/details/' + courseId)
      .toPromise();
  }

  rateCourse(rating: number, courseId: number): Promise<ICourse> {
    const data = {};
    data['courseId'] = courseId;
    data['rating'] = rating;

    return this.http.post<ICourse>(environment.apiURL + 'course/rate', <JSON>data)
      .toPromise();
  }

  changeUserCourseState(courseId: number, state: number): Observable<Object> {
    const data = {};
    data['courseId'] = courseId;
    data['userCourseState'] = state
    data['userId'] = this.authService.getLoggedUser.id;

    return this.http.post(environment.apiURL + 'course/changeCourseState', <JSON>data);
  }

  downloadPDF(courseId: number): Observable<Blob> {
    const httpOptions = {
      responseType: 'blob' as 'json'
    };

    return this.http.get<any>(environment.apiURL + 'course/downloadPDF/' + courseId, httpOptions);
  }

  downloadWord(courseId: number): Observable<Blob> {
    const httpOptions = {
      responseType: 'blob' as 'json'
    };

    return this.http.get<any>(environment.apiURL + 'course/downloadWord/' + courseId, httpOptions);
  }
}
