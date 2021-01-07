import { Component, OnInit, Output, EventEmitter, Input } from '@angular/core';
import { CourseService } from 'src/app/services/course.service';
import { ICourse } from 'src/app/shared/contracts/course';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-course-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.css']
})
export class ListComponent implements OnInit {

  constructor(private courseService: CourseService, private router: Router, private readonly authService: AuthService) { }

  courses: ICourse[];

  selectedCourse: ICourse;


  public get isUserAdmin(): Boolean {
    return this.authService.isUserAdmin;
  }


  @Output('selectCourseEvent')
  selectCourseEvent = new EventEmitter<ICourse>();

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

      let promise = new Promise<void>((resolve, reject) => {
        this.courseService.getDetails(inputCourse.id).then((result) => {
          this.selectCourseEvent.emit(result);
          this.selectedCourse = inputCourse;
        });

        resolve();
      })

    } else { //case 'Hide details' button
      this.selectedCourse = null;
      this.selectCourseEvent.emit(this.selectedCourse); //hides the details
    }
  }
}
