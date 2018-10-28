import { Component, OnInit, Input } from '@angular/core';
import { User } from 'src/app/_models/user';
import { Photo } from 'src/app/_models/photo';
import { AlertifyService } from 'src/app/_services/alertify.service';

@Component({
  selector: 'app-newsfeed-card',
  templateUrl: './newsfeed-card.component.html',
  styleUrls: ['./newsfeed-card.component.css']
})
export class NewsfeedCardComponent implements OnInit {
  @Input() user: User;
  @Input() photos: Photo[];

  constructor() {}

  ngOnInit() {}
}
