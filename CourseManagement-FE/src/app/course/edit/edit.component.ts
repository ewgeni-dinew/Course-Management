import { Component, OnInit } from '@angular/core';
import { ICourse } from 'src/app/shared/contracts/course';

@Component({
  selector: 'app-edit',
  templateUrl: './edit.component.html',
  styleUrls: ['./edit.component.css']
})
export class EditComponent implements OnInit {

  
  public get course() : ICourse {
    return ;
  }
  
  constructor() { }

  ngOnInit(): void {
  }

  editHandler(data: JSON) {

  }
}
