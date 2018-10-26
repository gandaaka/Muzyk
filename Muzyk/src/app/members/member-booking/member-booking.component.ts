import { Component, OnInit, Input, ViewChild } from '@angular/core';
import { Booking } from 'src/app/_models/booking';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { AuthService } from 'src/app/_services/auth.service';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { BookingService } from 'src/app/_services/booking.service';

@Component({
  selector: 'app-member-booking',
  templateUrl: './member-booking.component.html',
  styleUrls: ['./member-booking.component.css']
})
export class MemberBookingComponent implements OnInit {
  @ViewChild('bookingForm')
  @Input()
  recipientId: number;
  booking: Booking;
  bookingForm: FormGroup;

  constructor(
    private bookingService: BookingService,
    private authService: AuthService,
    private alertify: AlertifyService,
    private builder: FormBuilder
  ) {}

  ngOnInit() {
    this.createBoookingForm();
  }

  createBoookingForm() {
    this.bookingForm = this.builder.group({
      title: ['', Validators.required],
      desc: [''],
      date: ['', Validators.required]
    });
  }

  bookMeeting() {
    if (this.bookingForm.valid) {
      this.booking = Object.assign({}, this.bookingForm.value);
      this.booking.recipientId = this.recipientId;
      console.log(this.booking);
      console.log(this.authService.decodedToken.nameid);
      this.bookingService
        .bookMeeting(this.authService.decodedToken.nameid, this.booking)
        .subscribe(
          next => {
            this.alertify.success('Your meeting is booked successfully');
            this.bookingForm.reset(this.booking);
          },
          error => {
            this.alertify.error(error);
          }
        );
    }
  }
}
