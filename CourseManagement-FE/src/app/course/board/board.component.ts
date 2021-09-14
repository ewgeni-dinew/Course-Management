import { CdkDragDrop, moveItemInArray, transferArrayItem } from '@angular/cdk/drag-drop';
import { AfterViewInit, Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { CourseService } from 'src/app/services/course.service';
import { ICourse } from 'src/app/shared/contracts/course';

@Component({
  selector: 'app-board',
  templateUrl: './board.component.html',
  styleUrls: ['./board.component.css']
})
export class BoardComponent implements OnInit, AfterViewInit {

  constructor(private readonly courseService: CourseService) { }

  ngAfterViewInit(): void {
    this.courseService.getAllUserCourses()
    .subscribe(res => {
      this.allUserCourses = res;

      this.todoCourses = this.allUserCourses.filter(x => x.state === 2).map(x => x.title);

      this.inProgCourses = this.allUserCourses.filter(x => x.state === 3).map(x => x.title);

      this.doneCourses = this.allUserCourses.filter(x => x.state === 4).map(x => x.title);

      this.courseService.getAll()
        .subscribe(res => {
          this.allCourses = res
          this.allCourseTitles = this.allCourses
            .map(x => x.title)
            .filter((e) => !this.allUserCourses.map(x => x.title).includes(e));
        });
    });
  }

  ngOnInit(): void {

    
  }

  allCourseTitles: String[];

  allUserCourses: ICourse[];

  allCourses: ICourse[];

  todoCourses: String[];

  inProgCourses: String[];

  doneCourses: String[];

  drop(event: CdkDragDrop<string[]>, nextCourseGroup: number) {

    let courseTitle = event.item.element.nativeElement.innerText;

    let prevGroup = this.allUserCourses.filter(x => x.title === courseTitle)[0]?.state;

    if (!prevGroup) {
      prevGroup = 1;
    }

    if (event.previousContainer === event.container || nextCourseGroup - prevGroup != 1) {
      //ensures each course can be changed from Inactive>ToDo>InProgress>Done
      moveItemInArray(event.container.data, event.previousIndex, event.currentIndex);
    } else {
      transferArrayItem(event.previousContainer.data,
        event.container.data,
        event.previousIndex,
        event.currentIndex);

      let selectedCourse = this.allCourses.filter(x => x.title === courseTitle)[0];

      this.courseService.changeUserCourseState(selectedCourse.id, nextCourseGroup).subscribe();

      this.updateCourseState(selectedCourse, nextCourseGroup);
    }
  }

  updateCourseState(selectedCourse: ICourse, newState: number) {

    this.allUserCourses = this.allUserCourses.filter(x => x != selectedCourse);

    selectedCourse.state = newState;

    this.allUserCourses.push(selectedCourse);
  }
}
