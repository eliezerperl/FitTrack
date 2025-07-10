export interface Exercise {
  id: string;
  name: string;
  bodyPart: string;
  target: string;
  equipment: string;
  description: string;
  difficulty: string;
  category: string;
  secondaryMuscles: string[];
  instructions: string[];
}