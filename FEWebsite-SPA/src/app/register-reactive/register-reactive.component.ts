import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';

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

  constructor(private authService: AuthService,
              private alertify: AlertifyService,
              private userService: UserService,
              private gamesService: GamesService,
              public userModel: RegisterUser,
              public fgvm: FormGroupValidatorMethods) { }

  ngOnInit() {
    this.registerForm = new FormGroup({
      name: new FormControl(),
      description: new FormControl(),
      gender: new FormControl('M'),
      username: new FormControl(null, Validators.required),
      password: new FormControl(null, [Validators.required,
        Validators.minLength(8), Validators.maxLength(16)]),
      passwordConfirm: new FormControl(null, Validators.required),
    }, this.fgvm.passwordConfirm.matches);
    this.getGames();
    this.getGenders();
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
