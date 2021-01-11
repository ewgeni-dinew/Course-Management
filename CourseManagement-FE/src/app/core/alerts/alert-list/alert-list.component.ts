import { Component, Input, OnInit } from '@angular/core';
import { AlertService } from 'src/app/services/alert.service';
import { IAlert } from 'src/app/shared/contracts/alert';

@Component({
  selector: 'app-alert-list',
  templateUrl: './alert-list.component.html',
  styleUrls: ['./alert-list.component.css']
})
export class AlertListComponent implements OnInit {

  constructor(private readonly alertService: AlertService) {
    this.alerts = this.alertService.getAlerts; //set the alerts from HttpErrorService
  }

  ngOnInit(): void {
  }

  alerts: IAlert[];

  close(alert: IAlert) {
    this.alerts.splice(this.alerts.indexOf(alert), 1); //removes alert from array
  }
}
