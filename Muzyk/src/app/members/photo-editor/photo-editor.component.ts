import { Component, OnInit, Input, EventEmitter, Output } from '@angular/core';
import { FileUploader } from 'ng2-file-upload';
import { Photo } from '../../_models/photo';
import { environment } from '../../../environments/environment';
import { AuthService } from '../../_services/auth.service';
import { UserService } from '../../_services/user.service';
import { AlertifyService } from '../../_services/alertify.service';

@Component({
  selector: 'app-photo-editor',
  templateUrl: './photo-editor.component.html',
  styleUrls: ['./photo-editor.component.css']
})
export class PhotoEditorComponent implements OnInit {
  @Input() photos: Photo[];
  @Output() getMemberPhotoChange = new EventEmitter<string>();

  uploader: FileUploader;
  hasBaseDropZoneOver = false;
  baseUrl = environment.apiUrl;
  currentProfilePhoto: Photo;

  constructor(
    private authService: AuthService,
    private userService: UserService,
    private alertify: AlertifyService
  ) {}

  ngOnInit() {
    this.initializeUploader();
  }

  fileOverBase(e: any): void {
    this.hasBaseDropZoneOver = e;
  }

  initializeUploader() {
    this.uploader = new FileUploader({
      url:
        this.baseUrl +
        'users/' +
        this.authService.decodedToken.nameid +
        '/photos',
      authToken: 'Bearer ' + localStorage.getItem('token'),
      isHTML5: true,
      allowedFileType: ['image'],
      removeAfterUpload: true,
      autoUpload: false,
      maxFileSize: 100 * 1024 * 1024
    });

    this.uploader.onAfterAddingFile = file => {
      file.withCredentials = false;
    };

    this.uploader.onSuccessItem = (item, response, status, headers) => {
      if (response) {
        const res: Photo = JSON.parse(response);
        const photo = {
          id: res.id,
          photoUrl: res.photoUrl,
          dateAdded: res.dateAdded,
          description: res.description,
          isProfilePhoto: res.isProfilePhoto,
          isCoverPhoto: res.isCoverPhoto
        };
        this.photos.push(photo);
      }
    };
  }

  setProfilePhoto(photo: Photo) {
    this.userService
      .setProfilePhoto(this.authService.decodedToken.nameid, photo.id)
      .subscribe(
        () => {
          this.currentProfilePhoto = this.photos.filter(
            p => p.isProfilePhoto === true
          )[0];
          this.currentProfilePhoto.isProfilePhoto = false;
          photo.isProfilePhoto = true;
          this.getMemberPhotoChange.emit(photo.photoUrl);
        },
        error => {
          this.alertify.error('Photo could not be set as Profile Photo');
        }
      );
  }
}
