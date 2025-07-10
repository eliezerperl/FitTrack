import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Exercise } from '../../../core/models/exercise-model';

@Component({
  selector: 'app-exercise-card',
  imports: [],
  templateUrl: './exercise-card.html',
  styleUrl: './exercise-card.css'
})
export class ExerciseCard {
  @Input() exercise!: Exercise;
  @Output() viewDetails = new EventEmitter<void>();
}
