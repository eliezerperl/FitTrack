import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FoodList } from './food-list/food-list';
import { LogFood } from './log-food/log-food';

@Component({
  selector: 'app-food',
  imports: [CommonModule, FoodList, LogFood],
  templateUrl: './food.html',
  styleUrl: './food.css'
})
export class Food {
  currentTab: 'search' | 'log' = 'search';
}
