import { Component, OnInit } from '@angular/core';
import { IUser } from 'src/app/shared/contracts/user';
import { AuthService } from 'src/app/services/auth.service';
import { AccountService } from 'src/app/services/account.service';
import { AlertService } from 'src/app/services/alert.service';
import { AlertConsts } from 'src/app/utilities/constants/alerts';

@Component({
  selector: 'app-navigation',
  templateUrl: './navigation.component.html',
  styleUrls: ['./navigation.component.css']
})
export class NavigationComponent {

  constructor(private readonly authService: AuthService, private readonly accountService: AccountService, private readonly alertService: AlertService) { }

  get loggedUser(): IUser {
    return this.authService.getLoggedUser;
  }

  isUserAdmin(): Boolean {
    return this.authService.isUserAdmin;
  }

  logoutHandler() {
    this.accountService.logout();

    this.alertService.addAlertWithArgs(AlertConsts.USER_LOGOUT_SUCCESS, AlertConsts.TYPE_INFO);
  }  
}
