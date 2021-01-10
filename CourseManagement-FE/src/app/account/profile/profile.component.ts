import { Component, OnInit } from '@angular/core';
import { AccountService } from 'src/app/services/account.service';
import { IUser } from 'src/app/shared/contracts/user';
import { AuthService } from 'src/app/services/auth.service';
import { AppSettings } from "src/app/utilities/constants/app-settings";

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {

  get loggedUser(): IUser {
    return this.authService.getLoggedUser;
  }

  constructor(private accountService: AccountService, private readonly authService: AuthService) { }

  ngOnInit(): void {
  }

  updateProfileHandler(data: JSON) {
    data['id'] = this.loggedUser.id;
    this.accountService.updateAccount(data);
  }

  changePasswordHandler(data: JSON) {
    data['id'] = this.loggedUser.id;
    this.accountService.updateAccount(data);
  }
}
