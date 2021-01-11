import { Component, OnInit } from '@angular/core';
import { AccountService } from 'src/app/services/account.service';
import { AlertService } from 'src/app/services/alert.service';
import { IUser } from 'src/app/shared/contracts/user';
import { AlertConsts } from 'src/app/utilities/constants/alerts';

@Component({
  selector: 'app-account-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.css']
})
export class ListComponent implements OnInit {

  accounts: IUser[];

  constructor(private readonly accountService: AccountService, private readonly alertService: AlertService) { }

  ngOnInit(): void {
    this.accounts = this.accountService.getAll();
  }

  unblockHandler(accountId: number) {
    this.accountService.unblockAccount(accountId).then(() => {
      this.alertService.addAlertWithArgs(AlertConsts.PROFILE_UNBLOCKED, AlertConsts.TYPE_WARNING);
    }).then(() => {
      this.accounts = this.accountService.getAll();
    });
  }

  blockHandler(accountId: number) {
    this.accountService.blockAccount(accountId).then(() => {
      this.alertService.addAlertWithArgs(AlertConsts.PROFILE_BLOCKED, AlertConsts.TYPE_WARNING);
    }).then(() => {
      this.accounts = this.accountService.getAll();
    });
  }

  deleteHandler(accountId: number) {
    if (confirm("Are you sure you want to delete this user?")) {

      this.accountService.deleteAccount(accountId).then(() => {
        this.alertService.addAlertWithArgs(AlertConsts.PROFILE_DELETED, AlertConsts.TYPE_WARNING);
      }).then(() => {
        this.accounts = this.accountService.getAll()
      });
    }
  }
}
