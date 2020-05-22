import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { ProfileComponent } from './profile/profile.component';
import { ListComponent } from './list/list.component';

const routes: Routes = [
    {
        path: 'account',
        children: [
            {
                path: 'login',
                component: LoginComponent
            },
            {
                path: 'register',
                component: RegisterComponent
            },
            {
                path: 'profile',
                component: ProfileComponent
            },
            {
                path: 'list',
                component: ListComponent
            },
        ]
    }
]
export const AccountRoutingModule = RouterModule.forChild(routes);