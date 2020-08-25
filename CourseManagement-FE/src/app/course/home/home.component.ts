import { Component, OnInit } from '@angular/core';
import { ICourse } from 'src/app/shared/contracts/course';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  constructor() { }


  private courseToRemove: ICourse;
  private selectedCourse: ICourse;
  private courseRating: number;

  public get getCourseToRemove(): ICourse {
    return this.courseToRemove;
  }

  public get getSelectedCourse(): ICourse {
    return this.selectedCourse;
  }

  public get getCourseRating(): number {
    return this.courseRating;
  }

  ngOnInit(): void {
  }

  setSelectedEvent(course: ICourse) {

    if (course) {
      this.courseRating = course.rating;
    }

    this.selectedCourse = course;
  }

  resetCoursesOnPage(course: ICourse) {
    this.courseToRemove = course;
  }
}
