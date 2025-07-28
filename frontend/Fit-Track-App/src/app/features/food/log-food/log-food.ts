import { Component, Input } from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { FoodService } from '../../../core/services/food-service';
import { FoodNutrient, NutrientEntry } from '../../../core/models/food-nutrient-model';
import { ToastService } from '../../../core/services/toast-service';
import { CommonModule } from '@angular/common';
import { LoggedFood } from '../../../core/models/log-food-model';
import { NutrientListModal } from '../../../shared/components/nutrient-list-modal/nutrient-list-modal';
import { SharedService } from '../../../core/services/shared-service';

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
  nutrients: NutrientEntry[] = [];
  loading = false;
  showNutrients = false;
  userId: string | null = null;

  constructor(
    private fb: FormBuilder,
    private foodService: FoodService,
    private sharedService: SharedService,
    private toastService: ToastService
  ) {}

  ngOnInit(): void {
    this.sharedService.userId$.subscribe((id) => (this.userId = id));

    this.foodForm = this.fb.group({
      searchQuery: [''],
      foodName: ['', Validators.required],
      foodId: [''],
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
    this.foodForm.patchValue({ foodName: food.description, foodId: food.fdcId });
    this.nutrients = food.foodNutrients;
    this.foodResults = [];
  }

  submit(): void {
    if (this.foodForm.valid) {

      // const mappedNutrients = this.nutrients.map(n => ({
      //   name: n.nutrientName,
      //   unit: n.unitName,
      //   value: n.value,
      // }));

      this.loading = true;
      const entry: LoggedFood = {
        id: crypto.randomUUID(),
        foodId: this.foodForm.value.foodId,
        userId: this.userId ?? '00000000-0000-0000-0000-000000000000',
        foodName: this.foodForm.value.foodName,
        quantity: this.foodForm.value.quantity,
        notes: this.foodForm.value.notes,
        dateLogged: new Date(),
        nutrients: this.nutrients,
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
          this.toastService.failToast('Food entry failed');
        },
      });
    }
  }
}
