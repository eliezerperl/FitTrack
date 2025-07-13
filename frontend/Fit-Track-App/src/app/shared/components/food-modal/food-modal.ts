import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Food } from '../../../core/models/food-model';
import { FoodNutrient } from '../../../core/models/food-nutrient-model';
import { CommonModule } from '@angular/common';
import { NutrientModal } from '../nutrient-modal/nutrient-modal';

@Component({
  selector: 'app-food-modal',
  imports: [CommonModule, NutrientModal],
  templateUrl: './food-modal.html',
  styleUrl: './food-modal.css'
})
export class FoodModal {
  @Input() food: Food | null = null;
  @Output() close = new EventEmitter<void>();

  selectedNutrient: FoodNutrient | null = null;

  openNutrientModal(nutrient: FoodNutrient) {
    this.selectedNutrient = nutrient;
  }
}
