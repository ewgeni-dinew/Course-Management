import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { select, Store } from '@ngrx/store';
import { Observable } from 'rxjs';
import { ICourse } from 'src/app/shared/contracts/course';
import { State } from '../state/course.reducer';
import { getCourseRating, getCourseShowDetails, getSelectedCourse, getSelectedFavCourse } from '../state/course.selectors';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  selectedCourse$: Observable<ICourse>;
  courseRating$ = this.store.pipe(select(getCourseRating));
  showCourseDetails$ = this.store.pipe(select(getCourseShowDetails));

  constructor(private readonly router: Router, private readonly store: Store<State>) { }

  private heading: string;

  public get getHeading(): string {
    return this.heading;
  }

  ngOnInit(): void {
    if (this.router.url === "/course/favorites") {
      this.heading = 'Favorite Courses';
      this.selectedCourse$ = this.store.pipe(select(getSelectedFavCourse));
    }
    else {
      this.heading = 'Courses';
      this.selectedCourse$ = this.store.pipe(select(getSelectedCourse));
    }
  }
}
