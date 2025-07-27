export interface LoggedWorkout {
  id?: string,
  userId: string,
  exerciseId: string;
  exerciseName: string;
  date: Date;
  sets?: number;
  reps?: number;
  weight?: number;
  duration?: string;
  notes?: string;
}
