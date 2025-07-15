import { NutrientEntry } from './food-nutrient-model';

export interface LoggedFood {
  foodId: string;
  userId: string;
  foodName: string;
  quantity: number;
  nutrients: NutrientEntry[];
  date: Date;
  notes?: string;
}
