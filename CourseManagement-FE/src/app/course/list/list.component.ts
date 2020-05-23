import { Component, OnInit } from '@angular/core';
import { CourseService } from 'src/app/services/course.service';
import { ICourse } from 'src/app/shared/contracts/course';

@Component({
  selector: 'app-course-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.css']
})
export class ListComponent implements OnInit {

  constructor(private courseService: CourseService) { }

  courses: ICourse[];

  ngOnInit(): void {
    this.courses = this.courseService.getAll();
  }
  
}
