import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { LoginService, LoginRequest } from '../../services/login';

@Component({
  selector: 'app-login',
  standalone: false,
  templateUrl: './login.html',
  styleUrl: './login.css'
})
export class Login {
  loginForm: FormGroup;
  isLoading = false;
  errorMessage = '';

  constructor(
    private fb: FormBuilder,
    private loginService: LoginService,
    private router: Router
  ) {
    this.loginForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(6)]]
    });
  }

  onSubmit(): void {
    if (this.loginForm.valid) {
      this.isLoading = true;
      this.errorMessage = '';

      const credentials: LoginRequest = {
        email: this.loginForm.get('email')?.value,
        password: this.loginForm.get('password')?.value
      };

      this.loginService.login(credentials).subscribe({
        next: (response) => {
          if (response.success && response.token) {
            this.loginService.saveToken(response.token);
            this.router.navigate(['/landing']);
          } else {
            this.errorMessage = response.message || 'Error en el inicio de sesión';
          }
          this.isLoading = false;
        },
        error: (error) => {
          this.errorMessage = 'Error de conexión. Intente nuevamente.';
          this.isLoading = false;
          console.error('Login error:', error);
        }
      });
    }
  }

  get email() { return this.loginForm.get('email'); }
  get password() { return this.loginForm.get('password'); }
}
