import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { Food } from '../models/food-model';
import { Observable } from 'rxjs';
import { LoggedFood } from '../models/log-food-model';

@Injectable({
  providedIn: 'root',
})
export class FoodService {
  private apiUrl: string = `${environment.apiUrl}/food`;

  constructor(private http: HttpClient) {}

  searchFoods(food: string): Observable<Food[]> {
    return this.http.get<Food[]>(`${this.apiUrl}/${food}`);
  }

  createFoodEntry(foodEntry: LoggedFood): Observable<LoggedFood> {
    return this.http.post<LoggedFood>(`${this.apiUrl}`, foodEntry);
  }

  getAllFoodEntries() {}

  getFoodEntryById() {}

  getFoodEntriesByUserId() {}

  updateFoodEntry() {}

  deleteFoodEntry() {}
}
