import { Routes } from '@angular/router';
import { FoodList } from './features/food/food-list/food-list';
import { Fitness } from './features/fitness/fitness';

export const routes: Routes = [
  { path: 'food', component: FoodList },
  { path: 'fitness', component: Fitness },
];
