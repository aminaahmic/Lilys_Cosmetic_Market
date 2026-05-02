import { CommonModule } from '@angular/common';
import { Component, Inject } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import {
  MAT_DIALOG_DATA,
  MatDialogModule,
  MatDialogRef
} from '@angular/material/dialog';
import { MatIconModule } from '@angular/material/icon';

import { SubcategoryDto } from '../../../../../api-services/subcategories/subcategories-api.models';

@Component({
  selector: 'app-subcategory-delete-dialog',
  standalone: true,
  imports: [
    CommonModule,
    MatButtonModule,
    MatDialogModule,
    MatIconModule
  ],
  templateUrl: './subcategory-delete-dialog.component.html',
  styleUrl: './subcategory-delete-dialog.component.scss'
})
export class SubcategoryDeleteDialogComponent {
  constructor(
    private dialogRef: MatDialogRef<SubcategoryDeleteDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: SubcategoryDto
  ) {}

  cancel(): void {
    this.dialogRef.close(false);
  }

  confirm(): void {
    this.dialogRef.close(true);
  }
}