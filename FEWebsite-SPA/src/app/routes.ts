import { Routes } from '@angular/router';
import { AuthGuard } from './_guards/auth.guard';
import { PreventUnsavedChangesGuard } from './_guards/prevent-unsaved-changes.guard';
import { MessagesResolver } from './_resolvers/messages.resolver';
import { UserDetailResolver } from './_resolvers/user-detail.resolver';
import { UserEditResolver } from './_resolvers/user-edit.resolver';
import { UserListResolver } from './_resolvers/user-list.resolver';
import { HomeComponent } from './home/home.component';
import { MediaComponent } from './media/media.component';
import { MessagesComponent } from './messages/messages.component';
import { UserDetailComponent } from './users/user-detail/user-detail.component';
import { UserEditReactiveComponent } from './users/user-edit-reactive/user-edit-reactive.component';
import { UserListComponent } from './users/user-list/user-list.component';

export const routes: Routes = [
  {
    path: '',
    children: [
      { path: '', component: HomeComponent },
      { path: 'home', component: HomeComponent },
    ]
  },
  {
    path: '',
    runGuardsAndResolvers: 'always',
    canActivate: [AuthGuard],
    children: [
      { path: 'user/edit', component: UserEditReactiveComponent, resolve: {user: UserEditResolver},
        canDeactivate: [PreventUnsavedChangesGuard] },
      { path: 'users', component: UserListComponent, resolve: {users: UserListResolver} },
      { path: 'users/:id', component: UserDetailComponent, resolve: {user: UserDetailResolver} },
      { path: 'messages', component: MessagesComponent, resolve: {messages: MessagesResolver} },
      { path: 'media', component: MediaComponent },
    ]
  },
  { path: '**', redirectTo: 'home', pathMatch: 'full' },
];
