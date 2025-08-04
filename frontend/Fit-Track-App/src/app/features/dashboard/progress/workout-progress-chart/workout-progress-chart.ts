import { AfterViewInit, Component, ElementRef, Input, OnChanges, ViewChild } from '@angular/core';
import { Chart, ChartConfiguration, registerables  } from 'chart.js';
import { LoggedWorkout } from '../../../../core/models/log-workout-model';
Chart.register(...registerables);

@Component({
  selector: 'app-workout-progress-chart',
  imports: [],
  templateUrl: './workout-progress-chart.html',
  styleUrl: './workout-progress-chart.css'
})
export class WorkoutProgressChart implements AfterViewInit, OnChanges {
  @Input() workoutEntries: LoggedWorkout[] = [];
  @ViewChild('canvas', { static: false }) canvasRef!: ElementRef<HTMLCanvasElement>;
  chart: Chart | undefined;

  ngAfterViewInit() {
    this.renderChart();
  }

  ngOnChanges() {
    if (this.chart) {
      this.chart.destroy();
      this.renderChart();
    }
  }

  renderChart() {
    if (!this.canvasRef) return;

    const ctx = this.canvasRef.nativeElement.getContext('2d');
    if (!ctx) return;

    const dateCountMap = new Map<string, number>();
    this.workoutEntries.forEach(entry => {
      const dateStr = new Date(entry.date).toLocaleDateString();
      dateCountMap.set(dateStr, (dateCountMap.get(dateStr) || 0) + 1);
    });

    const labels = Array.from(dateCountMap.keys()).sort(
      (a, b) => new Date(a).getTime() - new Date(b).getTime()
    );
    const data = labels.map(label => dateCountMap.get(label) || 0);

    const config: ChartConfiguration<'bar'> = {
      type: 'bar',
      data: {
        labels,
        datasets: [{
          label: 'Workout Entries',
          data,
          borderColor: 'blue',
          backgroundColor: 'rgba(0, 0, 255, 0.1)',
        }]
      },
      options: {
        responsive: true,
        scales: {
          x: { title: { display: true, text: 'Date' } },
          y: { title: { display: true, text: 'Workouts Logged' }, beginAtZero: true }
        }
      }
    };

    this.chart = new Chart(ctx, config);
  }
}
