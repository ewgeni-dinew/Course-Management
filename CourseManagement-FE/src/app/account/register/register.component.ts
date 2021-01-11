import { Component, OnInit } from '@angular/core';
import { AccountService } from 'src/app/services/account.service';
import { Router } from '@angular/router';
import { AlertService } from 'src/app/services/alert.service';
import { AlertConsts } from 'src/app/utilities/constants/alerts';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  constructor(private readonly accountService: AccountService, private readonly router: Router, private readonly alertService: AlertService) { }

  ngOnInit(): void {
  }

  registerHandler(data: JSON) {
    this.accountService.registerAccount(data).then(() => {
      this.alertService.addAlertWithArgs(AlertConsts.REGISTER_USER_SUCCESS, AlertConsts.TYPE_PRIMARY);
    }).then(() => {
      this.router.navigate(['account/login']);
    });
  }
}
