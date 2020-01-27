import { Injectable } from '@angular/core';
import { CanDeactivate } from '@angular/router';
import { UserEditComponent } from '../users/user-edit/user-edit.component';

@Injectable({
  providedIn: 'root'
})
export class PreventUnsavedChangesGuard implements CanDeactivate<UserEditComponent> {
  canDeactivate(component: UserEditComponent) {
    if (component.editForm.dirty) {
      return confirm('Are you sure you want to continue? Any unsaved changes will be lost.');
    }

    return true;
  }
}
