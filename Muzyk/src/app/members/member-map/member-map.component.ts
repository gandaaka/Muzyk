import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-member-map',
  templateUrl: './member-map.component.html',
  styleUrls: ['./member-map.component.css']
})
export class MemberMapComponent implements OnInit {
  lat = 43.6532;
  lng = -79.3832;
  location: string;
  constructor() { }

  ngOnInit() {
  }

}
