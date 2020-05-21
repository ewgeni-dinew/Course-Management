import { Component, OnInit } from '@angular/core';
import { AccountService } from 'src/app/services/account.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  constructor(private accountService: AccountService) { }

  ngOnInit(): void {
  }

  registerHandler(data){
    console.log(data);

    this.accountService.registerAccount(data);
  }
}
