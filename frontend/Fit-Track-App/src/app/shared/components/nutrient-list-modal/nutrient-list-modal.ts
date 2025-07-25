import { Component, EventEmitter, Input, Output } from '@angular/core';
import { FoodNutrient } from '../../../core/models/food-nutrient-model';
import { CommonModule } from '@angular/common';
import { NutrientModal } from '../nutrient-modal/nutrient-modal';

@Component({
  selector: 'app-nutrient-list-modal',
  imports: [CommonModule, NutrientModal],
  templateUrl: './nutrient-list-modal.html',
  styleUrl: './nutrient-list-modal.css'
})
export class NutrientListModal {
  @Input() nutrients: FoodNutrient[] = [];
  @Output() close = new EventEmitter<void>();

  selectedNutrient: FoodNutrient | null = null;

  openNutrient(nutrient: FoodNutrient) {
    this.selectedNutrient = nutrient;
  }

  closeNutrientModal() {
    this.selectedNutrient = null;
  }
}
