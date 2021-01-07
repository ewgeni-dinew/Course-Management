import { Component, OnInit } from '@angular/core';
import { HttpErrorService } from 'src/app/services/http-error.service';
import { IAlert } from 'src/app/shared/contracts/alert';

@Component({
  selector: 'app-http-error',
  templateUrl: './http-error.component.html',
  styleUrls: ['./http-error.component.css']
})
export class HttpErrorComponent implements OnInit {

  constructor(private readonly errorService: HttpErrorService) {
    this.alerts = this.errorService.getAlerts;
    //this.reset();
  }

  ngOnInit(): void {
  }
  
  alerts: IAlert[];

  close(alert: IAlert) {
    this.alerts.splice(this.alerts.indexOf(alert), 1);
  }

  reset() {
    this.alerts = Array.from(ALERTS);
  }
}

const ALERTS: IAlert[] = [
  {
    type: 'success',
    message: 'This is an success alert',
  },
  {
    type: 'warning',
    message: 'This is a warning alert',
  }, 
  {
    type: 'danger',
    message: 'This is a danger alert',
  }
];
