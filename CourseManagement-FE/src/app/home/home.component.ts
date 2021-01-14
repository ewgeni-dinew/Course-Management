import { Component, OnInit } from '@angular/core';
import { IUser } from '../shared/contracts/user';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  constructor(private readonly authService: AuthService) { }


  public get isUserLoggedIn(): Boolean {
    return !!this.loggedUser;
  }


  public get loggedUser(): IUser {
    return this.authService.getLoggedUser;
  }

  ngOnInit(): void {
  }

}
