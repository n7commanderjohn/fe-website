import { FormGroupValidatorMethods, FormStrings } from './../../_helpers/formgroupvalidationmethods';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Component, OnInit, Output, EventEmitter } from '@angular/core';

import { AuthService } from './../../_services/auth.service';
import { AlertifyService } from './../../_services/alertify.service';
import { User } from 'src/app/_models/user';

@Component({
  selector: 'app-password-reset',
  templateUrl: './password-reset.component.html',
  styleUrls: ['./password-reset.component.css']
})
export class PasswordResetComponent implements OnInit {
  passwordResetForm: FormGroup;
  fs = FormStrings;
  userModel: User;

  constructor(private authService: AuthService,
              private alertify: AlertifyService,
              private fb: FormBuilder,
              public fgvm: FormGroupValidatorMethods) { }

  ngOnInit() {
    this.createPasswordResetForm();
  }

  private createPasswordResetForm() {
    this.passwordResetForm = this.fb.group({
      email: [null, Validators.required],
      username: [null, Validators.required],
    });
  }

  changePassword() {
    this.userModel = Object.assign({}, this.passwordResetForm.value);
    this.authService.resetPassword(this.userModel);
  }

  cancel() {
    this.authService.pwResetMode = false;
    this.alertify.message('Password Reset cancelled.');
  }

}
