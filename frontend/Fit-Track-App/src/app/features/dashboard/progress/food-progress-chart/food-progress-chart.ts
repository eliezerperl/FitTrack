import { AfterViewInit, Component, ElementRef, Input, OnChanges, ViewChild } from '@angular/core';
import { Chart, ChartConfiguration } from 'chart.js';
import { LoggedFood } from '../../../../core/models/log-food-model';

@Component({
  selector: 'app-food-progress-chart',
  imports: [],
  templateUrl: './food-progress-chart.html',
  styleUrl: './food-progress-chart.css'
})
export class FoodProgressChart implements AfterViewInit, OnChanges {
  @Input() foodEntries: LoggedFood[] = [];
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
    this.foodEntries.forEach(entry => {
      const date = new Date(entry.dateLogged).toLocaleDateString();
      dateCountMap.set(date, (dateCountMap.get(date) || 0) + 1);
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
          label: 'Food Entries',
          data,
          borderColor: 'blue',
          backgroundColor: 'rgba(0, 0, 255, 0.1)',
        }]
      },
      options: {
        responsive: true,
        scales: {
          x: { title: { display: true, text: 'Date' } },
          y: { title: { display: true, text: 'Food Entries' }, beginAtZero: true }
        }
      }
    };

    this.chart = new Chart(ctx, config);
  }
}
