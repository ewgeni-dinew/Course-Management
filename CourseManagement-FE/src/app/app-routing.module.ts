import { RouterModule, Routes } from "@angular/router";
import { HomeComponent } from './home/home.component';
import { NotFoundComponent } from './core/not-found/not-found.component';

const routes: Routes = [
    {
        path: '',
        pathMatch: 'full',
        component: HomeComponent
    },
    {
        path: 'home',
        component: HomeComponent
    },
    {
        path: '**',
        component: NotFoundComponent
    }
];

export const AppRoutingModule = RouterModule.forRoot(routes, { relativeLinkResolution: 'legacy' });