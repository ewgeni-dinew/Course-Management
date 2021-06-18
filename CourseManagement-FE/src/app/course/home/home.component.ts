import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { select, Store } from '@ngrx/store';
import { ICourse } from 'src/app/shared/contracts/course';
import { State } from '../state/course.reducer';
import { getSelectedCourse } from '../state/course.selectors';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  selectedCourse$ = this.store.pipe(select(getSelectedCourse));

  constructor(private readonly router: Router, private readonly store: Store<State>) { }

  private heading: string;
  private courseToRemove: ICourse;
  private selectedCourse: ICourse;

  public get getHeading(): string {
    return this.heading;
  }

  public get getCourseToRemove(): ICourse {
    return this.courseToRemove;
  }

  public get getSelectedCourse(): ICourse {
    return this.selectedCourse;
  }

  ngOnInit(): void {
    if (this.router.url === "/course/favorites") {
      this.heading = 'Favorite Courses';
    }
    else {
      this.heading = 'Courses';
    }
  }

  setSelectedEvent(course: ICourse) {
    this.selectedCourse = course;
  }

  resetCoursesOnPage(course: ICourse) {
    this.courseToRemove = course;
  }

  changePageHeading(title: string) {
    if (title !== '') {
      this.heading = `${title} Courses`;
    }
  }
}
