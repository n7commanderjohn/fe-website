import { Component, OnInit, HostListener } from '@angular/core';
import { FormGroup, FormBuilder, Validators, FormControl } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { BsDatepickerConfig } from 'ngx-bootstrap';

import { AlertifyService } from './../../_services/alertify.service';
import { UserService } from './../../_services/user.service';
import { AuthService } from './../../_services/auth.service';
import { GameGenresService } from './../../_services/gameGenres.service';
import { GamesService } from './../../_services/games.service';

import { FormGroupValidatorMethods, FormStrings } from './../../_helpers/formgroupvalidationmethods';
import { StatusCodeResultReturnObject } from './../../_models/statusCodeResultReturnObject';
import { User } from './../../_models/user';
import { Game } from './../../_models/game';
import { GameGenre } from './../../_models/gamegenre';
import { Gender } from './../../_models/gender';

@Component({
  selector: 'app-user-edit-reactive',
  templateUrl: './user-edit-reactive.component.html',
  styleUrls: ['./user-edit-reactive.component.css']
})
export class UserEditReactiveComponent implements OnInit {
  user: User;
  photoUrl: string;
  allGames: Game[];
  allGenres: GameGenre[];
  listOfGenders: Gender[];
  userEditForm: FormGroup;
  initialFormState: User;
  fs = FormStrings;
  bsConfig: Partial<BsDatepickerConfig>;
  maxDate = new Date();
  passwordChangeMode = false;
  debug = false;

  @HostListener('window:beforeunload', ['$event'])
  unloadNotification($event: any) {
    if (this.userEditForm.dirty) {
      $event.returnValue = true;
    }
  }

  constructor(
    private route: ActivatedRoute,
    private alertify: AlertifyService,
    private userService: UserService,
    private authService: AuthService,
    private gamesService: GamesService,
    private gameGenresService: GameGenresService,
    private fb: FormBuilder,
    public fgvm: FormGroupValidatorMethods
  ) {}

  ngOnInit() {
    this.bsConfig = {
      containerClass: 'theme-dark-blue'
    };
    this.route.data.subscribe(
      data => {
        this.user = data.user;

        this.createUserEditForm(this.user);
        this.getGames();
        this.getGameGenres();
        this.getGenders();
      },
      (error: StatusCodeResultReturnObject) => {
        this.alertify.error(error.response);
      }
    );
    this.authService.currentPhotoUrl.subscribe(
      photoUrl => (this.photoUrl = photoUrl)
    );
  }

  passwordChangeToggle() {
    this.passwordChangeMode = !this.passwordChangeMode;
    this.fgvm.passwordCurrent.toggleOtherPasswordFields(this.userEditForm, this.passwordChangeMode);
  }

  resetForm(formGroupValue: any, isUpdate?: boolean) {
    this.userEditForm.patchValue({passwordCurrent: undefined});
    this.userEditForm.reset(formGroupValue);
    if (isUpdate) {
      this.alertify.success('Profile updated successfully.');
    } else {
      this.alertify.message('Pending changes canceled.');
    }
  }

  updateUser() {
    if (this.userEditForm.valid) {
      this.assignFormValuesToUser();
      if (this.debug) {
        console.log(this.user);
        console.log(this.allGames);
        console.log(this.allGenres);
      } else {
        const userId = Number(this.authService.decodedToken.nameid);
        this.userService.updateUser(userId, this.user).subscribe(response => {
          this.resetForm(this.userEditForm.value, true);
          this.authService.updateTokenAndUserDetails(response);
          this.user.age = response.userAge;
        }, (error: string) => {
          this.alertify.error(error);
        });
      }
    }
  }

  private assignFormValuesToUser() {
    this.user = Object.assign(this.user, this.userEditForm.value);
    this.user.genderId = this.user.gender;
    this.user.games.forEach((element, index) => {
      this.allGames[index].checked = element as unknown as boolean; // the form returns an array of booleans
    });
    this.user.genres.forEach((element, index) => {
      this.allGenres[index].checked = element as unknown as boolean; // the form returns an array of booleans
    });
    this.user.games = this.allGames.filter(g => g.checked);
    this.user.genres = this.allGenres.filter(g => g.checked);

    const hasEmailBeenChanged = this.initialFormState.email !== this.user.email;
    const hasUserNameBeenChanged = this.initialFormState.username !== this.user.username;
    this.user.isPasswordNeeded = this.passwordChangeMode || hasEmailBeenChanged || hasUserNameBeenChanged;
  }

  private createUserEditForm(user: User) {
    this.userEditForm = this.fb.group({
      name: [user.name, Validators.required],
      description: [user.description],
      gender: [user.genderId],
      birthday: [new Date(user.birthday), Validators.required],
      email: [user.email,
        [Validators.required, Validators.email]
      ],
      username: [user.username, Validators.required],
      passwordCurrent: null,
      password: [{value: null, disabled: true},
        [Validators.minLength(8), Validators.maxLength(16)]
      ],
      passwordConfirm: [{value: null, disabled: true}],
      games: null,
      genres: null,
    },
    { validators: [
        this.fgvm.customValidators.passwordCurrentRequired,
        this.fgvm.customValidators.passwordNewRequired,
        this.fgvm.customValidators.confirmationPasswordMatches,
      ]
    });
    this.initialFormState = this.userEditForm.value;
  }

  private getGames() {
    this.gamesService.getGames().subscribe(
      retrievedGames => {
        this.setActiveGamesForUser(retrievedGames);
        this.initialFormState = this.userEditForm.value;
      },
      error => {
        this.alertify.error('List of Games failed to load.');
        console.log(error);
      }
    );
  }

  private getGameGenres() {
    this.gameGenresService.getGameGenres().subscribe(
      retrievedGenres => {
        this.setActiveGenresForUser(retrievedGenres);
        this.initialFormState = this.userEditForm.value;
      },
      error => {
        this.alertify.error('List of Game Genres failed to load.');
        console.log(error);
      }
    );
  }

  private setActiveGamesForUser(retrievedGames: Game[]) {
    const userGameIds = this.user.games.map(game => {
      return game.id;
    });

    retrievedGames.forEach(game => {
      game.checked = userGameIds.includes(game.id);
    });

    this.allGames = retrievedGames;

    const gamesInFormControls = this.allGames.map(
      game => new FormControl(game.checked)
    );
    this.userEditForm.setControl(this.fs.games, this.fb.array(gamesInFormControls));
  }

  private setActiveGenresForUser(retrievedGenres: GameGenre[]) {
    const userGenreIds = this.user.genres.map(genre => {
      return genre.id;
    });

    retrievedGenres.forEach(genre => {
      genre.checked = userGenreIds.includes(genre.id);
    });

    this.allGenres = retrievedGenres;
    const genresInFormControls = this.allGenres.map(
      genre => new FormControl(genre.checked)
    );
    this.userEditForm.setControl(this.fs.genres, this.fb.array(genresInFormControls));
  }

  private getGenders() {
    this.userService.getGenders().subscribe(genders => {
      this.listOfGenders = genders;
    }, (error: StatusCodeResultReturnObject) => {
      this.alertify.error(error.response);
    });
  }
}
