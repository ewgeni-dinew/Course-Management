import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { ProfileComponent } from './profile/profile.component';
import { FormsModule } from '@angular/forms';
import { AccountRoutingModule } from './account-routing.module';
import { ListComponent } from './list/list.component';

@NgModule({
  declarations: [LoginComponent, RegisterComponent, ProfileComponent, ListComponent],
  imports: [
    CommonModule,
    AccountRoutingModule,
    FormsModule //check
  ],
  exports: [
    LoginComponent //check
  ]
})
export class AccountModule { }
