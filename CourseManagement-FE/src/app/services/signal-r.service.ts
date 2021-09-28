import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class SignalRService {

  private courseViewers: CourseViewers[];

  private hubConnection: signalR.HubConnection;

  public get viewers(): CourseViewers[] {
    return this.courseViewers;
  }

  constructor() {
    this.courseViewers = [];
  }

  public connect() {

    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl(environment.baseURL + 'hub')
      .build();

    this.hubConnection
      .start()
      .then(() => console.log('WS connection started'))
      .catch((err) => console.log('Error on WS connection start: ' + err));

    this.onAddViewer();
    this.onRemoveViewer();
  }

  public disconnect() {
    this.hubConnection.stop()
      .then(() => console.log('WS connection stopped'))
      .catch((err) => console.log('Error on WS connection stop: ' + err));
  }

  public removeViewer(viewer: CourseViewer) {

    this.hubConnection.invoke('removeViewer', viewer)
      .catch(err => console.log(err));
  }

  onAddViewer() {
    this.hubConnection
      .on('addViewer', (data: CourseViewer) => {

        let courseViewer = this.courseViewers.find(x => x.courseId === data.courseId);

        if (!courseViewer) { // case 'no such course'
          courseViewer = {
            courseId: data.courseId,
            viewers: []
          };

          this.courseViewers.push(courseViewer);
        }

        courseViewer.viewers.push(data.viewer);
      });
  }

  onRemoveViewer() {
    this.hubConnection
      .on('removeViewer', (data: CourseViewer) => {

        let courseViewer = this.courseViewers.find(x => x.courseId === data.courseId);

        if (courseViewer) { // case 'course exists'
          courseViewer.viewers = courseViewer.viewers.filter(x => x !== data.viewer);
        }
      });
  }
}

export class CourseViewer {
  courseId: number;
  viewer: String;

  constructor(courseId: number, viewer: String) {
    this.courseId = courseId;
    this.viewer = viewer;
  }
}

export class CourseViewers {
  courseId: number;
  viewers: String[];
}
