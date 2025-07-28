import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoggedWorkout } from '../../../../core/models/log-workout-model';

@Component({
  selector: 'app-workout-history-modal',
  imports: [CommonModule],
  templateUrl: './workout-history-modal.html',
  styleUrl: './workout-history-modal.css'
})
export class WorkoutHistoryModal {
  @Input() exercises: LoggedWorkout[] = [];
  @Input() visible: boolean = false;
  @Input() onClose: () => void = () => {};
}
