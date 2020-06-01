import { Component, OnInit } from '@angular/core';
import { AccountService } from '../services/account.service';
import { IUser } from '../shared/contracts/user';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  constructor(private accountService: AccountService) { }

  
  public get isUserLoggedIn() : Boolean {
    return !!this.loggedUser;
  }
  
  
  public get loggedUser() : IUser {
    return this.accountService.getLoggedUser;
  }
  
  ngOnInit(): void {
    console.log(this.isUserLoggedIn);
  }

}
