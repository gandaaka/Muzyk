import { Component, OnInit, Input, ViewChild } from '@angular/core';
import { FileUploader } from 'ng2-file-upload';
import { environment } from 'src/environments/environment.prod';
import { AuthService } from 'src/app/_services/auth.service';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { Video } from 'src/app/_models/video';

import { NgForm } from '@angular/forms';
import { DomSanitizer } from '@angular/platform-browser';

@Component({
  selector: 'app-video-editor',
  templateUrl: './video-editor.component.html',
  styleUrls: ['./video-editor.component.css']
})
export class VideoEditorComponent implements OnInit {
  @ViewChild('videoForm')
  @Input() videos: Video[];

  videoForm: NgForm;
  uploader: FileUploader;
  hasBaseDropZoneOver = false;
  baseUrl = environment.apiUrl;

  constructor(
    private authService: AuthService,
    private alertify: AlertifyService,
    private santizer: DomSanitizer
  ) { }

  ngOnInit() {
    this.initializeUploader();
  }

  uploadVideo() {
    this.videoForm.reset();
  }

  // video upload - cloudinary
  initializeUploader() {
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
  }
}
