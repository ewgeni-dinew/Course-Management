import { Component, OnInit } from '@angular/core';
import { IUser } from '../shared/contracts/user';
import { AuthService } from '../services/auth.service';
import { MapboxService } from '../services/mapbox.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  constructor(private readonly authService: AuthService, private readonly mapBoxService: MapboxService) { }

  ngOnInit(): void {
    this.mapBoxService.buildMap();
    this.mapBoxService.fetchContributors();
  }

  public get isUserLoggedIn(): Boolean {
    return !!this.loggedUser;
  }

  public get loggedUser(): IUser {
    return this.authService.getLoggedUser;
  }

}
