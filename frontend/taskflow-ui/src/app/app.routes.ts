import { Routes } from '@angular/router';
import { Login } from './auth/login/login';
import { AuthGuard } from './core/guards/auth-guard';
import { Home } from './dashboard/home/home';
import { ProjectList } from './projects/project-list/project-list';
import { ProjectCreate } from './projects/project-create/project-create';

export const routes: Routes = [
    {path:'login', loadComponent: ()=> Login},
    {
        path:'dashboard',
        canActivate: [AuthGuard],
        loadComponent: () => Home
    },
    {
        path: 'projects',
        children: [
        { path: '', loadComponent: () => ProjectList, canActivate: [AuthGuard] },
        { path: 'create', loadComponent: () => ProjectCreate, canActivate: [AuthGuard] }
        ]
    },
    {path: '**', redirectTo: 'login'}
];
