import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { AlertifyService } from './../_services/alertify.service';
import { AuthService } from './../_services/auth.service';
import { UserService } from 'src/app/_services/user.service';
import { GamesService } from './../_services/games.service';
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
  userModel: RegisterUser;

  constructor(private authService: AuthService,
              private alertify: AlertifyService,
              private userService: UserService,
              private gamesService: GamesService) { }

  ngOnInit() {
    this.userModel = new RegisterUser();
    this.getGames();
    this.getGenders();
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

  register() {
    this.authService.register(this.userModel).subscribe((response) => {
      this.alertify.success('Registration successful.');
    }, (error: any) => {
      this.alertify.error(error);
    });
  }

  cancel() {
    this.cancelRegister.emit(false);
    this.alertify.message('Registration cancelled.');
  }

}
