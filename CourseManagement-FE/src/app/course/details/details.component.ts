import { Component, OnInit, Input } from '@angular/core';
import { ICourse } from 'src/app/shared/contracts/course';
import { CourseService } from 'src/app/services/course.service';

@Component({
  selector: 'app-course-details',
  templateUrl: './details.component.html',
  styleUrls: ['./details.component.css']
})
export class DetailsComponent implements OnInit {

  constructor(private readonly courseService: CourseService) { }

  @Input()
  selectedCourse: ICourse;

  ngOnInit(): void {
    
  }
}
