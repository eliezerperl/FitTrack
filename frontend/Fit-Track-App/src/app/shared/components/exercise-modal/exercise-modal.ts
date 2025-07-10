import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Exercise } from '../../../core/models/exercise-model';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-exercise-modal',
  imports: [CommonModule],
  templateUrl: './exercise-modal.html',
  styleUrl: './exercise-modal.css'
})
export class ExerciseModal {
  @Input() exercise!: Exercise;
  @Output() close = new EventEmitter<void>();
}
