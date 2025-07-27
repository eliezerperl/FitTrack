import { LoggedFood } from './log-food-model';
import { LoggedWorkout } from './log-workout-model';

export interface User {
  id: string;

  userName: string;

  passwordHash: string;

  passwordSalt: string;

  workoutEntries: LoggedWorkout[];

  foodEntries: LoggedFood[];
}
