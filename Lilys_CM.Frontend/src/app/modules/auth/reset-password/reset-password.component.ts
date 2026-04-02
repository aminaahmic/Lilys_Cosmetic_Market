import { Component, inject, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';
import { BaseComponent } from '../../../core/components/base-classes/base-component';
import { AuthApiService } from '../../../api-services/auth/auth-api.service';

@Component({
  selector: 'app-reset-password',
  standalone: false,
  templateUrl: './reset-password.component.html',
  styleUrl: './reset-password.component.scss',
})
export class ResetPasswordComponent extends BaseComponent implements OnInit {
  private fb = inject(FormBuilder);
  private authApi = inject(AuthApiService);
  private route = inject(ActivatedRoute);
  private router = inject(Router);
  private snackBar = inject(MatSnackBar);

  hidePassword = true;
  hideConfirmPassword = true;

  form = this.fb.group({
    token: ['', [Validators.required]],
    newPassword: ['', [Validators.required, Validators.minLength(6)]],
    confirmPassword: ['', [Validators.required]],
  });

  ngOnInit(): void {
    const tokenFromQuery = this.route.snapshot.queryParamMap.get('token') ?? '';
    if (tokenFromQuery) {
      this.form.patchValue({ token: tokenFromQuery });
    }
  }

  onSubmit(): void {
    if (this.form.invalid || this.isLoading) return;

    const newPassword = this.form.value.newPassword ?? '';
    const confirmPassword = this.form.value.confirmPassword ?? '';

    if (newPassword !== confirmPassword) {
      this.snackBar.open('Lozinka i potvrda lozinke se ne poklapaju.', 'Zatvori', {
        duration: 3500,
        panelClass: ['error-snackbar']
      });
      return;
    }

    this.startLoading();

    this.authApi.resetPassword({
      token: (this.form.value.token ?? '').trim(),
      newPassword,
      confirmPassword
    }).subscribe({
      next: (response) => {
        this.stopLoading();

        this.snackBar.open(
          response.message || 'Lozinka je uspješno promijenjena.',
          'Zatvori',
          {
            duration: 3500,
            panelClass: ['success-snackbar']
          }
        );

        this.router.navigate(['/auth/login']);
      },
      error: (err) => {
        this.stopLoading();

        const backendMessage =
          err?.error?.message ||
          err?.error?.Message ||
          'Promjena lozinke nije uspjela.';

        this.snackBar.open(backendMessage, 'Zatvori', {
          duration: 4000,
          panelClass: ['error-snackbar']
        });

        console.error('Reset password error:', err);
      }
    });
  }
}