import { Component, inject } from '@angular/core';
import { CurrentUserService } from '../../core/services/auth/current-user.service';

@Component({
  selector: 'app-client',
  standalone: false,
  templateUrl: './client.component.html',
  styleUrl: './client.component.scss'
})
export class ClientComponent {
  currentUser = inject(CurrentUserService);
}