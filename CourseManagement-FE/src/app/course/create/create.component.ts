import { Component, OnInit } from '@angular/core';
import { CourseService } from 'src/app/services/course.service';

@Component({
  selector: 'app-create',
  templateUrl: './create.component.html',
  styleUrls: ['./create.component.css']
})
export class CreateComponent implements OnInit {

  constructor(private readonly courseService: CourseService) { }

  ngOnInit(): void {
  }

  createHandler(data: JSON) {
    this.courseService.createCourse(data);
  }
}
