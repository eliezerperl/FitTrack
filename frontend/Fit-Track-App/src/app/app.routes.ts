import { Routes } from '@angular/router';
import { Fitness } from './features/fitness/fitness';
import { Food } from './features/food/food';
import { Login } from './features/auth/login/login';
import { Register } from './features/auth/register/register';
import { Dashboard } from './features/dashboard/dashboard';
import { NotFound } from './features/not-found/not-found';
import { Progress } from './features/dashboard/progress/progress';
import { DashboardHome } from './features/dashboard/dashboard-home/dashboard-home';
import { SearchWorkouts } from './features/fitness/search-workouts/search-workouts';
import { LogWorkout } from './features/fitness/log-workout/log-workout';

export const routes: Routes = [
  { path: '', redirectTo: 'login', pathMatch: 'full' },
  { path: 'login', component: Login },
  { path: 'register', component: Register },
  { path: 'food', component: Food },
  {
    path: 'fitness',
    component: Fitness,
    children: [
      { path: 'search', component: SearchWorkouts },
      { path: 'log', component: LogWorkout },
      {
        path: '',
        pathMatch: 'full',
        redirectTo: 'search',
      },
    ],
  },
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
        redirectTo: 'home',
      },
      {
        path: 'home',
        component: DashboardHome,
      },
    ],
  },
  { path: '**', component: NotFound },
];
