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
import { SharedService } from '../../../core/services/shared-service';

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
  userId: string | null = null;

  constructor(
    private fb: FormBuilder,
    private workoutService: WorkoutService,
    private sharedService: SharedService,
    private toastService: ToastService
  ) {}

  ngOnInit(): void {
    this.sharedService.userId$.subscribe((id) => (this.userId = id));

    this.workoutForm = this.fb.group({
      exerciseId: ['', Validators.required],
      exerciseName: ['', Validators.required],
      date: ['', Validators.required],
      sets: [null],
      reps: [null],
      weight: [null],
      duration: [null],
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
        // id: '00000000-0000-0000-0000-000000000000',
        userId: this.userId ?? '00000000-0000-0000-0000-000000000000',
        exerciseId: formValue.exerciseId,
        exerciseName: formValue.exerciseName,
        date: formValue.date,
        sets: Number(formValue.sets ?? 0),
        reps: Number(formValue.reps ?? 0),
        weight: formValue.weight,
        duration: this.formatDuration(formValue.duration),
        notes: formValue.notes,
      };

      this.workoutService.createWorkout(workoutEntry).subscribe((res) => {
        console.log('Workout created:', res);
        this.toastService.successToast(
          'Workout logged successfully!',
          'Success'
        );
      });
    }
  }
  formatDuration(input: string): string {
    if (!input.includes(':')) {
      return `00:${input.padStart(2, '0')}:00`;
    }
    const parts = input.split(':');
    if (parts.length === 2) {
      return `00:${parts[0].padStart(2, '0')}:${parts[1].padStart(2, '0')}`;
    }
    return input;
  }
}
