import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { Exercise } from '../models/exercise-model';
import { LoggedWorkout } from '../models/log-workout-model';

@Injectable({
  providedIn: 'root',
})
export class WorkoutService {
  private workoutApiUrl: string = `${environment.apiUrl}/workout`;

  constructor(private http: HttpClient) {}

  // CRUD Operations
  createWorkout(loggedWorkout: LoggedWorkout): Observable<LoggedWorkout> {
    return this.http.post<LoggedWorkout>(`${this.workoutApiUrl}`, loggedWorkout);
  }
  getAllWorkouts(): Observable<LoggedWorkout[]> {
    return this.http.get<LoggedWorkout[]>(`${this.workoutApiUrl}`);
  }
  getWorkoutById(id: string): Observable<LoggedWorkout> {
    return this.http.get<LoggedWorkout>(`${this.workoutApiUrl}/${id}`);
  }
  getWorkoutByUserId(userId: string): Observable<LoggedWorkout[]> {
    return this.http.get<LoggedWorkout[]>(`${this.workoutApiUrl}/user/${userId}`);
  }
  updateWorkout(exercise: LoggedWorkout): Observable<LoggedWorkout> {
    return this.http.post<LoggedWorkout>(`${this.workoutApiUrl}`, exercise);
  }
  deleteWorkout(id: string): Observable<void> {
    return this.http.delete<void>(`${this.workoutApiUrl}/${id}`);
  }

  // Target Muscle List
  getTargetMuscleList(): Observable<string[]> {
    return this.http.get<string[]>(`${this.workoutApiUrl}/musclelist`);
  }
  // Muscle Workouts
  getMuscleWorkouts(muscle: string): Observable<Exercise[]> {
    return this.http.get<Exercise[]>(`${this.workoutApiUrl}/muscle/${muscle}`);
  }

  // Body part List
  getBodyPartList(): Observable<string[]> {
    return this.http.get<string[]>(`${this.workoutApiUrl}/bodypartlist`);
  }
  // Body part Workouts
  getBodyPartWorkouts(bodyPart: string): Observable<Exercise[]> {
    return this.http.get<Exercise[]>(
      `${this.workoutApiUrl}/bodypart/${bodyPart}`
    );
  }

  // Equipment List
  getEquipmentList(): Observable<string[]> {
    return this.http.get<string[]>(`${this.workoutApiUrl}/equipmentlist`);
  }
  // Equipment workouts
  getEquipmentWorkouts(equipment: string): Observable<Exercise[]> {
    return this.http.get<Exercise[]>(
      `${this.workoutApiUrl}/equipment/${equipment}`
    );
  }

  // Get workout image
  getWorkoutDemoImage(exerciseId: string) {
    return this.http.get(`${this.workoutApiUrl}/image/${exerciseId}`);
  }
}
