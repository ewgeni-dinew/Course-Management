import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NotFoundComponent } from './not-found/not-found.component';
import { NavigationComponent } from './navigation/navigation.component';
import { RouterModule } from '@angular/router';



@NgModule({
  declarations: [NotFoundComponent, NavigationComponent],
  imports: [
    CommonModule,
    RouterModule
  ],
  exports: [
    NavigationComponent
  ]
})
export class CoreModule { }
