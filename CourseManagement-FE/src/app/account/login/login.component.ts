import { Component, OnInit } from '@angular/core';
import { AccountService } from 'src/app/services/account.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  constructor(private accountService: AccountService, private router: Router) { }

  ngOnInit(): void {
  }

  loginHandler(data: JSON) {
    let promise = new Promise<void>((resolve, reject) => {
      this.accountService.login(data).then(() => {
         this.router.navigate(['/home']) 
        });

      resolve();
    });

  }
}
