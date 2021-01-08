import { Component, Input, OnInit } from '@angular/core';
import { HttpErrorService } from 'src/app/services/http-error.service';
import { IAlert } from 'src/app/shared/contracts/alert';

@Component({
  selector: 'app-http-error-list',
  templateUrl: './http-error-list.component.html',
  styleUrls: ['./http-error-list.component.css']
})
export class HttpErrorListComponent implements OnInit {

  constructor(private readonly errorService: HttpErrorService) {
    this.alerts = this.errorService.getAlerts; //set the alerts from HttpErrorService
  }

  ngOnInit(): void {
  }

  alerts: IAlert[];

  close(alert: IAlert) {
    this.alerts.splice(this.alerts.indexOf(alert), 1); //removes alert from array
  }
}
