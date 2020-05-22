import { RouterModule, Routes } from '@angular/router';
import { CreateComponent } from './create/create.component';
import { EditComponent } from './edit/edit.component';
import { HomeComponent } from './home/home.component';

const routes: Routes = [
    {
        path: 'course',
        children:[
            {
                path: '',
                component: HomeComponent
            },
            {
                path: 'create',
                component: CreateComponent
            },
            {
                path: 'edit',
                component: EditComponent
            },
            {
                path: 'list',
                component: HomeComponent
            }
        ]
    },
];

export const CourseRoutingModule = RouterModule.forChild(routes);