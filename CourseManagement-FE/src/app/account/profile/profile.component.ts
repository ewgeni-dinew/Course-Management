import { Component, OnInit } from '@angular/core';
import { AccountService } from 'src/app/services/account.service';
import { IUser } from 'src/app/shared/contracts/user';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {

  get loggedUser(): IUser {
    return this.accountService.getLoggedUser;
  }

  constructor(private accountService: AccountService) { }

  ngOnInit(): void {
  }

  updateProfileHandler(data: JSON) {
    data['id'] = this.loggedUser.id;
    this.accountService.updateAccount(data);
  }

  logoutHandler() {
    this.accountService.logout();
  }
}
