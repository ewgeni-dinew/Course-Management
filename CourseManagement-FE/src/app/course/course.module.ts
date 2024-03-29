import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CreateComponent } from './create/create.component';
import { EditComponent } from './edit/edit.component';
import { ListComponent } from './list/list.component';
import { DetailsComponent } from './details/details.component';
import { HomeComponent } from './home/home.component';
import { CourseRoutingModule } from './course-routing.module';
import { FormsModule } from '@angular/forms';
import { NgbAccordionModule, NgbRatingModule } from '@ng-bootstrap/ng-bootstrap';
import { StoreModule } from '@ngrx/store';
import { courseReducer } from './state/course.reducer';
import { BoardComponent } from './board/board.component';
import { DragDropModule } from '@angular/cdk/drag-drop';
import { MyCoursesComponent } from './my-courses/my-courses.component';

@NgModule({
  declarations: [
    CreateComponent,
    EditComponent,
    ListComponent,
    DetailsComponent,
    HomeComponent,
    BoardComponent,
    MyCoursesComponent
  ],
  imports: [
    CommonModule,
    CourseRoutingModule,
    FormsModule,
    NgbAccordionModule,
    NgbRatingModule,
    DragDropModule,
    StoreModule.forFeature('courses', courseReducer),
  ]
})
export class CourseModule { }
