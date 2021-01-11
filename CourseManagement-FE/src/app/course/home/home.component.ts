import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ICourse } from 'src/app/shared/contracts/course';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  constructor(private readonly router: Router) { }

  private heading: string;
  private courseToRemove: ICourse;
  private selectedCourse: ICourse;
  private courseRating: number;

  public get getHeading(): string {
    return this.heading;
  }

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
    if (this.router.url === "/course/favorites") {
      this.heading = 'Favorite Courses';
    }
    else {
      this.heading = 'Courses';
    }
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

  changePageHeading(title: string) {
    if (title !== '') {
      this.heading = `${title} Courses`;
    }
  }
}
