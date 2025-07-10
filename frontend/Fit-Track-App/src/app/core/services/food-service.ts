import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class FoodService {
  constructor(private http: HttpClient) {}

  searchFoods(food: string) {
    return this.http.get(`${environment.apiUrl}/food/${food}`);
  }
}
