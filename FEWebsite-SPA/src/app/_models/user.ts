import { BaseModel } from './_basemodels/basemodel';
import { Game } from './game';
import { GameGenre } from './gameGenre';
import { Photo } from './photo';

export interface User extends BaseModel {
    email: string;
    username: string;
    age: number;
    genderId: string;
    gender: string;
    birthday: Date;
    accountCreated: Date;
    lastLogin: Date | any;
    photoUrl: string;
    photos: Photo[];
    listOfGames?: string[];
    listOfGenres?: string[];
    games?: Game[];
    genres?: GameGenre[];
    passwordCurrent: string;
    password: string;
    passwordConfirm: string;
    isPasswordNeeded: boolean;
    listOfLikees?: number[];
}
