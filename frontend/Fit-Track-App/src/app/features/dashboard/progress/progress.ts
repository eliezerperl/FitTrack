import { Component, OnInit } from '@angular/core';
import { WorkoutService } from '../../../core/services/workout-service';
import { SharedService } from '../../../core/services/shared-service';
import { Exercise } from '../../../core/models/exercise-model';
import { WorkoutHistoryModal } from './workout-history-modal/workout-history-modal';
import { LoggedWorkout } from '../../../core/models/log-workout-model';
import { LoggedFood } from '../../../core/models/log-food-model';
import { FoodService } from '../../../core/services/food-service';
import { FoodHistoryModal } from './food-history-modal/food-history-modal';

@Component({
  selector: 'app-progress',
  imports: [WorkoutHistoryModal, FoodHistoryModal],
  templateUrl: './progress.html',
  styleUrl: './progress.css',
})
export class Progress implements OnInit {
  userId: string | null = null;
  myLoggedWorkouts: LoggedWorkout[] = [];
  myLoggedFoods: LoggedFood[] = [];

  showLoggedWorkoutsModal: boolean = false;
  showLoggedFoodModal: boolean = false;

  constructor(
    private sharedService: SharedService,
    private workoutService: WorkoutService,
    private foodService: FoodService
  ) {}

  ngOnInit(): void {
    this.sharedService.userId$.subscribe((id) => {
      this.userId = id;
    });
  }

  getMyWorkoutHistory() {
    if (this.userId) {
      this.workoutService.getWorkoutByUserId(this.userId).subscribe({
        next: (res) => {
          this.myLoggedWorkouts = res;
          this.showLoggedWorkoutsModal = true;
          console.log('Workout history:', this.myLoggedWorkouts);
        },
        error: (err) => {
          console.log(err);
        },
      });
    }
  }

  onCloseLoggedWorkoutModal(): void {
    this.showLoggedWorkoutsModal = false;
  }

  onShowFoodHistory(): void {
    if (this.userId) {
      this.foodService.getFoodEntriesByUserId(this.userId).subscribe({
        next: (res) => {
          this.myLoggedFoods = res;
          this.showLoggedFoodModal = true;
        },
        error: (err) => console.error(err),
      });
    }
  }

  onCloseFoodModal(): void {
    this.showLoggedFoodModal = false;
  }
}
