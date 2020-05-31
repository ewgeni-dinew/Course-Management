import { Component, OnInit } from '@angular/core';
import { CourseService } from 'src/app/services/course.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-create',
  templateUrl: './create.component.html',
  styleUrls: ['./create.component.css']
})
export class CreateComponent implements OnInit {

  constructor(private readonly courseService: CourseService, private router: Router) { }

  ngOnInit(): void {
  }

  createHandler(data: JSON) {
    let promise = new Promise((resolve, rejects) => {
      this.courseService.createCourse(data).then(() => {
        this.router.navigate(['/course/list']);
      });

      resolve();
    });
  }
}
