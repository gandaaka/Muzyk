import { Component, OnInit, Input } from '@angular/core';
import { User } from 'src/app/_models/user';
import { AuthService } from 'src/app/_services/auth.service';
import { UserService } from 'src/app/_services/user.service';
import { AlertifyService } from 'src/app/_services/alertify.service';

@Component({
  selector: 'app-member-recommendation-card',
  templateUrl: './member-recommendation-card.component.html',
  styleUrls: ['./member-recommendation-card.component.css']
})
export class MemberRecommendationCardComponent implements OnInit {
  @Input() user: User;

  constructor(
    private authService: AuthService,
    private userService: UserService,
    private alertify: AlertifyService
  ) { }

  ngOnInit() {
  }

  sendFollowRequest(id: number) {
    this.userService.sendFollow(this.authService.decodedToken.nameid, id).subscribe(data => {
      this.alertify.success('You are now following ' + this.user.firstName);
    }, error => {
      this.alertify.error(error);
    });
  }
}
