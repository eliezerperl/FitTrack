import { Routes } from '@angular/router';
import { Fitness } from './features/fitness/fitness';
import { Food } from './features/food/food';

export const routes: Routes = [
  { path: 'food', component: Food },
  { path: 'fitness', component: Fitness },
];
