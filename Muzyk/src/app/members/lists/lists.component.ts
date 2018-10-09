import { Component, OnInit, Input } from '@angular/core';
import { User } from '../../_models/user';
import { UserService } from '../../_services/user.service';
import { AlertifyService } from '../../_services/alertify.service';
import { ActivatedRoute } from '@angular/router';
import { Pagination, PaginatedResult } from 'src/app/_models/pagination';

@Component({
  selector: 'app-lists',
  templateUrl: './lists.component.html',
  styleUrls: ['./lists.component.css']
})
export class ListsComponent implements OnInit {
  users: User[];
  user: User = JSON.parse(localStorage.getItem('user'));
  genreList = [
    { value: 'Blues', display: 'Blues' },
    { value: 'Rock', display: 'Rock' },
    { value: 'Classical', display: 'Classical Music' },
    { value: 'Country', display: 'Country' },
    { value: 'Dance', display: 'Dance' },
    { value: 'Jazz', display: 'Jazz' }
  ];
  userParams: any = {};
  pagination: Pagination;
  defaultGenre = 'None';

  constructor(
    private userService: UserService,
    private alertify: AlertifyService,
    private route: ActivatedRoute
  ) {}

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.users = data['users'].result;
      this.pagination = data['users'].pagination;
    });
    this.userParams.minExp = 0;
    this.userParams.maxExp = 25;
    this.userParams.orderBy = 'created';
  }

  pageChanged(event: any): void {
    this.pagination.currentPage = event.page;
    this.loadUsers();
  }

  resetFilters() {
    this.userParams.genre = this.defaultGenre = 'None';
    this.userParams.minExp = 0;
    this.userParams.maxExp = 25;
    this.loadUsers();
  }

  loadUsers() {
    this.userService
      .getUsers(
        this.pagination.currentPage,
        this.pagination.itemsPerPage,
        this.userParams
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
  }

  selectGenre(genre: String) {
    let selectedGenre: String;
    switch (genre) {
      case 'Rock':
        selectedGenre = genre;
        break;
      case 'Blues':
        selectedGenre = genre;
        break;
      case 'Jazz':
        selectedGenre = genre;
        break;
      case 'Classical':
        selectedGenre = genre;
        break;
      case 'Country':
        selectedGenre = genre;
        break;
      case 'Dance':
        selectedGenre = genre;
        break;
      case 'None':
        selectedGenre = '';
        break;
      default:
        selectedGenre = '';
        break;
    }
    this.userParams.genre = selectedGenre;
  }
}
