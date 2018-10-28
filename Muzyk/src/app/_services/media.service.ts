import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Video } from '../_models/video';

@Injectable({
  providedIn: 'root'
})
export class MediaService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) {}

  setProfilePhoto(userId: number, id: number) {
    return this.http.post(
      this.baseUrl + 'users/' + userId + '/photos/' + id + '/setMain',
      {}
    );
  }

  setCoverPhoto(userId: number, id: number) {
    return this.http.post(
      this.baseUrl + 'users/' + userId + '/photos/' + id + 'setCover',
      {}
    );
  }

  deletePhoto(userId: number, id: number) {
    return this.http.delete(this.baseUrl + 'users/' + userId + '/photos/' + id);
  }

  uploadVideo(userId: number, video: Video) {
    return this.http.post(this.baseUrl + 'users/' + userId + '/videos/', video);
  }

  deleteVideo(userId: number, id: number) {
    return this.http.delete(this.baseUrl + 'users/' + userId + '/videos/' + id);
  }
}
