import { Component, OnInit } from '@angular/core';
import { AccountService } from 'src/app/services/account.service';
import { IUser } from 'src/app/shared/contracts/user';

@Component({
  selector: 'app-account-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.css']
})
export class ListComponent implements OnInit {

  accounts: IUser[];

  constructor(private accountService: AccountService) { }

  ngOnInit(): void {
    this.accounts = this.accountService.getAll();
  }

  unblockHandler(accountId: number) {
    this.accountService.unblockAccount(accountId).then(() => {
      this.accounts = this.accountService.getAll();
    });
  }

  blockHandler(accountId: number) {
    this.accountService.blockAccount(accountId).then(() => {
      this.accounts = this.accountService.getAll();
    });
  }

  deleteHandler(accountId: number) {
    if (confirm("Are you sure you want to delete this user?")) {

      this.accountService.deleteAccount(accountId).then(() => {
        this.accounts = this.accountService.getAll()
      });
    }
  }
}
