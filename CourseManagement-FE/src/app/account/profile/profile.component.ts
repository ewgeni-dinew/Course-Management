import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {



  constructor() { }

  ngOnInit(): void {
  }

  updateProfileHandler(data){
    console.log(data);
  }
}
