import { Component, OnInit } from '@angular/core';
import { CourseService } from 'src/app/services/course.service';
import { Router } from '@angular/router';
import { AlertService } from 'src/app/services/alert.service';
import { AlertConsts } from 'src/app/utilities/constants/alerts';

@Component({
  selector: 'app-create',
  templateUrl: './create.component.html',
  styleUrls: ['./create.component.css']
})
export class CreateComponent implements OnInit {

  constructor(private readonly courseService: CourseService, private readonly router: Router, private readonly alertService: AlertService) { }

  ngOnInit(): void {
  }

  createHandler(data: JSON) {
    this.courseService.createCourse(data).then(() => {
      this.alertService.addAlertWithArgs(AlertConsts.CREATE_COURSE_SUCCESS, AlertConsts.TYPE_PRIMARY);
    }).then(() => {
      this.router.navigate(['/course/list']);
    });
  }
}
