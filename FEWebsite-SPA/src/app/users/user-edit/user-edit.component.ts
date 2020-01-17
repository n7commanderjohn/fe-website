import { Component, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';

import { User } from 'src/app/_models/user';
import { Game } from 'src/app/_models/game';
import { GameGenre } from './../../_models/gamegenre';

import { AlertifyService } from './../../_services/alertify.service';
import { GameGenresService } from './../../_services/gameGenres.service';
import { GamesService } from './../../_services/games.service';

@Component({
  selector: 'app-user-edit',
  templateUrl: './user-edit.component.html',
  styleUrls: ['./user-edit.component.css']
})
export class UserEditComponent implements OnInit {
  @ViewChild('editForm', {static: true}) editForm: NgForm;
  user: User;
  allGames: Game[];
  allGenres: GameGenre[];

  constructor(
    private route: ActivatedRoute,
    private alertify: AlertifyService,
    private gamesService: GamesService,
    private gameGenresService: GameGenresService
  ) {}

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.user = data.user;

      this.getGames();
      this.getGameGenres();
    });
  }

  updateUser() {
    console.log(this.user);
    console.log(this.allGames);
    console.log(this.allGames.filter(game => {
      if (game.selected) {
        return game;
      }})
    );
    this.alertify.success('Profile updated successfully.');
    this.editForm.reset(this.user);
  }

  getGames() {
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

  setActiveGamesForUser(retrievedGames: Game[]) {
    const userGameIds = this.user.games.map(game => {
      return game.id;
    });

    retrievedGames.forEach(game => {
      game.selected = userGameIds.includes(game.id);
    });

    const activeGames = retrievedGames.filter(game => {
      if (game.selected) {
        return game;
      }
    });

    console.log(activeGames);
    console.log(retrievedGames);

    this.allGames = retrievedGames;
  }

  getGameGenres() {
    this.gameGenresService.getGameGenres().subscribe(
      genres => {
        this.allGenres = genres;
      },
      error => {
        this.alertify.error('List of Game Genres failed to load.');
        console.log(error);
      }
    );
  }
}
