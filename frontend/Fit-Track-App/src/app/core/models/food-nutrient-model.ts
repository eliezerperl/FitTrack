export interface FoodNutrient {
  foodNutrientId: number;
  indentLevel: number;
  nutrientId: number;
  nutrientName: string;
  nutrientNumber: string;
  rank: number;
  unitName: string;
  value: number;
}

export interface NutrientEntry {
  name: string;
  unit: string;
  value: number;
}