import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { ProfileComponent } from './profile/profile.component';
import { ListComponent } from './list/list.component';
import { AuthGuard } from '../utilities/guards/auth.guard';
import { AdminGuard } from '../utilities/guards/admin.guard';
import { NegateAuthGuard } from '../utilities/guards/negate-auth.guard';

const routes: Routes = [
    {
        path: 'account',
        children: [
            {
                path: 'login',
                component: LoginComponent,
                canActivate: [NegateAuthGuard]
            },
            {
                path: 'register',
                component: RegisterComponent,
                canActivate: [NegateAuthGuard]
            },
            {
                path: 'profile',
                component: ProfileComponent,
                canActivate: [AuthGuard]
            },
            {
                path: 'list',
                component: ListComponent,
                canActivate: [AuthGuard, AdminGuard]
            },
        ]
    }
]
export const AccountRoutingModule = RouterModule.forChild(routes);