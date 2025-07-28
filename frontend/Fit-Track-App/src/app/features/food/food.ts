import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-food',
  imports: [CommonModule, RouterModule],
  templateUrl: './food.html',
  styleUrl: './food.css'
})
export class Food {
}
