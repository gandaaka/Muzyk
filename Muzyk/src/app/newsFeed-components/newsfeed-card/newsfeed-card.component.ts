import { Component, OnInit, Input } from '@angular/core';
import { User } from 'src/app/_models/user';
import { Photo } from 'src/app/_models/photo';

@Component({
  selector: 'app-newsfeed-card',
  templateUrl: './newsfeed-card.component.html',
  styleUrls: ['./newsfeed-card.component.css']
})
export class NewsfeedCardComponent implements OnInit {
  @Input()
  user: User;
  photos: Photo[];

  constructor() {
  }

  ngOnInit() {
    this.photos = this.user.photos;
  }
}
