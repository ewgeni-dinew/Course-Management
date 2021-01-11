import { Component, Input, OnInit, Output, EventEmitter } from '@angular/core';
import { IAlert } from 'src/app/shared/contracts/alert';

@Component({
  selector: 'app-alert',
  templateUrl: './alert.component.html',
  styleUrls: ['./alert.component.css']
})
export class AlertComponent implements OnInit {

  @Output()
  removeAlertEvent = new EventEmitter<IAlert>();

  @Input()
  alert: IAlert;

  constructor() { }

  ngOnInit(): void {

    //send an event to remove the error after a period of seconds;
    let timeout_period = 5 * 1000;

    setTimeout(() => {
      this.sendCloseAlertEvent(this.alert);
    }, timeout_period);
  }

  sendCloseAlertEvent(alert: IAlert) {
    this.removeAlertEvent.emit(alert);
  }
}