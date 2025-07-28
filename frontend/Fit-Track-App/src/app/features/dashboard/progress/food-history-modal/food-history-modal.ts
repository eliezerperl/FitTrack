import { Component, Input } from '@angular/core';
import { LoggedFood } from '../../../../core/models/log-food-model';
import { CommonModule } from '@angular/common';
import { NutrientEntry } from '../../../../core/models/food-nutrient-model';
import { NutrientListModal } from '../../../../shared/components/nutrient-list-modal/nutrient-list-modal';
import { FoodService } from '../../../../core/services/food-service';

@Component({
  selector: 'app-food-history-modal',
  imports: [CommonModule, NutrientListModal],
  templateUrl: './food-history-modal.html',
  styleUrl: './food-history-modal.css',
})
export class FoodHistoryModal {
  @Input() entries: LoggedFood[] = [];
  @Input() visible: boolean = false;
  @Input() onClose: () => void = () => {};

  showNutrientModal = false;
  selectedNutrients: NutrientEntry[] = [];

  constructor(private foodService: FoodService) {}

  openNutrientListModal(food: string): void {
    this.foodService.searchFoodNutrients(food).subscribe({
      next: (res) => {
        this.selectedNutrients = res;
        this.showNutrientModal = true;
      },
      error: (err) => {
        console.log(err);
      },
    });
  }

  closeNutrientListModal(): void {
    this.showNutrientModal = false;
    this.selectedNutrients = [];
  }
}
