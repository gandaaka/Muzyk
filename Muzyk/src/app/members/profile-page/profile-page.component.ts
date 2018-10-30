import { Component, OnInit, ViewChild } from '@angular/core';
import { User } from '../../_models/user';
import { UserService } from '../../_services/user.service';
import { AlertifyService } from '../../_services/alertify.service';
import { ActivatedRoute } from '@angular/router';
import { NgxGalleryOptions, NgxGalleryImage, NgxGalleryAnimation, NgxGalleryAction } from 'ngx-gallery';
import { AuthService } from '../../_services/auth.service';
import { TabsetComponent, TabDirective } from 'ngx-bootstrap';

@Component({
  selector: 'app-profile-page',
  templateUrl: './profile-page.component.html',
  styleUrls: ['./profile-page.component.css']
})
export class ProfilePageComponent implements OnInit {
  @ViewChild('memberTabs') memberTabs: TabsetComponent;
  user: User;
  galleryOptions: NgxGalleryOptions[];
  galleryImages: NgxGalleryImage[];
  galleryActions: NgxGalleryAction[];
  tabs: any[];

  constructor(
    private userService: UserService,
    private alertify: AlertifyService,
    private route: ActivatedRoute,
    public authService: AuthService
  ) {}

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.user = data['user'];
    });

    this.route.queryParams.subscribe(params => {
      const selectedTab = params['tab'];
      this.memberTabs.tabs[selectedTab > 0 ? selectedTab : 0].active = true;
    });
    // photo gallery code
    this.galleryOptions = [
      {
        width: '700px',
        height: '500px',
        imagePercent: 85,
        thumbnailsColumns: 4,
        imageAnimation: NgxGalleryAnimation.Fade,
        preview: true
      }
    ];
    this.galleryImages = this.getImages();
  }

  getImages() {
    const imageUrls = [];
    for (let i = 0; i < this.user.photos.length; i++) {
      imageUrls.push({
        small: this.user.photos[i].mediaUrl,
        medium: this.user.photos[i].mediaUrl,
        big: this.user.photos[i].mediaUrl,
        description: this.user.photos[i].description
      });
    }
    return imageUrls;
  }

  selectTab(tabId: number) {
    this.memberTabs.tabs[tabId].active = true;
  }

  sendFollowRequest(id: number) {
    this.userService.sendFollow(this.authService.decodedToken.nameid, id).subscribe(data => {
      this.alertify.success('You are now following ' + this.user.firstName);
    }, error => {
      this.alertify.error(error);
    });
  }
}
