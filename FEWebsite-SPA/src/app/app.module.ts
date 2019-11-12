import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';

import { AppComponent } from './app.component';
import { GameComponent } from './game/game.component';
import { NavComponent } from './nav/nav.component';

@NgModule({
   declarations: [
      AppComponent,
      GameComponent,
      NavComponent,
   ],
   imports: [
      BrowserModule,
      HttpClientModule,
      FormsModule,
   ],
   providers: [],
   bootstrap: [
      AppComponent,
   ]
})
export class AppModule { }
