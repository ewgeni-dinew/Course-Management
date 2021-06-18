import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { ICourse } from 'src/app/shared/contracts/course';
import { CourseService } from 'src/app/services/course.service';
import { Router } from '@angular/router';
import { AlertService } from 'src/app/services/alert.service';
import { AlertConsts } from 'src/app/utilities/constants/alerts';
import { getCourseShowDetails, getSelectedCourse } from '../state/course.selectors';
import { select, Store } from '@ngrx/store';
import { State } from '../state/course.reducer';
import { selectCourse, selectCourseRating } from '../state/course.actions';

@Component({
  selector: 'app-course-details',
  templateUrl: './details.component.html',
  styleUrls: ['./details.component.css']
})
export class DetailsComponent implements OnInit {

  constructor(private readonly courseService: CourseService, private readonly router: Router,
    private readonly alertService: AlertService, private readonly store: Store<State>) { }

  public get courseContentParagrahs(): string[] {
    return this.selectedCourse.content.split(/\r?\n/).filter(Boolean);
  }

  showCourseDetails$ = this.store.pipe(select(getCourseShowDetails));
  
  @Input()
  selectedCourse: ICourse;

  @Input()
  courseRating: number; //used for the initial rating

  @Input()
  inputRating: number; //used for the updated(input) value

  @Output()
  removeCourseEvent = new EventEmitter<ICourse>();

  ngOnInit(): void {
  }

  addToFavoritesHandler(courseId: number) {
    this.courseService.addCourseToFavorites(courseId).then(() => {
      this.alertService.addAlertWithArgs(AlertConsts.ADD_FAV_COURSE_SUCCESS, AlertConsts.TYPE_INFO);
    }).then(() => {
      this.router.navigate(['/course/favorites']);
    });
  }

  removeFromFavoritesHandler(course: ICourse) {
    this.courseService.removeCourseFromFavorites(course.id).then(() => {
      this.selectedCourse = null;
      this.removeCourseEvent.emit(course);
    }).then(() => {
      this.alertService.addAlertWithArgs(AlertConsts.REMOVE_FAV_COURSE_SUCCESS, AlertConsts.TYPE_WARNING);
    });
  }

  rateCourseHandler() {
    this.courseService.rateCourse(this.inputRating, this.selectedCourse.id).then((res) => {
      this.store.dispatch(selectCourseRating({ rating: res.rating })); //dispatch event to update the rating from the store      
    }).then(() => {
      this.alertService.addAlertWithArgs(AlertConsts.RATE_COURSE_SUCCESS, AlertConsts.TYPE_INFO);
    });
  }

  isPageFavorites(): Boolean {
    return this.router.url === "/course/favorites";
  }

  downloadPDF() {
    this.courseService.downloadPDF(this.selectedCourse.id)
      .subscribe(data => {

        var downloadURL = window.URL.createObjectURL(data);
        var link = document.createElement('a');
        link.href = downloadURL;
        link.download = this.selectedCourse.title + ".pdf";
        link.click();
      });
  }

  downloadWord() {
    this.courseService.downloadWord(this.selectedCourse.id)
      .subscribe(data => {

        var downloadURL = window.URL.createObjectURL(data);
        var link = document.createElement('a');
        link.href = downloadURL;
        link.download = this.selectedCourse.title + ".doc";
        link.click();
      });
  }
}
