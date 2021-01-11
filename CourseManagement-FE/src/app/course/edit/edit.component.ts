import { Component, OnInit } from '@angular/core';
import { ICourse } from 'src/app/shared/contracts/course';
import { CourseService } from 'src/app/services/course.service';
import { Router, Route, ActivatedRoute } from '@angular/router';
import { AlertService } from 'src/app/services/alert.service';
import { AlertConsts } from 'src/app/utilities/constants/alerts';

@Component({
  selector: 'app-edit',
  templateUrl: './edit.component.html',
  styleUrls: ['./edit.component.css']
})
export class EditComponent implements OnInit {

  private selectedCourse: ICourse;

  public get course(): ICourse {
    return this.selectedCourse;
  }

  constructor(
    private readonly courseService: CourseService,
    private readonly route: ActivatedRoute,
    private readonly router: Router,
    private readonly alertService: AlertService) { }

  ngOnInit(): void {
    const id = parseInt(this.route.snapshot.paramMap.get('id'));

    this.courseService.getDetails(id).then((res) => {
      this.selectedCourse = res;
    });
  }

  editCourseHandler(data: JSON) {
    if (confirm("Are you sure to edit this course?")) {
      data['id'] = this.course.id;

      this.courseService.editCourse(data).then(() => {
        this.alertService.addAlertWithArgs(AlertConsts.EDIT_COURSE_SUCCESS, AlertConsts.TYPE_SUCCESS);
      }).then(() => {
        this.router.navigate(['course/list']);
      })
    }
  }

  deleteCourseHandler(courseId: number) {
    if (confirm("Are you sure to delete this course?")) {

      this.courseService.deleteCourse(courseId).then(() => {
        this.alertService.addAlertWithArgs(AlertConsts.DELETE_COURSE_SUCCESS, AlertConsts.TYPE_WARNING);
      }).then(() => {
        this.router.navigate(['course/list']);
      })
    }
  }
}
