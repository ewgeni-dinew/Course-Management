import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { ICourse } from 'src/app/shared/contracts/course';
import { CourseService } from 'src/app/services/course.service';
import { Router } from '@angular/router';
import { AlertService } from 'src/app/services/alert.service';
import { AlertConsts } from 'src/app/utilities/constants/alerts';

@Component({
  selector: 'app-course-details',
  templateUrl: './details.component.html',
  styleUrls: ['./details.component.css']
})
export class DetailsComponent implements OnInit {

  constructor(private readonly courseService: CourseService, private readonly router: Router, private readonly aletService: AlertService) { }

  @Input()
  selectedCourse: ICourse;

  @Output()
  removeCourseEvent = new EventEmitter<ICourse>();

  @Input()
  inputRating: number;

  ngOnInit(): void {
  }

  addToFavoritesHandler(courseId: number) {
    this.courseService.addCourseToFavorites(courseId).then(() => {
      this.aletService.addAlertWithArgs(AlertConsts.ADD_FAV_COURSE_SUCCESS, AlertConsts.TYPE_INFO);
    }).then(() => {
      this.router.navigate(['/course/favorites']);
    });
  }

  removeFromFavoritesHandler(course: ICourse) {
    this.courseService.removeCourseFromFavorites(course.id).then(() => {
      this.selectedCourse = null;
      this.removeCourseEvent.emit(course);
    }).then(() => {
      this.aletService.addAlertWithArgs(AlertConsts.REMOVE_FAV_COURSE_SUCCESS, AlertConsts.TYPE_WARNING);
    });
  }

  rateCourseHandler() {
    this.courseService.rateCourse(this.selectedCourse.rating, this.selectedCourse.id).then((res) => {
      this.selectedCourse.rating = res.rating;
      this.inputRating = res.rating;
    }).then(()=>{
      this.aletService.addAlertWithArgs(AlertConsts.REMOVE_FAV_COURSE_SUCCESS, AlertConsts.TYPE_INFO);
    });
  }

  isPageFavorites(): Boolean {
    return this.router.url === "/course/favorites";
  }
}
