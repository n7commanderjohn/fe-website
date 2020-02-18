import { Injectable } from '@angular/core';
import { CanDeactivate } from '@angular/router';
import { UserEditReactiveComponent } from './../users/user-edit-reactive/user-edit-reactive.component';

@Injectable({
  providedIn: 'root'
})
export class PreventUnsavedChangesGuard implements CanDeactivate<UserEditReactiveComponent> {
  canDeactivate(component: UserEditReactiveComponent) {
    if (component.userEditForm.dirty) {
      return confirm('Are you sure you want to continue? Any unsaved changes will be lost.');
    }

    return true;
  }
}
