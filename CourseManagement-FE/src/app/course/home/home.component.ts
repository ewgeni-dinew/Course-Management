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

  public get getCourseToRemove(): ICourse {
    return this.courseToRemove;
  }

  private selectedCourse: ICourse;

  public get getSelectedCourse(): ICourse {
    return this.selectedCourse;
  }


  ngOnInit(): void {
  }

  setSelectedEvent(course: ICourse) {

    this.selectedCourse = course;
  }

  resetCoursesOnPage(course: ICourse) {
    this.courseToRemove = course;
  }
}
