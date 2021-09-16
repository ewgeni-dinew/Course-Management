import { Injectable } from '@angular/core';
import * as mapboxgl from 'mapbox-gl';
import { environment } from 'src/environments/environment';
import { Marker } from '../shared/contracts/marker';

@Injectable({
  providedIn: 'root'
})
export class MapboxService {

  constructor() { }

  private inputMarker: Marker;

  
  public get getMarker() : Marker {
    return this.inputMarker;
  }
  
  map: mapboxgl.Map;
  style = 'mapbox://styles/mapbox/light-v10';
  lat = 42.78;
  lng = 25.31;
  zoom = 6.3

  buildMap() {
    this.map = new mapboxgl.Map({
      container: 'map',
      style: this.style,
      zoom: this.zoom,
      center: [this.lng, this.lat],
      accessToken: environment.mapbox.accessToken
    });

    this.map.addControl(new mapboxgl.NavigationControl());
  }

  addEventListener() {
    this.map.on('click', (event) => {
      this.inputMarker.lng = event.lngLat.lng;
      this.inputMarker.lat = event.lngLat.lat;
  });
}

fetchContributors() {
  const markers = [
    [27.90, 43.21],
    [23.33, 42.70],
    [23.56, 43.19],
    [25.33, 41.56],
    [25.95, 43.84],
    [24.01, 42.02],
  ];

  markers.forEach(m => {
    new mapboxgl.Marker({ color: 'red' })
      .setLngLat(m)
      .addTo(this.map);
  });
}
}