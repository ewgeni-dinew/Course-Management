import { Component, Input, OnInit } from '@angular/core';
import { CourseService } from 'src/app/services/course.service';
import { ICourse } from 'src/app/shared/contracts/course';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';
import { Store } from '@ngrx/store';
import { State } from '../state/course.reducer';
import { deselectCourse, selectCourse, selectFavCourse } from '../state/course.actions';
import { CourseViewer, SignalRService } from 'src/app/services/signal-r.service';

@Component({
  selector: 'app-course-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.css']
})
export class ListComponent implements OnInit {

  constructor(private readonly courseService: CourseService, private readonly router: Router,
    private readonly authService: AuthService, private readonly store: Store<State>, private readonly signalrService: SignalRService) { }

  @Input()
  clickedCourse: ICourse; //comes from store; shows the course that is currently clicked for details

  @Input()
  courseId: number; //comes from the store; used for the course summary 'card'

  courses: ICourse[];

  public get isUserAdmin(): Boolean {
    return this.authService.isUserAdmin;
  }

  ngOnInit(): void {
    if (this.router.url === "/course/favorites") this.courses = this.courseService.getFavoriteCourses();
    else this.courseService.getAll().subscribe(res => this.courses = res);
  }

  removeCourseFromList(course: ICourse) {
    this.courses.forEach((item, index) => {
      if (item.id === course.id) this.courses.splice(index, 1);
    });
  }

  selectCourseHandler(inputCourse: ICourse) {

    if (inputCourse.id !== this.clickedCourse?.id) {
      //case 'Find out more' button clicked   

      this.courseService.getDetails(inputCourse.id).then((result) => {
        if (this.isPageFavoriteCourse()) {
          this.store.dispatch(selectFavCourse({ course: result })) //displatch event for selected course: for favorite
        } else {
          this.store.dispatch(selectCourse({ course: result })); //displatch event for selected course: for normal
        }
      });

    } else {
      //case 'Hide details' button clicked

      this.store.dispatch(deselectCourse({ isFavCourse: this.isPageFavoriteCourse() }));

      let username = this.authService.getLoggedUser.username;

      //call WS to remove current user with CourseID
      this.signalrService.removeViewer(new CourseViewer(this.clickedCourse?.id, username));
    }
  }

  getCourseSummaryParagraphs(course: ICourse): string[] {
    return course.summary.split(/\r?\n/).filter(Boolean);
  }

  isPageFavoriteCourse(): boolean {
    if (this.router.url === "/course/favorites") return true;
    else return false;
  }
}
