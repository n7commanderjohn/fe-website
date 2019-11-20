import { AuthGuard } from './_guards/auth.guard';
import { MediaComponent } from './media/media.component';
import { MessagesComponent } from './messages/messages.component';
import { UserListComponent } from './user-list/user-list.component';
import { HomeComponent } from './home/home.component';
import { Routes, RouterModule } from '@angular/router';

export const routes: Routes = [
  { path: 'home', component: HomeComponent },
  { path: 'users', component: UserListComponent, canActivate: [AuthGuard] },
  { path: 'messages', component: MessagesComponent },
  { path: 'media', component: MediaComponent },
  { path: '**', redirectTo: 'home', pathMatch: 'full' },
];

export const RoutesRoutes = RouterModule.forChild(routes);
