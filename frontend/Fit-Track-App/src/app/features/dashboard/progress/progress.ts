import { Component, OnInit } from '@angular/core';
import { WorkoutService } from '../../../core/services/workout-service';
import { SharedService } from '../../../core/services/shared-service';
import { Exercise } from '../../../core/models/exercise-model';
import { WorkoutHistoryModal } from './workout-history-modal/workout-history-modal';
import { LoggedWorkout } from '../../../core/models/log-workout-model';
import { LoggedFood } from '../../../core/models/log-food-model';
import { FoodService } from '../../../core/services/food-service';
import { FoodHistoryModal } from './food-history-modal/food-history-modal';
import { WorkoutProgressChart } from './workout-progress-chart/workout-progress-chart';
import { FoodProgressChart } from './food-progress-chart/food-progress-chart';
@Component({
  selector: 'app-progress',
  imports: [
    WorkoutHistoryModal,
    FoodHistoryModal,
    WorkoutProgressChart,
    FoodProgressChart,
  ],
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
    if (this.userId) {
      this.workoutService.getWorkoutByUserId(this.userId).subscribe({
        next: (res) => {
          this.myLoggedWorkouts = res;
          console.log('Workout history:', this.myLoggedWorkouts);
        },
        error: (err) => {
          console.log(err);
        },
      });
      this.foodService.getFoodEntriesByUserId(this.userId).subscribe({
        next: (res) => {
          this.myLoggedFoods = res;
        },
        error: (err) => console.error(err),
      });
    }
  }

  getMyWorkoutHistory() {
    this.showLoggedWorkoutsModal = true;
  }

  onCloseLoggedWorkoutModal(): void {
    this.showLoggedWorkoutsModal = false;
  }

  onShowFoodHistory(): void {
    this.showLoggedFoodModal = true;
  }

  onCloseFoodModal(): void {
    this.showLoggedFoodModal = false;
  }
}
