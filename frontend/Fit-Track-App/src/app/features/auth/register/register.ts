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
  selector: 'app-register',
  imports: [ReactiveFormsModule, RouterModule],
  templateUrl: './register.html',
  styleUrl: './register.css',
})
export class Register {
  registerForm: FormGroup;

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private toastService: ToastService
  ) {
    this.registerForm = this.fb.group({
      username: ['', [Validators.required]],
      password: ['', Validators.required],
      confirmPassword: ['', Validators.required],
    });
  }

  register() {
    if (this.registerForm.valid) {
      const { password, confirmPassword } = this.registerForm.value;
      if (password !== confirmPassword) {
        this.toastService.failToast("Passwords don't match");
        return;
      }

      console.log('Registering:', this.registerForm.value);
      const user = {
        username: this.registerForm.value.username,
        password: this.registerForm.value.password,
      };
      this.authService.register(user).subscribe({
        next: (res) => {
          console.log(res);
          this.toastService.successToast('Registered!');
        },
        error: (err) => {
          console.log(err);
          this.toastService.failToast();
        },
      });
    }
  }
}
