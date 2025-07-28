import { Component, Input } from '@angular/core';
import { LoggedFood } from '../../../../core/models/log-food-model';
import { CommonModule } from '@angular/common';
import { NutrientEntry } from '../../../../core/models/food-nutrient-model';
import { NutrientListModal } from '../../../../shared/components/nutrient-list-modal/nutrient-list-modal';

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

  openNutrientListModal(nutrients: NutrientEntry[]): void {
    this.selectedNutrients = nutrients;
    this.showNutrientModal = true;
  }

  closeNutrientListModal(): void {
    this.showNutrientModal = false;
    this.selectedNutrients = [];
  }
}
