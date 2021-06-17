import { Component, OnInit } from '@angular/core';
import { AccountService } from 'src/app/services/account.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  constructor(private readonly accountService: AccountService, private readonly router: Router) { }

  ngOnInit(): void {
  }

  loginHandler(data: JSON) {
    this.accountService.loginUser(data).then(() => {
      this.router.navigate(['/home']);
    });
  }
}
