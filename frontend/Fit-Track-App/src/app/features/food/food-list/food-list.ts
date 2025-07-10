import { Component } from '@angular/core';
import { FoodService } from '../../../core/services/food-service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-food-list',
  imports: [CommonModule, FormsModule],
  templateUrl: './food-list.html',
  styleUrl: './food-list.css',
})
export class FoodList {
  query: string = '';
  results: string[] = [];
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
        this.results = res;
        this.loading = false;
      },
      error: () => {
        this.results = [];
        this.loading = false;
      },
    });
  }
}
