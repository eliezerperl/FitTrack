import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { RouterModule } from '@angular/router';
import { SharedService } from '../../../core/services/shared-service';

@Component({
  selector: 'app-navbar',
  imports: [CommonModule, RouterModule],
  templateUrl: './navbar.html',
  styleUrl: './navbar.css',
})
export class Navbar implements OnInit {
  isLoggedIn = false;

  constructor(private sharedService: SharedService) {}

  ngOnInit(): void {
    this.sharedService.isAuthenticated$.subscribe((status) => {
      this.isLoggedIn = status;
    });
  }

  logout() {
    this.sharedService.logoutUser();
  }
}
