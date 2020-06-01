import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NotFoundComponent } from './not-found/not-found.component';
import { NavigationComponent } from './navigation/navigation.component';
import { RouterModule } from '@angular/router';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { FooterComponent } from './footer/footer.component';



@NgModule({
  declarations: [NotFoundComponent, NavigationComponent, FooterComponent],
  imports: [
    CommonModule,
    RouterModule,
    NgbModule
  ],
  exports: [
    NavigationComponent,
    FooterComponent
  ]
})
export class CoreModule { }
