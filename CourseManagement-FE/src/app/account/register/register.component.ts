import { Component, OnInit } from '@angular/core';
import { AccountService } from 'src/app/services/account.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  constructor(private accountService: AccountService, private router: Router) { }

  ngOnInit(): void {
  }

  registerHandler(data: JSON) {
    let promise = new Promise<void>((resolve, reject) => {
      this.accountService.registerAccount(data).then(() => {
        this.router.navigate(['account/login']);
      });

      resolve();
    });
  }
}
