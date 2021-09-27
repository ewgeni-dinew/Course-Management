import { Injectable } from '@angular/core';
import * as mapboxgl from 'mapbox-gl';
import { environment } from 'src/environments/environment';
import { Marker } from '../shared/contracts/marker';
import { AccountService } from './account.service';

@Injectable({
  providedIn: 'root'
})
export class MapboxService {

  constructor(private readonly accountService: AccountService) { }

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

      this.accountService.setGeoLocation(this.inputMarker.lng, this.inputMarker.lat);
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

  addCurrentUserMarker(getLat: number, getLng: number) {
    new mapboxgl.Marker({ color: 'red' })
      .setLngLat([getLng, getLat])
      .addTo(this.map);
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

  fetchContributors() {

    this.accountService.getAllContributors().subscribe((res) => {
      res.forEach(m => {
        new mapboxgl.Marker({ color: 'red' })
          .setLngLat([m.geoLng, m.geoLat])
          .addTo(this.map);
      })
    });
  }
}