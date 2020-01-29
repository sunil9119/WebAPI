import { Component, OnInit } from '@angular/core';
import { logging } from 'protractor';
import { AuthService } from '../_service/auth.service';
import { AlertifyService } from '../_service/alertify.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  model: any = {};

  constructor(public authService: AuthService, private alertify: AlertifyService,
              private router: Router) { }

  ngOnInit() { }
  login() {
    this.authService.login(this.model).subscribe(next => {
      this.alertify.success('Login success');
    }, error => {
      this.alertify.error(error);
    }, () => {
      this.router.navigate(['/members']);
    });
    // console.log(this.model);
  }
  loggedIn() {
    return this.authService.loggedIn();
    // const token = localStorage.getItem('token'); // check for thelocal storage token
    // return !!token; // returns true if token exists
  }
  logout() {
    localStorage.removeItem('token'); // remove the item form local storage
    this.alertify.message('Logged out');
    this.router.navigate(['/home']);
  }

}
