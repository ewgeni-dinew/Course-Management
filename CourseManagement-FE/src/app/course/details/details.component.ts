import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { ICourse } from 'src/app/shared/contracts/course';
import { CourseService } from 'src/app/services/course.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-course-details',
  templateUrl: './details.component.html',
  styleUrls: ['./details.component.css']
})
export class DetailsComponent implements OnInit {

  constructor(private readonly courseService: CourseService, private router: Router) { }

  @Input()
  selectedCourse: ICourse;

  @Output()
  removeCourseEvent = new EventEmitter<ICourse>();

  @Input()
  inputRating: number;
  
  ngOnInit(): void {
  }

  addToFavoritesHandler(courseId: number) {
    let promise = new Promise((resolve, reject) => {
      this.courseService.addCourseToFavorites(courseId).then(() => {
        this.router.navigate(['/course/favorites']);
      });

      resolve();
    });
  }

  removeFromFavoritesHandler(course: ICourse) {
    let promise = new Promise((resolve, reject) => {
      this.courseService.removeCourseFromFavorites(course.id).then(() => {
        this.selectedCourse = null;
        this.removeCourseEvent.emit(course);
      });

      resolve();
    });
  }

  rateCourseHandler() {
    console.log(this.selectedCourse.rating);
    
    //call BE

    //this.selectedCourse = BE response;
  }

  isPageFavorites(): Boolean {
    return this.router.url === "/course/favorites";
  }
}
