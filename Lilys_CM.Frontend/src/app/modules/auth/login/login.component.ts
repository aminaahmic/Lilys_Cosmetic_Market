import { Component, inject } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { BaseComponent } from '../../../core/components/base-classes/base-component';
import { AuthFacadeService } from '../../../core/services/auth/auth-facade.service';
import { LoginCommand } from '../../../api-services/auth/auth-api.model';
import { CurrentUserService } from '../../../core/services/auth/current-user.service';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-login',
  standalone: false,
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss',
})
export class LoginComponent extends BaseComponent {
  private fb = inject(FormBuilder);
  private auth = inject(AuthFacadeService);
  private router = inject(Router);
  private currentUser = inject(CurrentUserService);
  private snackBar = inject(MatSnackBar);

  hidePassword = true;

  form = this.fb.group({
    email: ['admin@lilys.local', [Validators.required, Validators.email]],
    password: ['Admin123!', [Validators.required]],
    rememberMe: [false],
  });

  onSubmit(): void {
    if (this.form.invalid || this.isLoading) return;

    this.startLoading();

    const payload: LoginCommand = {
      email: (this.form.value.email ?? '').trim(),
      password: this.form.value.password ?? '',
      fingerprint: null,
    };

    this.auth.login(payload).subscribe({
      next: () => {
        this.stopLoading();

        const target = this.currentUser.getDefaultRoute();

        this.snackBar.open('Prijava uspješna.', 'Zatvori', {
          duration: 2200,
          panelClass: ['success-snackbar']
        });

        this.router.navigate([target]);
      },
      error: (err) => {
        this.stopLoading();

        this.snackBar.open(
          'Neispravan email ili password.',
          'Zatvori',
          {
            duration: 3200,
            panelClass: ['error-snackbar']
          }
        );

        console.error('Login error:', err);
      },
    });
  }
}