import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MapBoxComponent } from './components/mapbox/map-box.component';


@NgModule({
  declarations: [
    MapBoxComponent
  ],
  exports: [
    MapBoxComponent
  ],
  imports: [
    CommonModule
  ]
})
export class SharedModule { }
