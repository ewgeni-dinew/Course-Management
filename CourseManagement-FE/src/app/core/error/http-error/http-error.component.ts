import { Component, Input, OnInit, Output, EventEmitter } from '@angular/core';
import { IAlert } from 'src/app/shared/contracts/alert';

@Component({
  selector: 'app-http-error',
  templateUrl: './http-error.component.html',
  styleUrls: ['./http-error.component.css']
})
export class HttpErrorComponent implements OnInit {

  @Output()
  removeAlertEvent = new EventEmitter<IAlert>();

  @Input()
  alert: IAlert;

  constructor() { }

  ngOnInit(): void {

    //send an event to remove the error after a period of seconds;
    let timeout_period = 6 * 1000;

    setTimeout(() => {
      this.sendCloseAlertEvent(this.alert);
    }, timeout_period);
  }

  sendCloseAlertEvent(alert: IAlert) {
    this.removeAlertEvent.emit(alert);
  }
}