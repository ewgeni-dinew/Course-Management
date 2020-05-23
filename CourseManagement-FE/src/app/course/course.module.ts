import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CreateComponent } from './create/create.component';
import { EditComponent } from './edit/edit.component';
import { ListComponent } from './list/list.component';
import { DetailsComponent } from './details/details.component';
import { HomeComponent } from './home/home.component';
import { CourseRoutingModule } from './course-routing.module';
import { FormsModule } from '@angular/forms';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';



@NgModule({
  declarations: [CreateComponent, EditComponent, ListComponent, DetailsComponent, HomeComponent],
  imports: [
    CommonModule,
    CourseRoutingModule,
    FormsModule,
    NgbModule
  ]
})
export class CourseModule { }
