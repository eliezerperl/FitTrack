import { Component, EventEmitter, Input, Output } from '@angular/core';
import { NutrientEntry } from '../../../core/models/food-nutrient-model';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-nutrient-modal',
  imports: [CommonModule],
  templateUrl: './nutrient-modal.html',
  styleUrl: './nutrient-modal.css'
})
export class NutrientModal {
  @Input() nutrient: NutrientEntry | null = null;
  @Output() close = new EventEmitter<void>();
}
