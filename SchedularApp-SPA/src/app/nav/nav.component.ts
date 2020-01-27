import { Component, OnInit } from '@angular/core';
import { logging } from 'protractor';
import { AuthService } from '../_service/auth.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  model: any = {};

  constructor(private authService: AuthService) { }

  ngOnInit() { }
  login() {
    this.authService.login(this.model).subscribe(next => {
      console.log('Login success');
    }, error => {
      console.log('Failed to login');
    });
    // console.log(this.model);
  }
  loggedIn() {
    const token = localStorage.getItem('token'); // check for thelocal storage token
    return !!token; // returns true if token exists
  }
  logout() {
    localStorage.removeItem('token'); // remove the item form local storage
    console.log('Logged out');
  }

}
