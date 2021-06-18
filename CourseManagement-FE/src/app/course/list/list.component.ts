import { Component, OnInit, Output, EventEmitter, Input } from '@angular/core';
import { CourseService } from 'src/app/services/course.service';
import { ICourse } from 'src/app/shared/contracts/course';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';
import { Store } from '@ngrx/store';
import { State } from '../state/course.reducer';
import { selectCourse } from '../state/course.actions';

@Component({
  selector: 'app-course-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.css']
})
export class ListComponent implements OnInit {

  constructor(private readonly courseService: CourseService, private readonly router: Router,
    private readonly authService: AuthService, private readonly store: Store<State>) { }

  courses: ICourse[];

  selectedCourse: ICourse;

  public get isUserAdmin(): Boolean {
    return this.authService.isUserAdmin;
  }

  public get courseSummaryParagrahs(): string[] {
    return this.selectedCourse?.summary.split(/\r?\n/).filter(Boolean);
  }

  //to be removed
  // @Output('selectCourseEvent')
  // selectCourseEvent = new EventEmitter<ICourse>();

  removeCourseFromList(course: ICourse) {
    this.courses.forEach((item, index) => {
      if (item.id === course.id) this.courses.splice(index, 1);
    });
  }

  ngOnInit(): void {
    if (this.router.url === "/course/favorites") {
      this.courses = this.courseService.getFavoriteCourses();
    }
    else {
      this.courses = this.courseService.getAll();
    }
  }

  selectCourseHandler(inputCourse: ICourse) {
    if (inputCourse.id !== this.selectedCourse?.id) { //case 'Find out more' button

      this.courseService.getDetails(inputCourse.id).then((result) => {
        //this.selectCourseEvent.emit(result); to be removed
        this.selectedCourse = inputCourse;
        this.store.dispatch(selectCourse({ course: result }));
      });

    } else { //case 'Hide details' button
      //this.selectCourseEvent.emit(this.selectedCourse); //hides the details //to be removed
      
      this.selectedCourse = null;
      this.store.dispatch(selectCourse({ course: this.selectedCourse }));
    }
  }

  getCourseSummaryParagraphs(course: ICourse): string[] {
    return course.summary.split(/\r?\n/).filter(Boolean);
  }

  selectCourse() {
    this.store.dispatch({
      type: '[Course] Select course'
    });

    this.store.select('courses');
  }
}
