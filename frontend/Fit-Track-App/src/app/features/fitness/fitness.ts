import { Component, OnInit } from '@angular/core';
import { WorkoutService } from '../../core/services/workout-service';
import { Exercise } from '../../core/models/exercise-model';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ExerciseCard } from '../../shared/components/exercise-card/exercise-card';
import { ExerciseModal } from '../../shared/components/exercise-modal/exercise-modal';

@Component({
  selector: 'app-fitness',
  imports: [CommonModule, FormsModule, ExerciseCard, ExerciseModal],
  templateUrl: './fitness.html',
  styleUrl: './fitness.css',
})
export class Fitness implements OnInit {
  currentTab: 'search' | 'log' = 'search';

  selectedExercise?: Exercise;
  showModal = false;

  // Filter options
  bodyParts: string[] = [];
  muscles: string[] = [];
  equipmentList: string[] = [];

  // Selected filters
  selectedFilterType: 'bodyPart' | 'muscle' | 'equipment' = 'bodyPart';
  selectedFilterValue: string = '';

  // Result
  exercises: Exercise[] = [];
  loading = false;

  constructor(private workoutService: WorkoutService) {}

  ngOnInit(): void {
    this.fetchFilterLists();
  }

  fetchFilterLists(): void {
    this.workoutService
      .getBodyPartList()
      .subscribe((res) => (this.bodyParts = res));
    this.workoutService
      .getTargetMuscleList()
      .subscribe((res) => (this.muscles = res));
    this.workoutService
      .getEquipmentList()
      .subscribe((res) => (this.equipmentList = res));
  }

  fetchExercises(): void {
    this.loading = true;

    const obs$ =
      this.selectedFilterType === 'bodyPart'
        ? this.workoutService.getBodyPartWorkouts(this.selectedFilterValue)
        : this.selectedFilterType === 'muscle'
        ? this.workoutService.getMuscleWorkouts(this.selectedFilterValue)
        : this.workoutService.getEquipmentWorkouts(this.selectedFilterValue);

    obs$.subscribe({
      next: (res) => {
        this.exercises = res;
        this.loading = false;
      },
      error: (err) => {
        console.error(err);
        this.loading = false;
      },
    });
  }

  onExerciseClick(exercise: Exercise) {
    this.selectedExercise = exercise;
    this.showModal = true;
  }

  closeModal() {
    this.showModal = false;
    this.selectedExercise = undefined;
  }

  getImageUrl(id: string) {
    return this.workoutService.getWorkoutDemoImage(id);
  }
}
