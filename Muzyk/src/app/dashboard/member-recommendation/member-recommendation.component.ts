import { Component, OnInit, Input } from '@angular/core';
import { User } from 'src/app/_models/user';
import { AuthService } from 'src/app/_services/auth.service';
import { RecommendationService } from 'src/app/_services/recommendation.service';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { Subject } from 'rxjs';
import { Recommendation } from 'src/app/_models/recommendation';
import { UserService } from 'src/app/_services/user.service';
import { takeUntil } from 'rxjs/operators';

@Component({
  selector: 'app-member-recommendation',
  templateUrl: './member-recommendation.component.html',
  styleUrls: ['./member-recommendation.component.css']
})
export class MemberRecommendationComponent implements OnInit {
  private ngUnsubscribe = new Subject();
  recommendations: Recommendation[];
  recommendedUsers: User[] = [];

  constructor(
    private authService: AuthService,
    private recommendationService: RecommendationService,
    private userService: UserService,
    private alertify: AlertifyService
  ) {}

  ngOnInit() {
    this.loadRecommendations();
  }

  loadRecommendations() {
    this.recommendationService
      .getRecommendations(this.authService.decodedToken.nameid)
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe(
        (recommendations: Recommendation[]) => {
          this.recommendations = recommendations;
          const recommendedUserIds: any[] = [];
          this.recommendations.forEach(recommendation => {
            console.log(recommendation.userId);
            // console.log(loggedUser.id);
            if (recommendation.userId === this.authService.decodedToken.nameid) {
              recommendedUserIds.push(recommendation.relatedUser1);
              recommendedUserIds.push(recommendation.relatedUser2);
              recommendedUserIds.push(recommendation.relatedUser3);
              recommendedUserIds.push(recommendation.relatedUser4);
              recommendedUserIds.push(recommendation.relatedUser5);
            }
          });
          // console.log(this.recommendedUsers)
          recommendedUserIds.forEach(userId => {
            this.userService
              .getUser(userId)
              .pipe(takeUntil(this.ngUnsubscribe))
              .subscribe((user: User) => {
                this.recommendedUsers.push(user);
                console.log(user);
              });
          });
          console.log(this.recommendedUsers);
          // console.log(this.recommendations);
        },
        error => {
          this.alertify.error(error);
        }
      );
  }
}
