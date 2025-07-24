import { Routes } from '@angular/router';
import { Fitness } from './features/fitness/fitness';
import { Food } from './features/food/food';
import { Login } from './features/auth/login/login';
import { Register } from './features/auth/register/register';
import { Dashboard } from './features/dashboard/dashboard';
import { NotFound } from './features/not-found/not-found';

export const routes: Routes = [
  { path: '', redirectTo: 'login', pathMatch: 'full' },
  { path: 'login', component: Login },
  { path: 'register', component: Register },
  { path: 'food', component: Food },
  { path: 'fitness', component: Fitness },
  { path: 'dashboard', component: Dashboard },
  { path: '**', component: NotFound },
];
