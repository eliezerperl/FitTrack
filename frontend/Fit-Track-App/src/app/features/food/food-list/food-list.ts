import { Component } from '@angular/core';
import { FoodService } from '../../../core/services/food-service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Food } from '../../../core/models/food-model';
import { FoodModal } from '../../../shared/components/food-modal/food-modal';

@Component({
  selector: 'app-food-list',
  imports: [CommonModule, FormsModule, FoodModal],
  templateUrl: './food-list.html',
  styleUrl: './food-list.css',
})
export class FoodList {
  query: string = '';
  results: Food[] = [];
  selectedFood: Food | null = null;
  loading = false;
  hasSearched = false;

  constructor(private foodService: FoodService) {}

  onSubmit(event: Event) {
    event.preventDefault();
    this.onSearch(this.query);
  }

  onSearch(query: string) {
    this.loading = true;
    this.hasSearched = true;

    this.foodService.searchFoods(query).subscribe({
      next: (res: any) => {
        console.log(res)
        this.results = res;
        this.loading = false;
      },
      error: () => {
        this.results = [];
        this.loading = false;
      },
    });
  }

  openFoodModal(food: Food) {
    this.selectedFood = food;
  }
}
