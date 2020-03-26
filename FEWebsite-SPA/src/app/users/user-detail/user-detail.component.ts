import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { NgxGalleryAnimation, NgxGalleryImage, NgxGalleryOptions } from '@kolkov/ngx-gallery';
import { TabsetComponent } from 'ngx-bootstrap';
import { UserService } from '../../_services/user.service';
import { AuthService } from '../../_services/auth.service';
import { AlertifyService } from '../../_services/alertify.service';
import { StatusCodeResultReturnObject } from '../../_models/statusCodeResultReturnObject';
import { User } from '../../_models/user';


@Component({
  selector: 'app-user-detail',
  templateUrl: './user-detail.component.html',
  styleUrls: ['./user-detail.component.css']
})
export class UserDetailComponent implements OnInit {
  @ViewChild('userTabset', { static: true }) userTabset: TabsetComponent;
  user: User;
  loggedInUser: User = JSON.parse(localStorage.getItem('user'));
  loggedInUserId = Number(this.authService.decodedToken.nameid);
  userLiked: boolean;
  userDesc: string[];
  galleryOptions: NgxGalleryOptions[];
  galleryImages: NgxGalleryImage[];

  constructor(private userService: UserService,
              private authService: AuthService,
              private alertify: AlertifyService,
              private route: ActivatedRoute) { }

  ngOnInit() {
    this.route.data.subscribe({
      next: data => {
        this.user = data.user as User;
        if (this.user.description) {
          this.userDesc = this.user.description.split('\n');
        }
      }
    });

    this.getUserLikes();
    this.route.queryParams.subscribe({
      next: params => {
        const selectedTab = Number(params.tab > 0 ? params.tab : 0);
        this.selectTab(selectedTab);
      }
    });

    this.galleryOptions = [
      {
        width: '500px',
        height: '500px',
        imagePercent: 100,
        thumbnailsColumns: 4,
        imageAnimation: NgxGalleryAnimation.Slide,
        // preview: false
      }
    ];

    this.galleryImages = this.getImages();
  }

  getImages() {
    const imageUrls: NgxGalleryImage[] = [];
    for (const photo of this.user.photos) {
      imageUrls.push({
        small: photo.url,
        medium: photo.url,
        big: photo.url,
        description: photo.description,
      });
    }

    return imageUrls;
  }

  selectTab(tabId: number) {
    if (this.userTabset.tabs.length > 0) {
      this.userTabset.tabs[tabId].active = true;
    }
  }

  toggleLike() {
    this.userService.toggleLike(this.loggedInUserId, this.user.id)
    .subscribe({
      next: (next: StatusCodeResultReturnObject) => {
        this.userLiked = !this.userLiked;
        this.alertify.success(next.response);
      },
      error: (error: StatusCodeResultReturnObject) => {
        this.alertify.error(error.response);
      }
    });
  }

  isUserLiked() {
    return this.userLiked;
  }

  private getUserLikes() {
    this.userService.getLikes(this.loggedInUserId)
    .subscribe({
      next: likes => {
        this.loggedInUser.listOfLikees = likes;
        this.userLiked = this.loggedInUser.listOfLikees.includes(this.user.id);
      }
    });
  }
}
