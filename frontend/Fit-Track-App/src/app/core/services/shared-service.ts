import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from './auth-service';
import { BehaviorSubject, Observable } from 'rxjs';
import { ToastService } from './toast-service';
import { User } from '../models/user-model';
import { AuthRequest } from '../models/auth-request-model';

@Injectable({
  providedIn: 'root',
})
export class SharedService {
  private isAuthenticatedSubject = new BehaviorSubject<boolean>(false);
  isAuthenticated$: Observable<boolean> =
    this.isAuthenticatedSubject.asObservable();

  constructor(
    private router: Router,
    private authService: AuthService,
    private toastService: ToastService
  ) {}

  setAuthenticated(value: boolean) {
    this.isAuthenticatedSubject.next(value);
  }

  registerThenLogin(req: AuthRequest) {
    this.authService.register(req).subscribe({
      next: (res) => {
        console.log(res);
        this.toastService.successToast('Registered!');

        this.loginUser(req);
      },
      error: (err) => {
        console.log(err);
        this.toastService.failToast();
      },
    });
  }

  loginUser(req: AuthRequest) {
    this.authService.login(req).subscribe({
      next: (res) => {
        console.log(res);
        this.toastService.successToast('Logged in!');
        this.setAuthenticated(true);
        this.router.navigate(['/dashboard']);
      },
      error: (err) => {
        console.log(err);
        this.toastService.failToast();
      },
    });
  }

  logoutUser() {
    this.authService.logout().subscribe({
      next: () => {
        this.setAuthenticated(false);
        this.router.navigate(['/']);
        this.toastService.successToast('Logged out');
      },
      error: (err) => {
        this.setAuthenticated(false);
        console.log(err);
      },
    });
  }
}
