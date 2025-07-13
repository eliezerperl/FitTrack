import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { Food } from '../models/food-model';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class FoodService {
  constructor(private http: HttpClient) {}

  searchFoods(food: string): Observable<Food[]> {
    return this.http.get<Food[]>(`${environment.apiUrl}/food/${food}`);
  }
}
