import { Component, OnInit } from '@angular/core';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';
import { Router } from '@angular/router';
import { BsDatepickerConfig } from 'ngx-bootstrap';

import { AlertifyService } from '../_services/alertify.service';
import { AuthService } from '../_services/auth.service';
import { UserService } from '../_services/user.service';
import { GamesService } from '../_services/games.service';

import { FormStrings, FormGroupValidatorMethods } from '../_helpers/formgroupvalidationmethods';
import { StatusCodeResultReturnObject } from '../_models/statusCodeResultReturnObject';
import { Game } from '../_models/game';
import { Gender } from '../_models/gender';
import { User } from '../_models/user';

@Component({
  selector: 'app-register-reactive',
  templateUrl: './register-reactive.component.html',
  styleUrls: ['./register-reactive.component.css']
})
export class RegisterReactiveComponent implements OnInit {
  listOfGames: Game[];
  listOfGenders: Gender[];
  registerForm: FormGroup;
  fs = FormStrings;
  bsConfig: Partial<BsDatepickerConfig>;
  userModel: User;

  constructor(private authService: AuthService,
              private alertify: AlertifyService,
              private userService: UserService,
              private gamesService: GamesService,
              private fb: FormBuilder,
              private router: Router,
              public fgvm: FormGroupValidatorMethods) { }

  ngOnInit() {
    this.bsConfig = {
      containerClass: 'theme-dark-blue'
    };
    this.createRegisterForm();
    this.getGames();
    this.getGenders();
  }

  register() {
    if (this.registerForm.valid) {
      this.userModel = Object.assign({}, this.registerForm.value);
      this.authService.register(this.userModel).subscribe((response) => {
        this.alertify.success('Registration successful.');
      }, (error: StatusCodeResultReturnObject) => {
        this.alertify.error(error.response);
      }, () => {
        this.authService.login(this.userModel).subscribe(() => {
          this.router.navigate(['/home']);
        });
      });
    }
  }

  cancel() {
    this.authService.registerMode = false;
    this.alertify.message('Registration cancelled.');
  }

  private createRegisterForm() {
    this.registerForm = this.fb.group({
      name: [null],
      description: [null],
      gender: ['M'],
      birthday: [null, Validators.required],
      email: [null, [Validators.required, Validators.email]],
      username: [null, Validators.required],
      password: [null,
        [Validators.required, Validators.minLength(8), Validators.maxLength(16)]],
      passwordConfirm: [null, Validators.required],
    },
    { validators: [
        this.fgvm.customValidators.confirmationPasswordMatches
      ]
    });
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
