import { Component, OnInit } from '@angular/core';
import { User } from '../_models/user';
import { ActivatedRoute } from '@angular/router';
import { UserService } from '../_services/user.service';
import { Pagination, PaginatedResult } from '../_models/pagination';
import { AlertifyService } from '../_services/alertify.service';

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
