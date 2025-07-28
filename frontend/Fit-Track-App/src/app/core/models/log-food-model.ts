import { FoodNutrient, NutrientEntry } from './food-nutrient-model';

export interface LoggedFood {
  id: string;
  foodId: string;
  userId: string;
  foodName: string;
  quantity: number;
  nutrients: NutrientEntry[];
  dateLogged: Date;
  notes?: string;
}
