import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { Pagination, PaginatedResult } from '../_models/pagination';
import { User } from '../_models/user';
import { UserService } from '../_services/user.service';
import { ActivatedRoute } from '@angular/router';
import { AlertifyService } from '../_services/alertify.service';

@Component({
  selector: 'app-follower-list',
  templateUrl: './follower-list.component.html',
  styleUrls: ['./follower-list.component.css']
})
export class FollowerListComponent implements OnInit {
  users: User[];
  pagination: Pagination;
  followsParam: string;
  tFollowers: number;
  tFollowees: number;

  constructor(
    private userService: UserService,
    private route: ActivatedRoute,
    private alertify: AlertifyService,
  ) {}

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.users = data['user'].result;
      this.pagination = data['user'].pagination;
    });
    this.followsParam = 'Followers';
    this.loadUsers();
  }

  loadUsers() {
    this.userService
      .getUsers(
        this.pagination.currentPage,
        this.pagination.itemsPerPage,
        null,
        this.followsParam
      )
      .subscribe(
        (res: PaginatedResult<User[]>) => {
          this.users = res.result;
          this.pagination = res.pagination;
        },
        error => {
          this.alertify.error(error);
        }
      );
      if (this.followsParam === 'Followers') {
        this.tFollowers = this.pagination.totalItems;
      } else {
        this.tFollowees = this.pagination.totalItems;
      }
  }

  pageChanged(event: any): void {
    this.pagination.currentPage = event.page;
    this.loadUsers();
  }
}
