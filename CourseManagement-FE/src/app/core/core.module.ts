import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NotFoundComponent } from './not-found/not-found.component';
import { NavigationComponent } from './navigation/navigation.component';
import { RouterModule } from '@angular/router';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { FooterComponent } from './footer/footer.component';
import { HttpErrorComponent } from './error/http-error/http-error.component';
import { HttpErrorListComponent } from './error/http-error-list/http-error-list.component';

@NgModule({
  declarations: [NotFoundComponent, NavigationComponent, FooterComponent, HttpErrorComponent, HttpErrorListComponent],
  imports: [
    CommonModule,
    RouterModule,
    NgbModule
  ],
  exports: [
    NavigationComponent,
    FooterComponent,
    HttpErrorListComponent
  ]
})
export class CoreModule { }
