import { RouterModule, Routes } from '@angular/router';
import { CreateComponent } from './create/create.component';
import { EditComponent } from './edit/edit.component';
import { HomeComponent } from './home/home.component';
import { AuthGuard } from '../utilities/guards/auth.guard';
import { AdminGuard } from '../utilities/guards/admin.guard';
import { BoardComponent } from './board/board.component';

const routes: Routes = [
    {
        path: 'course',
        children: [
            {
                path: '',
                component: HomeComponent,
            },        
            {
                path: 'list',
                component: HomeComponent
            },
            {
                path: 'favorites',
                component: HomeComponent
            },
            {
                path: 'create',
                component: CreateComponent,
                canActivate: [AdminGuard]
            },
            {
                path: 'edit/:id',
                component: EditComponent,
                canActivate: [AdminGuard]
            },
            {
                path: 'board',
                component: BoardComponent,            
            }
        ],
        canActivate: [AuthGuard]
    },
];

export const CourseRoutingModule = RouterModule.forChild(routes);