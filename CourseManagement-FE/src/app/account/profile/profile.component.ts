import { Component, OnInit } from '@angular/core';
import { AccountService } from 'src/app/services/account.service';
import { IUser } from 'src/app/shared/contracts/user';
import { AuthService } from 'src/app/services/auth.service';
import { AlertService } from 'src/app/services/alert.service';
import { AlertConsts } from 'src/app/utilities/constants/alerts';
import { Marker } from 'src/app/shared/contracts/marker';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {

  get loggedUser(): IUser {
    return this.authService.getLoggedUser;
  }
  constructor(private readonly accountService: AccountService, private readonly authService: AuthService, private readonly alertService: AlertService) { }

  ngOnInit(): void {
  }

  marker: Marker;

  updateMarker(){
    
  }

  updateProfileHandler(data: JSON) {
    if (confirm("Please, confirm you want to update the profile information?")) {
      data['id'] = this.loggedUser.id;

      this.accountService.updateAccount(data);

      //show success message
      //  this.alertService.addAlertWithArgs(AlertConsts.PROFILE_UPDATE_SUCCESS, AlertConsts.TYPE_SUCCESS)
    }
  }

  changePasswordHandler(data: JSON) {
    if (confirm("Please, confirm you want to change the profile password?")) {
      delete data['confirmPassword'];

      data['id'] = this.loggedUser.id;

      this.accountService.changeUserPassword(data).then(() => {
        this.alertService.addAlertWithArgs(AlertConsts.PASSWORD_CHANGE_SUCCESS, AlertConsts.TYPE_SUCCESS);
      });
    }
  }
}
