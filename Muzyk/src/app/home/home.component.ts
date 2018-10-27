import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { AuthService } from '../_services/auth.service';
import { AlertifyService } from '../_services/alertify.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  registerMode = false;
  model: any = {};

  constructor(
    private http: HttpClient,
    private authService: AuthService,
    private alertify: AlertifyService,
    private router: Router
    ) {}

  ngOnInit() {}

  registerToggle() {
    this.registerMode = true;
  }
  cancelRegisterMode(registerMode: boolean) {
    this.registerMode = registerMode;
  }

  login() {
    this.authService.login(this.model).subscribe(
      next => {
        this.alertify.success('logged in successfully');
      },
      error => {
        this.alertify.error(error);
      },
      () => {
        this.router.navigate(['/newsfeed']);
      }
    );
  }

  loggedIn() {
    // this.router.navigate(['/newsfeed']);
    return this.authService.checkLoggedIn();
  }

  logout() {
    localStorage.removeItem('token');
    localStorage.removeItem('user');

    this.authService.decodedToken = null;
    this.authService.currentUser = null;

    this.alertify.message('logged out');
    this.router.navigate(['home']);
  }
}
