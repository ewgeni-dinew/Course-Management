import { Component, OnInit } from '@angular/core';
import { MapboxService } from 'src/app/services/mapbox.service';

@Component({
  selector: 'app-map-box',
  templateUrl: './map-box.component.html',
  styleUrls: ['./map-box.component.css']
})
export class MapBoxComponent implements OnInit {

  constructor(private readonly mapboxService: MapboxService) { }

  ngOnInit(): void {
    this.mapboxService.buildMap();

    this.mapboxService.fetchContributors();

    this.mapboxService.addEventListener();
  }

}
