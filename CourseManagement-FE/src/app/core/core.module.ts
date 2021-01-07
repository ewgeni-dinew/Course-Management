import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NotFoundComponent } from './not-found/not-found.component';
import { NavigationComponent } from './navigation/navigation.component';
import { RouterModule } from '@angular/router';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { FooterComponent } from './footer/footer.component';
import { HttpErrorComponent } from './http-error/http-error.component';

@NgModule({
  declarations: [NotFoundComponent, NavigationComponent, FooterComponent, HttpErrorComponent],
  imports: [
    CommonModule,
    RouterModule,
    NgbModule
  ],
  exports: [
    NavigationComponent,
    FooterComponent,
    HttpErrorComponent
  ]
})
export class CoreModule { }
