import { Component, OnInit } from '@angular/core';
import { AccountService } from 'src/app/services/account.service';
import { IUser } from 'src/app/shared/contracts/user';

@Component({
  selector: 'app-navigation',
  templateUrl: './navigation.component.html',
  styleUrls: ['./navigation.component.css']
})
export class NavigationComponent implements OnInit {

  constructor(private accountService: AccountService) { }

  get loggedUser(): IUser {
    return this.accountService.loggedUser;
  }

  ngOnInit(): void {
  }

  isUserAdmin(): boolean {
    return this.loggedUser?.role === "Admin";
  }
}
