import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Booking } from '../_models/booking';

@Injectable({
  providedIn: 'root'
})
export class BookingService {
  baseUrl = environment.apiUrl;

constructor(private http: HttpClient) { }

  bookMeeting(id: number, bookingData: Booking) {
    return this.http.post(this.baseUrl + 'users/' + id + '/booking/' + bookingData.recipientId, bookingData);
  }
}
