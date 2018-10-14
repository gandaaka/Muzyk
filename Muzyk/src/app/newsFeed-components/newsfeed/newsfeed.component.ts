import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { UserService } from '../../_services/user.service';
import { Pagination, PaginatedResult } from '../../_models/pagination';
import { AlertifyService } from '../../_services/alertify.service';
import { User } from '../../_models/user';
import { TabsetComponent } from 'ngx-bootstrap';

@Component({
  selector: 'app-newsfeed',
  templateUrl: './newsfeed.component.html',
  styleUrls: ['./newsfeed.component.css']
})
export class NewsfeedComponent implements OnInit {
  users: User[];
  pagination: Pagination;
  followsParam: string;

  constructor(
    private route: ActivatedRoute,
    private userService: UserService,
    private alertify: AlertifyService
  ) {}

  ngOnInit() {}
}
