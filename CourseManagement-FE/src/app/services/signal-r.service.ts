import { Injectable, OnInit } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { environment } from 'src/environments/environment';
import { CourseService } from './course.service';

@Injectable({
  providedIn: 'root'
})
export class SignalRService implements OnInit {

  private hubConnection: signalR.HubConnection;

  constructor(private readonly courseService: CourseService) {

    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl(environment.baseURL + 'hub')
      .build();

    this.hubConnection
      .start()
      .then(() => console.log('WS connection started'))
      .catch((err) => console.log('Error on WS connection start: ' + err));
  }

  ngOnInit(): void {


  }

  public startConnection() {

    this.hubConnection
      .on('addCourse', (data) => {
        //this.courseService.
      });
  }
}
