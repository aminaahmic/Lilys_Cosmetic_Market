import { Component, inject } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';
import { BaseComponent } from '../../../core/components/base-classes/base-component';
import { AuthApiService } from '../../../api-services/auth/auth-api.service';

@Component({
  selector: 'app-forgot-password',
  standalone: false,
  templateUrl: './forgot-password.component.html',
  styleUrl: './forgot-password.component.scss',
})
export class ForgotPasswordComponent extends BaseComponent {
  private fb = inject(FormBuilder);
  private authApi = inject(AuthApiService);
  private router = inject(Router);
  private snackBar = inject(MatSnackBar);

  form = this.fb.group({
    email: ['', [Validators.required, Validators.email]],
  });

  onSubmit(): void {
    if (this.form.invalid || this.isLoading) return;

    this.startLoading();

    this.authApi.forgotPassword({
      email: (this.form.value.email ?? '').trim()
    }).subscribe({
      next: (response) => {
        this.stopLoading();

        this.snackBar.open(
          response.message || 'Reset token je generisan.',
          'Zatvori',
          {
            duration: 3000,
            panelClass: ['success-snackbar']
          }
        );

        if (response.resetToken) {
          this.router.navigate(['/auth/reset-password'], {
            queryParams: { token: response.resetToken }
          });
        }
      },
      error: (err) => {
        this.stopLoading();

        const backendMessage =
          err?.error?.message ||
          err?.error?.Message ||
          'Greška prilikom pokretanja resetovanja lozinke.';

        this.snackBar.open(backendMessage, 'Zatvori', {
          duration: 4000,
          panelClass: ['error-snackbar']
        });

        console.error('Forgot password error:', err);
      }
    });
  }
}