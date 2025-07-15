import { Component, Input } from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { FoodService } from '../../../core/services/food-service';
import { FoodNutrient } from '../../../core/models/food-nutrient-model';
import { ToastService } from '../../../core/services/toast-service';
import { CommonModule } from '@angular/common';
import { LoggedFood } from '../../../core/models/log-food-model';
import { NutrientListModal } from '../../../shared/components/nutrient-list-modal/nutrient-list-modal';

@Component({
  selector: 'app-log-food',
  imports: [CommonModule, ReactiveFormsModule, NutrientListModal],
  templateUrl: './log-food.html',
  styleUrl: './log-food.css',
})
export class LogFood {
  foodForm!: FormGroup;
  searchQuery = '';
  foodResults: any[] = [];
  nutrients: FoodNutrient[] = [];
  loading = false;
  showNutrients = false;

  constructor(
    private fb: FormBuilder,
    private foodService: FoodService,
    private toastService: ToastService
  ) {}

  ngOnInit(): void {
    this.foodForm = this.fb.group({
      searchQuery: [''],
      foodName: ['', Validators.required],
      quantity: [1, [Validators.required, Validators.min(1)]],
      notes: [''],
    });
  }

  onSearch(): void {
    const encodedQuery = encodeURIComponent(this.foodForm.value.searchQuery.trim());
    if (!encodedQuery) return;

    this.loading = true;
    this.foodService.searchFoods(encodedQuery).subscribe({
      next: (res) => {
        this.foodResults = res;
        this.loading = false;
      },
      error: (err) => {
        this.loading = false;
        console.log(err);
      },
    });
  }

  selectFood(food: any): void {
    this.foodForm.patchValue({ foodName: food.description });
    this.nutrients = food.foodNutrients;
    this.foodResults = [];
  }

  submit(): void {
    if (this.foodForm.valid) {

      const mappedNutrients = this.nutrients.map(n => ({
        name: n.nutrientName,
        unit: n.unitName,
        value: n.value,
      }));

      this.loading = true;
      const entry: LoggedFood = {
        foodId: crypto.randomUUID(),
        userId: '00000000-0000-0000-0000-000000000000',
        foodName: this.foodForm.value.foodName,
        quantity: this.foodForm.value.quantity,
        notes: this.foodForm.value.notes,
        date: new Date(),
        nutrients: mappedNutrients,
      };
      console.log('Sending to backend:', entry);
      this.foodService.createFoodEntry(entry).subscribe({
        next: (res: any) => {
          console.log(res);
          this.loading = false;
          this.toastService.successToast('Food entry logged!');
        },
        error: (err) => {
          console.log(err);
          this.loading = false;
        },
      });
    }
  }
}
