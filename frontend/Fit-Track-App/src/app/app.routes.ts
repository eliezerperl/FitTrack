import { Routes } from '@angular/router';
import { Fitness } from './features/fitness/fitness';
import { Food } from './features/food/food';
import { Login } from './features/auth/login/login';
import { Register } from './features/auth/register/register';
import { Dashboard } from './features/dashboard/dashboard';
import { NotFound } from './features/not-found/not-found';
import { Progress } from './features/dashboard/progress/progress';
import { DashboardHome } from './features/dashboard/dashboard-home/dashboard-home';

export const routes: Routes = [
  { path: '', redirectTo: 'login', pathMatch: 'full' },
  { path: 'login', component: Login },
  { path: 'register', component: Register },
  { path: 'food', component: Food },
  { path: 'fitness', component: Fitness },
  {
    path: 'dashboard',
    component: Dashboard,
    children: [
      {
        path: 'progress',
        component: Progress,
      },
      {
        path: '',
        pathMatch: 'full',
        redirectTo: 'home', // optional landing screen inside dashboard
      },
      {
        path: 'home',
        component: DashboardHome, // the current dashboard home content
      },
    ],
  },
  { path: '**', component: NotFound },
];
