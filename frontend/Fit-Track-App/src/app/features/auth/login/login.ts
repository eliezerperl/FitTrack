import { Component } from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { RouterModule } from '@angular/router';
import { ToastService } from '../../../core/services/toast-service';
import { AuthService } from '../../../core/services/auth-service';

@Component({
  selector: 'app-login',
  imports: [ReactiveFormsModule, RouterModule],
  templateUrl: './login.html',
  styleUrl: './login.css',
})
export class Login {
  loginForm: FormGroup;

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private toastService: ToastService
  ) {
    this.loginForm = this.fb.group({
      username: ['', [Validators.required]],
      password: ['', Validators.required],
    });
  }

  login() {
    if (this.loginForm.valid) {
      console.log('Logging in:', this.loginForm.value);
      const user = {
        username: this.loginForm.value.username,
        password: this.loginForm.value.password,
      };
      this.authService.login(user).subscribe({
        next: (res) => {
          console.log(res);
          this.toastService.successToast('Logged in!!');
        },
        error: (err) => {
          console.log(err);
          this.toastService.failToast();
        },
      });
    }
  }
}
