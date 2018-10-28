import { Component, OnInit, Input } from '@angular/core';
import { User } from 'src/app/_models/user';
import { Video } from 'src/app/_models/video';

@Component({
  selector: 'app-newsfeed-video-card',
  templateUrl: './newsfeed-video-card.component.html',
  styleUrls: ['./newsfeed-video-card.component.css']
})
export class NewsfeedVideoCardComponent implements OnInit {
  @Input() videos: Video[];
  @Input() user: User;

  constructor() { }

  ngOnInit() {
  }

}
