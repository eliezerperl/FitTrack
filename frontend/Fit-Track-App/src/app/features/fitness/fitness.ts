import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LogWorkout } from './log-workout/log-workout';
import { SearchWorkouts } from './search-workouts/search-workouts';

@Component({
  selector: 'app-fitness',
  imports: [CommonModule, LogWorkout, SearchWorkouts],
  templateUrl: './fitness.html',
  styleUrl: './fitness.css',
})
export class Fitness {
  currentTab: 'search' | 'log' = 'search';
}
