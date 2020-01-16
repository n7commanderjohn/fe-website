import { BaseModel } from './_basemodels/basemodel';
import { Game } from './game';
import { GameGenre } from './gamegenre';
import { Photo } from './photo';

export interface User extends BaseModel {
    username: string;
    age: number;
    gender: string;
    accountCreated: Date;
    lastLogin: Date;
    photoUrl: string;
    photos: Photo[];
    listOfGames?: Game[];
    listOfGenres?: GameGenre[];
}
