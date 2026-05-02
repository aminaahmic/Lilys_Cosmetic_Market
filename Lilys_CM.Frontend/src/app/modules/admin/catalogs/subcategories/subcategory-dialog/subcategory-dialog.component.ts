import { CommonModule } from '@angular/common';
import { Component, Inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import {
  MAT_DIALOG_DATA,
  MatDialogModule,
  MatDialogRef
} from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';

import { ListProductCategoriesQueryDto } from '../../../../../api-services/product-categories/product-categories-api.model';
import { SubcategoryDto } from '../../../../../api-services/subcategories/subcategories-api.models';

export interface SubcategoryDialogData {
  mode: 'create' | 'edit';
  categories: ListProductCategoriesQueryDto[];
  subcategory: SubcategoryDto | null;
}

export interface SubcategoryDialogResult {
  name: string;
  categoryId: number;
  isEnabled: boolean;
}

@Component({
  selector: 'app-subcategory-dialog',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    MatButtonModule,
    MatDialogModule,
    MatFormFieldModule,
    MatIconModule,
    MatInputModule,
    MatSelectModule,
    MatSlideToggleModule
  ],
  templateUrl: './subcategory-dialog.component.html',
  styleUrl: './subcategory-dialog.component.scss'
})
export class SubcategoryDialogComponent {
  name = '';
  categoryId: number | null = null;
  isEnabled = true;

  constructor(
    private dialogRef: MatDialogRef<SubcategoryDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: SubcategoryDialogData
  ) {
    if (data.subcategory) {
      this.name = data.subcategory.name;
      this.categoryId = data.subcategory.categoryId;
      this.isEnabled = data.subcategory.isEnabled;
    }
  }

  get title(): string {
    return this.data.mode === 'edit' ? 'Edit Subcategory' : 'New Subcategory';
  }

  get submitText(): string {
    return this.data.mode === 'edit' ? 'AŽURIRAJ' : 'DODAJ';
  }

  save(): void {
    if (!this.name.trim() || !this.categoryId) {
      return;
    }

    const result: SubcategoryDialogResult = {
      name: this.name.trim(),
      categoryId: this.categoryId,
      isEnabled: this.isEnabled
    };

    this.dialogRef.close(result);
  }

  cancel(): void {
    this.dialogRef.close(null);
  }
}