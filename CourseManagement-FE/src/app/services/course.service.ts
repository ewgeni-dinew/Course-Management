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

    return this.http.post(environment.apiUrl + 'course/create', data).toPromise();
  }

  deleteCourse(courseId: number): Promise<Object> {
    const data = {};
    data['id'] = courseId;

    return this.http.post(environment.apiUrl + 'course/delete', <JSON>data).toPromise();
  }

  editCourse(data: JSON): Promise<Object> {
    return this.http.post(environment.apiUrl + 'course/edit', data)
      .toPromise();
  }

  getAll(): Observable<ICourse[]> {
    return this.http.get<ICourse[]>(environment.apiUrl + 'course/getall');
  }

  getAllUserCourses(): Observable<ICourse[]> {
    return this.http.get<ICourse[]>(environment.apiUrl + 'course/getallusercourses');
  }

  getFavoriteCourses(): ICourse[] {
    let courses: ICourse[] = [];

    this.http.get<ICourse[]>(environment.apiUrl + 'course/getfavorites')
      .subscribe(res => {
        res.forEach(x => courses.push(x));
      });

    return courses;
  }

  addCourseToFavorites(courseId: number): Promise<Object> {
    const data = {};
    data['courseId'] = courseId;

    return this.http.post(environment.apiUrl + 'course/addToFavorites', <JSON>data).toPromise();
  }

  removeCourseFromFavorites(courseId: number): Promise<Object> {
    const data = {};
    data['courseId'] = courseId;

    return this.http.post(environment.apiUrl + 'course/removeFromFavorites', <JSON>data).toPromise();
  }

  getDetails(courseId: number): Promise<ICourse> {
    return this.http.get<ICourse>(environment.apiUrl + 'course/details/' + courseId)
      .toPromise();
  }

  rateCourse(rating: number, courseId: number): Promise<ICourse> {
    const data = {};
    data['courseId'] = courseId;
    data['rating'] = rating;

    return this.http.post<ICourse>(environment.apiUrl + 'course/rate', <JSON>data)
      .toPromise();
  }

  changeUserCourseState(courseId: number, state: number): Observable<Object> {
    const data = {};
    data['courseId'] = courseId;
    data['userCourseState'] = state
    data['userId'] = this.authService.getLoggedUser.id;

    return this.http.post(environment.apiUrl + 'course/ChangeCourseState', <JSON>data);
  }

  downloadPDF(courseId: number): Observable<Blob> {
    const httpOptions = {
      responseType: 'blob' as 'json'
    };

    return this.http.get<any>(environment.apiUrl + 'course/downloadPDF/' + courseId, httpOptions);
  }

  downloadWord(courseId: number): Observable<Blob> {
    const httpOptions = {
      responseType: 'blob' as 'json'
    };

    return this.http.get<any>(environment.apiUrl + 'course/downloadWord/' + courseId, httpOptions);
  }
}
