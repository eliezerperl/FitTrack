import { FoodNutrient } from "./food-nutrient-model";

export interface Food {
  fdcId: number;
  description: string;
  dataType: string;
  foodCategory: string;
  foodCategoryId: number;
  foodCode: number;
  commonNames: string;
  additionalDescriptions: string;
  publishedDate: string;
  score: number;
  allHighlightFields: string;

  foodNutrients: FoodNutrient[];

  finalFoodInputFoods?: any[];
  foodAttributes?: any[];
  foodAttributeTypes?: any[];
  foodMeasures?: any[];
  foodVersionIds?: any[];
  microbes?: any[];
}