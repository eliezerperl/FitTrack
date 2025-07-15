import { Routes } from '@angular/router';
import { Fitness } from './features/fitness/fitness';
import { Food } from './features/food/food';
import { Login } from './features/auth/login/login';
import { Register } from './features/auth/register/register';

export const routes: Routes = [
  { path: '', redirectTo: 'login', pathMatch: 'full' },
  { path: 'login', component: Login },
  { path: 'register', component: Register },
  { path: 'food', component: Food },
  { path: 'fitness', component: Fitness },
  { path: '**', redirectTo: 'login' },
];
