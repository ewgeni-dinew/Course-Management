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

  constructor(private courseService: CourseService, private router: ActivatedRoute) { }

  ngOnInit(): void {
    const id = parseInt(this.router.snapshot.paramMap.get('id'));

    let promise = new Promise((resolve, reject) => {
      this.courseService.getDetails(id).then((res)=>{        
        this.selectedCourse = res;
        
        resolve();
      });

    });
  }

  editHandler(data: JSON) {

  }
}
