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
  private map: mapboxgl.Map;
  private style = 'mapbox://styles/mapbox/light-v10';
  private lat = 42.78;
  private lng = 25.31;
  private zoom = 6.3

  public get getMarker(): Marker {
    return this.inputMarker;
  }

  public get getMap(): mapboxgl.Map {
    return this.map;
  }


  destroyMap() {
    this.map = null;
  }

  saveMarkerToMap() {

    if (this.map._markers.length === 1) {
      this.map._markers[0].remove();

      new mapboxgl.Marker({ color: 'red' })
        .setLngLat([this.inputMarker.lng, this.inputMarker.lat])
        .addTo(this.map);

      //TODO send call to API
    }
  }

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

  //
  // Used when wanting to mark a desired location (Marker)
  //
  addEventListener() {
    this.map.on('click', (event) => {

      if (typeof this.inputMarker === 'undefined' || (event.lngLat.lng !== this.inputMarker.lng && event.lngLat.lat !== this.inputMarker.lat)) {

        //set the clicked coordinates to the inputMarker
        this.inputMarker = {
          lng: event.lngLat.lng,
          lat: event.lngLat.lat
        };

        //add the new point to the map
        new mapboxgl.Marker({ color: 'black' })
          .setLngLat([this.inputMarker.lng, this.inputMarker.lat])
          .addTo(this.map);

        //remove the previous point from the map
        if (this.map._markers.length > 1) {
          this.map._markers[0].remove();
        }
      }
    });
  }

  //TODO: call the BE api
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