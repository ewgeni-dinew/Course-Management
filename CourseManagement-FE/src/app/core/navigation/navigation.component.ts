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
export class NavigationComponent implements OnInit {

  constructor(private readonly authService: AuthService, private readonly accountService: AccountService, private readonly aletService: AlertService) { }

  get loggedUser(): IUser {
    return this.authService.getLoggedUser;
  }

  ngOnInit(): void {
  }

  isUserAdmin(): boolean {
    return this.loggedUser?.role === "Admin";
  }

  logoutHandler() {
    this.accountService.logout();

    this.aletService.addAlertWithArgs(AlertConsts.USER_LOGOUT_SUCCESS, AlertConsts.TYPE_INFO);
  }
  
  //TODO remove
  refreshTokenHandler() {
    this.accountService.refreshToken();
  }
}
