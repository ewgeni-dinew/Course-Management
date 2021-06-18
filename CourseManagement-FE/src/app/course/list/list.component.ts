import { Component, Input, OnInit } from '@angular/core';
import { CourseService } from 'src/app/services/course.service';
import { ICourse } from 'src/app/shared/contracts/course';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';
import { Store } from '@ngrx/store';
import { State } from '../state/course.reducer';
import { selectCourse, selectCourseRating, selectCourseShowDetails } from '../state/course.actions';

@Component({
  selector: 'app-course-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.css']
})
export class ListComponent implements OnInit {

  constructor(private readonly courseService: CourseService, private readonly router: Router,
    private readonly authService: AuthService, private readonly store: Store<State>) { }

  @Input()
  showCourseDetails: boolean; //comes from store; shows if the course details should be displayed

  @Input()
  clickedCourse: ICourse; //comes from store; shows the course that is currently clicked for details

  courses: ICourse[];

  public get isUserAdmin(): Boolean {
    return this.authService.isUserAdmin;
  }

  ngOnInit(): void {
    if (this.router.url === "/course/favorites") this.courses = this.courseService.getFavoriteCourses();
    else this.courses = this.courseService.getAll();
  }

  removeCourseFromList(course: ICourse) {
    this.courses.forEach((item, index) => {
      if (item.id === course.id) this.courses.splice(index, 1);
    });
  }

  selectCourseHandler(inputCourse: ICourse) {
    if (!this.showCourseDetails || (this.showCourseDetails && inputCourse.id !== this.clickedCourse?.id)) { //case 'Find out more' button clicked
      this.courseService.getDetails(inputCourse.id).then((result) => {
        this.store.dispatch(selectCourse({ course: result }));
        this.store.dispatch(selectCourseRating({ rating: result.rating }));
        this.store.dispatch(selectCourseShowDetails({ flag: true }));
      });

    } else { //case 'Hide details' button clicked
      this.store.dispatch(selectCourseShowDetails({ flag: false }));
    }
  }

  getCourseSummaryParagraphs(course: ICourse): string[] {
    return course.summary.split(/\r?\n/).filter(Boolean);
  }
}
