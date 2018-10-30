import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Recommendation } from '../_models/recommendation';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class RecommendationService {

  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) {}

  getRecommendations(userId: number): Observable<Recommendation[]> {
    return this.http.get<Recommendation[]>(this.baseUrl + 'users/' + userId + '/recommendedUsers');
  }

  getRecommendation(userId: number, id: number): Observable<Recommendation> {
    return this.http.get<Recommendation>(this.baseUrl + 'users/' + userId + '/recommendedUsers/' + id);
  }
}
