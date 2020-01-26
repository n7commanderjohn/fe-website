import { Component, OnInit, Input } from '@angular/core';
import { AuthService } from './../../_services/auth.service';
import { AlertifyService } from './../../_services/alertify.service';
import { UserService } from 'src/app/_services/user.service';
import { environment } from 'src/environments/environment';
import { Photo } from 'src/app/_models/photo';
import { FileUploader } from 'ng2-file-upload';

@Component({
  selector: 'app-photo-editor',
  templateUrl: './photo-editor.component.html',
  styleUrls: ['./photo-editor.component.css']
})
export class PhotoEditorComponent implements OnInit {
  @Input() photos: Photo[];
  uploader: FileUploader;
  hasBaseDropZoneOver = false;
  response: string;
  baseUrl = environment.apiUrl;

  constructor(private authService: AuthService,
              private userService: UserService,
              private alertify: AlertifyService) {
  }

  ngOnInit() {
    this.initializeUploader();
  }

  fileOverBase(e: any): void {
    this.hasBaseDropZoneOver = e;
  }

  initializeUploader() {
    this.uploader = new FileUploader({
      url: this.baseUrl + 'users/' + this.authService.decodedToken.nameid + '/photos',
      authToken: 'Bearer ' + localStorage.getItem('token'),
      isHTML5: true,
      allowedFileType: ['image'],
      autoUpload: false,
      maxFileSize: 10 * 1024 * 1024,
    });

    this.hasBaseDropZoneOver = false;
    this.response = '';
    this.uploader.onAfterAddingFile = (file) => { file.withCredentials = false; };
    this.uploader.onSuccessItem = (item, response, status, headers) => {
      if (response) {
        const photo: Photo = JSON.parse(response);
        this.photos.push(photo);
      }
    };
    this.uploader.onCompleteAll = () => { this.uploader.clearQueue(); };
    this.uploader.response.subscribe((res: string) => this.response = res );
  }

  setMainPhoto(photoId: number) {
    const userId = Number(this.authService.decodedToken.nameid);
    this.userService.setMainPhoto(userId, photoId).subscribe(() => {
      this.alertify.success('Photo has been set as main.');
    }, error => {
      this.alertify.error(error);
    });
  }

}
