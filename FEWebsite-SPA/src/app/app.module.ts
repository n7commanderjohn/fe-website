import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserModule, HAMMER_GESTURE_CONFIG, HammerGestureConfig } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { RouterModule } from '@angular/router';
import { JwtModule } from '@auth0/angular-jwt';
import { FileUploadModule } from 'ng2-file-upload';
import {
   BsDatepickerModule,
   BsDropdownModule,
   ButtonsModule,
   PaginationModule,
   TabsModule
   } from 'ngx-bootstrap';
import { NgxGalleryModule } from 'ngx-gallery';
import { TimeAgoPipe } from 'time-ago-pipe';
import { AuthGuard } from './_guards/auth.guard';
import { PreventUnsavedChangesGuard } from './_guards/prevent-unsaved-changes.guard';
import { FormGroupValidatorMethods } from './_helpers/formgroupvalidationmethods';
import { MessagesResolver } from './_resolvers/messages.resolver';
import { UserDetailResolver } from './_resolvers/user-detail.resolver';
import { UserEditResolver } from './_resolvers/user-edit.resolver';
import { UserListResolver } from './_resolvers/user-list.resolver';
import { AlertifyService } from './_services/alertify.service';
import { AuthService } from './_services/auth.service';
import { ErrorInteceptorProvider } from './_services/error.inteceptor';
import { GameGenresService } from './_services/gameGenres.service';
import { GamesService } from './_services/games.service';
import { UserService } from './_services/user.service';
import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { MediaComponent } from './media/media.component';
import { MessagesComponent } from './messages/messages.component';
import { NavComponent } from './nav/nav.component';
import { RegisterReactiveComponent } from './register-reactive/register-reactive.component';
import { RegisterComponent } from './register/register.component';
import { routes } from './routes';
import { PasswordResetComponent } from './users/password-reset/password-reset.component';
import { PhotoEditorComponent } from './users/photo-editor/photo-editor.component';
import { UserCardComponent } from './users/user-card/user-card.component';
import { UserDetailComponent } from './users/user-detail/user-detail.component';
import { UserEditReactiveComponent } from './users/user-edit-reactive/user-edit-reactive.component';
import { UserEditComponent } from './users/user-edit/user-edit.component';
import { UserListComponent } from './users/user-list/user-list.component';

export function tokenGetter() {
   return localStorage.getItem('token');
}

export class CustomHammerConfig extends HammerGestureConfig  {
   overrides = {
       pinch: { enable: false },
       rotate: { enable: false }
   };
}

@NgModule({
   declarations: [
      AppComponent,
      NavComponent,
      HomeComponent,
      RegisterComponent,
      RegisterReactiveComponent,
      UserListComponent,
      MediaComponent,
      MessagesComponent,
      UserCardComponent,
      UserDetailComponent,
      UserEditComponent,
      UserEditReactiveComponent,
      PhotoEditorComponent,
      PasswordResetComponent,
      TimeAgoPipe
   ],
   imports: [
      BrowserModule,
      HttpClientModule,
      FormsModule,
      ReactiveFormsModule,
      BrowserAnimationsModule,
      BsDropdownModule.forRoot(),
      BsDatepickerModule.forRoot(),
      TabsModule.forRoot(),
      RouterModule.forRoot(routes),
      NgxGalleryModule,
      FileUploadModule,
      JwtModule.forRoot({
         config: {
           tokenGetter,
           whitelistedDomains: ['localhost:5000'],
           blacklistedRoutes: ['localhost:5000/api/auth/']
         }
      }),
      PaginationModule.forRoot(),
      ButtonsModule.forRoot()
   ],
   providers: [
      AuthService,
      ErrorInteceptorProvider,
      AlertifyService,
      AuthGuard,
      PreventUnsavedChangesGuard,
      UserService,
      UserListResolver,
      UserDetailResolver,
      UserEditResolver,
      MessagesResolver,
      { provide: HAMMER_GESTURE_CONFIG, useClass: CustomHammerConfig },
      GamesService,
      GameGenresService,
      FormGroupValidatorMethods,
   ],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }
