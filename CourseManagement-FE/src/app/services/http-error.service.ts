import { Injectable } from '@angular/core';
import { IAlert } from '../shared/contracts/alert';

@Injectable({
  providedIn: 'root'
})
export class HttpErrorService {

  constructor() {
    this.alerts = [];
  }

  private alerts: IAlert[];

  public get getAlerts(): IAlert[] {
    return this.alerts;
  }

  public addAlert(alert: IAlert) {
    this.alerts.push(alert);
  }
}
