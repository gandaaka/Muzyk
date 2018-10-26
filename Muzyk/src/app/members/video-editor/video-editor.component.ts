import { Component, OnInit, Input, ViewChild } from '@angular/core';
import { FileUploader } from 'ng2-file-upload';
import { environment } from 'src/environments/environment.prod';
import { AuthService } from 'src/app/_services/auth.service';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { Video } from 'src/app/_models/video';

import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { UserService } from 'src/app/_services/user.service';

@Component({
  selector: 'app-video-editor',
  templateUrl: './video-editor.component.html',
  styleUrls: ['./video-editor.component.css']
})
export class VideoEditorComponent implements OnInit {
  @ViewChild('videoForm')
  @Input()
  videos: Video[];

  videoForm: FormGroup;
  mediaUrl: string;
  desc: string;

  uploader: FileUploader;
  hasBaseDropZoneOver = false;
  baseUrl = environment.apiUrl;

  constructor(
    private authService: AuthService,
    private userService: UserService,
    private alertify: AlertifyService,
    private builder: FormBuilder
  ) {}

  ngOnInit() {
    // this.initializeUploader();
    this.createVideoForm();
  }

  createVideoForm() {
    this.videoForm = this.builder.group({
      mediaUrl: ['', Validators.required],
      description: ['']
    });
  }

  uploadVideo() {
    if (this.videoForm.valid) {
      const video: Video = Object.assign({}, this.videoForm.value);
      this.videos.push(video);
      this.userService
        .uploadVideo(this.authService.decodedToken.nameid, video)
        .subscribe(
          next => {
            this.alertify.success('Video uploaded successully');
          },
          error => {
            this.alertify.error(error);
          }
        );
      this.videoForm.reset();
    }
  }

  // video upload - cloudinary
  /*   initializeUploader() {
    this.uploader = new FileUploader({
      url:
        this.baseUrl +
        'users/' +
        this.authService.decodedToken.nameid +
        '/videos',
      authToken: 'Bearer ' + localStorage.getItem('token'),
      isHTML5: true,
      allowedFileType: ['video'],
      removeAfterUpload: true,
      autoUpload: false,
      maxFileSize: 200 * 1024 * 1024
    });

    this.uploader.onAfterAddingFile = file => {
      file.withCredentials = false;
    };

    this.uploader.onSuccessItem = (item, response, status, headers) => {
      if (response) {
        // const res: Video = JSON.parse(response);
        this.alertify.success('Video Uploaded Successfully');
      }
    };
  }

  fileOverBase(e: any): void {
    this.hasBaseDropZoneOver = e;
  } */
}
