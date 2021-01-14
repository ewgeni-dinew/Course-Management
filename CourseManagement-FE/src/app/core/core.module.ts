import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NotFoundComponent } from './not-found/not-found.component';
import { NavigationComponent } from './navigation/navigation.component';
import { RouterModule } from '@angular/router';
import { NgbAlertModule, NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { FooterComponent } from './footer/footer.component';
import { AlertComponent } from './alerts/alert/alert.component';
import { AlertListComponent } from './alerts/alert-list/alert-list.component';

@NgModule({
  declarations: [NotFoundComponent, NavigationComponent, FooterComponent, AlertComponent, AlertListComponent],
  imports: [
    CommonModule,
    RouterModule,
    NgbModule,
    NgbAlertModule
  ],
  exports: [
    NavigationComponent,
    FooterComponent,
    AlertComponent,
    AlertListComponent
  ]
})
export class CoreModule { }
