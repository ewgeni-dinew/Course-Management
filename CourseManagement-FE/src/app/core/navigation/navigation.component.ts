import { Component, OnInit } from '@angular/core';
import { IUser } from 'src/app/shared/contracts/user';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-navigation',
  templateUrl: './navigation.component.html',
  styleUrls: ['./navigation.component.css']
})
export class NavigationComponent implements OnInit {

  constructor(private readonly authService: AuthService) { }

  get loggedUser(): IUser {
    return this.authService.getLoggedUser;
  }

  ngOnInit(): void {
  }

  isUserAdmin(): boolean {
    return this.loggedUser?.role === "Admin";
  }
}
