import { Component, OnInit, Input, EventEmitter, Output } from '@angular/core';
import { AuthService } from './../../_services/auth.service';
import { AlertifyService } from './../../_services/alertify.service';
import { UserService } from 'src/app/_services/user.service';
import { environment } from 'src/environments/environment';
import { Photo } from 'src/app/_models/photo';
import { User } from 'src/app/_models/user';
import { FileUploader } from 'ng2-file-upload';

@Component({
  selector: 'app-photo-editor',
  templateUrl: './photo-editor.component.html',
  styleUrls: ['./photo-editor.component.css']
})
export class PhotoEditorComponent implements OnInit {
  @Input() photos: Photo[];
  @Output() getUserPhotoChange = new EventEmitter<string>();
  uploader: FileUploader;
  hasBaseDropZoneOver = false;
  response: string;
  baseUrl = environment.apiUrl;
  currentMain: Photo;

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
      url: this.baseUrl + this.userService.user + '/' + this.authService.decodedToken.nameid + '/' + this.userService.photo,
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
        if (this.photos.length === 0) {
          this.setNewMainPhoto(photo, 'Your new photo is now the main photo because it is the only one.');
        }
        this.photos.push(photo);
      }
    };
    this.uploader.onCompleteAll = () => {
      this.uploader.clearQueue();
      this.alertify.success('Photo uploads were successful.');
    };
    this.uploader.response.subscribe((res: string) => this.response = res );
  }

  setMainPhoto(newMainPhoto: Photo) {
    const userId = Number(this.authService.decodedToken.nameid);
    this.userService.setMainPhoto(userId, newMainPhoto.id as number).subscribe({
      next: () => {
        this.currentMain = this.photos.filter(p => p.isMain)[0];
        this.currentMain.isMain = false;

        this.setNewMainPhoto(newMainPhoto, 'Selected Photo has been set as main.');
      },
      error: (error: string) => {
        this.alertify.error(error);
    }});
  }


  deletePhoto(photoId: number) {
    this.alertify.confirm('Are you sure you wish to delete this photo?', () => {
      const userId = Number(this.authService.decodedToken.nameid);
      this.userService.deletePhoto(userId, photoId).subscribe({
        next: () => {
          this.photos.splice(this.photos.findIndex(p => p.id === photoId), 1);
          this.alertify.success('The selected photo has been deleted.');

          const areThereAnyPhotosLeft = this.photos.length > 0;
          if (areThereAnyPhotosLeft) {
            const areThereMainPhotos = this.photos.filter(p => p.isMain).length > 0;
            if (!areThereMainPhotos) {
              const newMainPhoto = this.photos.find(p => !p.isMain);
              this.setNewMainPhoto(newMainPhoto, 'Another photo has been selected to be the main photo.');
            }

          } else {
            this.setDefaultUserPhoto();
          }
        }, error: () => {
          this.alertify.error('This photo has failed to be deleted.');
        }
      });
    });
  }

  private setDefaultUserPhoto() {
    const defaultUserPicture = '../../../assets/defaultUser.png';

    this.alertify.success('The main picture has been set to the default.');
    this.authService.changeUserPhoto(defaultUserPicture);

    const currentUser = this.authService.currentUser;
    currentUser.photoUrl = defaultUserPicture;
    localStorage.setItem('user', JSON.stringify(currentUser));
  }

  private setNewMainPhoto(newMainPhoto: Photo, alertifyMessage: string) {
    newMainPhoto.isMain = true;
    this.alertify.success(alertifyMessage);
    this.authService.changeUserPhoto(newMainPhoto.url);

    const currentUser = this.authService.currentUser;
    currentUser.photoUrl = newMainPhoto.url;
    localStorage.setItem('user', JSON.stringify(currentUser));
  }
}
