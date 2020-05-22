import { Component, OnInit } from '@angular/core';
import { AccountService } from 'src/app/services/account.service';
import { IUser } from 'src/app/shared/contracts/user';

@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.css']
})
export class ListComponent implements OnInit {

  accounts: IUser[];

  constructor(private accountService: AccountService) { }

  ngOnInit(): void {
    this.accounts = this.accountService.getAll(); 
  }
}
