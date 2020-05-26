import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { CourseService } from 'src/app/services/course.service';
import { ICourse } from 'src/app/shared/contracts/course';

@Component({
  selector: 'app-course-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.css']
})
export class ListComponent implements OnInit {

  constructor(private courseService: CourseService) { }

  courses: ICourse[];

  selectedCourse: ICourse;

  @Output('selectCourseEvent')
  selectCourseEvent = new EventEmitter<ICourse>();

  ngOnInit(): void {
    this.courses = this.courseService.getAll();
  }

  selectCourseHandler(inputCourse: ICourse) {

    if (inputCourse.id !== this.selectedCourse?.id) {

      let promise = new Promise((resolve, reject) => {
        this.courseService.getDetails(inputCourse.id).then((result) => {
          this.selectCourseEvent.emit(result);
          this.selectedCourse = inputCourse;

          resolve();
        });
      })

    } else {
      this.selectedCourse = null;
      this.selectCourseEvent.emit(this.selectedCourse); //hides the details
    }
  }
}
