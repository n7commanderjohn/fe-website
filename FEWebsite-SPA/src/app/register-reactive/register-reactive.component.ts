import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';
import { BsDatepickerConfig } from 'ngx-bootstrap';

import { AlertifyService } from '../_services/alertify.service';
import { AuthService } from '../_services/auth.service';
import { UserService } from '../_services/user.service';
import { GamesService } from '../_services/games.service';

import { FormStrings, FormGroupValidatorMethods } from '../_helpers/formgroupvalidationmethods';
import { RegisterUser } from '../_models/registerUser';
import { Game } from '../_models/game';
import { Gender } from '../_models/gender';

@Component({
  selector: 'app-register-reactive',
  templateUrl: './register-reactive.component.html',
  styleUrls: ['./register-reactive.component.css']
})
export class RegisterReactiveComponent implements OnInit {
  @Output() cancelRegister = new EventEmitter();
  listOfGames: Game[];
  listOfGenders: Gender[];
  registerForm: FormGroup;
  fs = FormStrings;
  bsConfig: Partial<BsDatepickerConfig>;

  constructor(private authService: AuthService,
              private alertify: AlertifyService,
              private userService: UserService,
              private gamesService: GamesService,
              private fb: FormBuilder,
              public userModel: RegisterUser,
              public fgvm: FormGroupValidatorMethods) { }

  ngOnInit() {
    this.bsConfig = {
      containerClass: 'theme-dark-blue'
    };
    this.createRegisterForm();
    this.getGames();
    this.getGenders();
  }

  createRegisterForm() {
    this.registerForm = this.fb.group({
      name: [null],
      description: [null],
      gender: ['M'],
      dateOfBirth: [null, Validators.required],
      email: [null, Validators.required],
      username: [null, Validators.required],
      password: [null,
        [Validators.required, Validators.minLength(8), Validators.maxLength(16)]],
      passwordConfirm: [null, Validators.required],
    }, {validator: this.fgvm.passwordConfirm.matches});
  }

  register() {
    // this.authService.register(this.userModel).subscribe((response) => {
    //   this.alertify.success('Registration successful.');
    // }, (error: any) => {
    //   this.alertify.error(error);
    // });
    console.log(this.registerForm.value);
  }

  cancel() {
    this.cancelRegister.emit(false);
    this.alertify.message('Registration cancelled.');
  }

  private getGames() {
    this.gamesService.getGames().subscribe(games => {
      this.listOfGames = games;
    }, error => {
      this.alertify.error(error);
    });
  }

  private getGenders() {
    this.userService.getGenders().subscribe(genders => {
      this.listOfGenders = genders;
    }, error => {
      this.alertify.error(error);
    });
  }

}
