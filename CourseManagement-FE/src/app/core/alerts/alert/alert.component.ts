import { Component, Input, OnInit, Output, EventEmitter } from '@angular/core';
import { IAlert } from 'src/app/shared/contracts/alert';
import { AlertConsts } from 'src/app/utilities/constants/alerts';

@Component({
  selector: 'app-alert',
  templateUrl: './alert.component.html',
  styleUrls: ['./alert.component.css']
})
export class AlertComponent implements OnInit {

  @Output()
  removeAlertEvent = new EventEmitter<IAlert>();

  @Input()
  public alert: IAlert;

  constructor() { }

  ngOnInit(): void {

    if (this.alert) {
      //send an event to remove the error after a period of seconds;
      let timeout_period: number;

      //different times for different types
      switch (this.alert.type) {
        case AlertConsts.TYPE_DANGER:
          timeout_period = 5000
          break;
        case AlertConsts.TYPE_INFO:
          timeout_period = 1500
          break;
        case AlertConsts.TYPE_WARNING:
          timeout_period = 2000
          break;
        case AlertConsts.TYPE_PRIMARY:
          timeout_period = 1500
          break;
        case AlertConsts.TYPE_SUCCESS:
          timeout_period = 1500
          break;
        default:
          break;
      }

      setTimeout(() => {
        this.sendCloseAlertEvent(this.alert);
      }, timeout_period);
    }
  }

  sendCloseAlertEvent(alert: IAlert) {
    this.removeAlertEvent.emit(alert);
  }
}