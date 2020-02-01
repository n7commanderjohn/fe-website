import { Component, OnInit, HostListener } from '@angular/core';
import { FormGroup, FormBuilder, Validators, FormArray, FormControl } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';

import { User } from 'src/app/_models/user';
import { Game } from 'src/app/_models/game';
import { GameGenre } from './../../_models/gamegenre';

import { AlertifyService } from './../../_services/alertify.service';
import { UserService } from 'src/app/_services/user.service';
import { AuthService } from './../../_services/auth.service';
import { GameGenresService } from './../../_services/gameGenres.service';
import { GamesService } from './../../_services/games.service';

import { FormGroupValidatorMethods, FormStrings } from './../../_helpers/formgroupvalidationmethods';

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
  userEditForm: FormGroup;
  fs = FormStrings;

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
    this.route.data.subscribe(
      data => {
        this.user = data.user;

        this.createUserEditForm(this.user);
        this.getGames();
        this.getGameGenres();
      },
      error => {
        this.alertify.error(error.response);
      }
    );
    this.authService.currentPhotoUrl.subscribe(
      photoUrl => (this.photoUrl = photoUrl)
    );
  }

  updateUser() {
    if (this.userEditForm.valid) {
      this.user = Object.assign({}, this.userEditForm.value);
      const userId = Number(this.authService.decodedToken.nameid);
      this.userService.updateUser(userId, this.user).subscribe(
        (response) => {
            this.alertify.success('Profile updated successfully.');
            this.userEditForm.reset(this.user);
        },
        error => {
          this.alertify.error(error);
        }
        );
    }
  }

  get games() {
    return this.userEditForm.get(this.fs.games) as FormArray;
  }

  get genres() {
    return this.userEditForm.get(this.fs.genres) as FormArray;
  }

  private createUserEditForm(user: User) {
    this.userEditForm = this.fb.group(
      {
        name: [user.name],
        description: [user.description],
        gender: [user.genderId],
        birthday: [user.birthday, Validators.required],
        email: [user.email, Validators.required],
        username: [user.username, Validators.required],
        password: [null,
          [
            Validators.required,
            Validators.minLength(8),
            Validators.maxLength(16)
          ]
        ],
        passwordConfirm: [null, Validators.required],
        games: null,
        genres: null,
      },
      { validator: this.fgvm.passwordConfirm.matches }
    );
  }

  private getGames() {
    this.gamesService.getGames().subscribe(
      retrievedGames => {
        this.setActiveGamesForUser(retrievedGames);
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
}
