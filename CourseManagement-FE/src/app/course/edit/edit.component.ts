import { Component, OnInit } from '@angular/core';
import { ICourse } from 'src/app/shared/contracts/course';
import { CourseService } from 'src/app/services/course.service';
import { Router, Route, ActivatedRoute } from '@angular/router';

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

  constructor(private courseService: CourseService, private route: ActivatedRoute, private router: Router) { }

  ngOnInit(): void {
    const id = parseInt(this.route.snapshot.paramMap.get('id'));

    let promise = new Promise((resolve, reject) => {
      this.courseService.getDetails(id).then((res) => {
        this.selectedCourse = res;

        resolve();
      });

    });
  }

  editCourseHandler(data: JSON) {
    if (confirm("Are you sure to edit this course?")) {
      data['id'] = this.course.id;

      let promise = new Promise((resolve, reject) => {
        this.courseService.editCourse(data).then(() => {
          this.router.navigate(['course/list']);
        })
      });
    }
  }

  deleteCourseHandler(courseId: number) {
    if (confirm("Are you sure to delete this course?")) {
      let promise = new Promise((resolve, reject) => {
        this.courseService.deleteCourse(courseId).then(() => {
          this.router.navigate(['course/list']);
        })
      });
    }
  }
}
