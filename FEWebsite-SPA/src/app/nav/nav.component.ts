import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  loginCredentials: any = {};
  nums: number[] = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10 ];

  constructor() { }

  ngOnInit() {
  }

  login() {
    console.log(this.loginCredentials);
  }

}
