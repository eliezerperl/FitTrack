import { Component, OnInit } from '@angular/core';
import { Exercise } from '../../../core/models/exercise-model';
import {
  FormBuilder,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { WorkoutService } from '../../../core/services/workout-service';
import { CommonModule } from '@angular/common';
import { LoggedWorkout } from '../../../core/models/log-workout-model';
import { ToastService } from '../../../core/services/toast-service';

@Component({
  selector: 'app-log-workout',
  imports: [CommonModule, FormsModule, ReactiveFormsModule],
  templateUrl: './log-workout.html',
  styleUrl: './log-workout.css',
})
export class LogWorkout implements OnInit {
  workoutForm!: FormGroup;
  selectedExercise!: Exercise;
  filterType: 'bodyPart' | 'muscle' | 'equipment' = 'bodyPart';
  selectedFilter: string = '';
  searchOptions: string[] = [];
  bodyParts: string[] = [];
  muscles: string[] = [];
  equipmentList: string[] = [];
  exercises: Exercise[] = [];
  loading = false;

  constructor(
    private fb: FormBuilder,
    private workoutService: WorkoutService,
    private toastService: ToastService
  ) {}

  ngOnInit(): void {
    this.workoutForm = this.fb.group({
      // workoutType: ['', Validators.required],
      exerciseId: ['', Validators.required],
      exerciseName: ['', Validators.required],
      date: ['', Validators.required],
      sets: [null],
      reps: [null],
      weight: [null],
      duration: [null],
      // distanceKm: [null],
      notes: [''],
      searchType: ['bodyPart'],
      searchValue: [''],
    });
    this.workoutForm.get('searchType')?.valueChanges.subscribe((type) => {
      this.updateSearchOptions(type);
    });

    this.workoutService
      .getBodyPartList()
      .subscribe((data) => (this.bodyParts = data));
    this.workoutService
      .getTargetMuscleList()
      .subscribe((data) => (this.muscles = data));
    this.workoutService
      .getEquipmentList()
      .subscribe((data) => (this.equipmentList = data));
  }

  updateSearchOptions(type: string) {
    if (type === 'Body Part') {
      this.searchOptions = this.bodyParts;
    } else if (type === 'Target Muscle') {
      this.searchOptions = this.muscles;
    } else if (type === 'Equipment') {
      this.searchOptions = this.equipmentList;
    }
  }

  getOptions(): string[] {
    switch (this.filterType) {
      case 'muscle':
        return this.muscles;
      case 'equipment':
        return this.equipmentList;
      default:
        return this.bodyParts;
    }
  }

  onFilterSelect(selected: string) {
    this.selectedFilter = selected;
    this.fetchExercisesByFilter();
  }

  onWorkoutTypeChange(): void {
    const type = this.workoutForm.get('workoutType')?.value;

    // Clear irrelevant fields
    if (type === 'strength') {
      this.workoutForm.get('duration')?.reset();
      this.workoutForm.get('distanceKm')?.reset();
    } else {
      this.workoutForm.get('sets')?.reset();
      this.workoutForm.get('reps')?.reset();
      this.workoutForm.get('weight')?.reset();
    }
  }

  fetchExercisesByFilter() {
    this.loading = true;
    const type = this.workoutForm.get('searchType')?.value;
    const value = this.workoutForm.get('searchValue')?.value;

    let obs$ =
      type === 'bodyPart'
        ? this.workoutService.getBodyPartWorkouts(value)
        : type === 'muscle'
        ? this.workoutService.getMuscleWorkouts(value)
        : this.workoutService.getEquipmentWorkouts(value);

    obs$.subscribe((exs) => {
      this.exercises = exs;
      this.loading = false;
    });
  }

  selectExercise(ex: Exercise): void {
    this.selectedExercise = ex;
    this.workoutForm.patchValue({
      exerciseId: ex.id,
      exerciseName: ex.name,
    });
  }

  submit(): void {
    if (this.workoutForm.valid && this.selectedExercise) {
      const formValue = this.workoutForm.value;

      const workoutEntry: LoggedWorkout = {
        id: '00000000-0000-0000-0000-000000000000',
        userId: '00000000-0000-0000-0000-000000000000',
        exerciseId: formValue.exerciseId,
        exerciseName: formValue.exerciseName,
        date: formValue.date,
        sets: Number(formValue.sets ?? 0),
        reps: Number(formValue.reps ?? 0),
        weight: formValue.weight,
        duration: formValue.duration,
        notes: formValue.notes,
      };

      this.workoutService.createWorkout(workoutEntry).subscribe((res) => {
        console.log('Workout created:', res);
        this.toastService.successToast('Workout logged successfully!', 'Success')
      });
    }
  }
}
