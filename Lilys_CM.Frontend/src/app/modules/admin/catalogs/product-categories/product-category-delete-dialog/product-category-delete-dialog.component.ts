import { CommonModule } from '@angular/common';
import { Component, Inject } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import {
  MAT_DIALOG_DATA,
  MatDialogModule,
  MatDialogRef
} from '@angular/material/dialog';
import { MatIconModule } from '@angular/material/icon';

import { ListProductCategoriesQueryDto } from '../../../../../api-services/product-categories/product-categories-api.model';

@Component({
  selector: 'app-product-category-delete-dialog',
  standalone: true,
  imports: [
    CommonModule,
    MatButtonModule,
    MatDialogModule,
    MatIconModule
  ],
  templateUrl: './product-category-delete-dialog.component.html',
  styleUrl: './product-category-delete-dialog.component.scss'
})
export class ProductCategoryDeleteDialogComponent {
  constructor(
    private dialogRef: MatDialogRef<ProductCategoryDeleteDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: ListProductCategoriesQueryDto
  ) { }

  get hasProducts(): boolean {
    return (this.data.productCount ?? 0) > 0;
  }

  get hasSubcategories(): boolean {
    return (this.data.subcategoryCount ?? 0) > 0;
  }

  get cannotDelete(): boolean {
    return this.hasProducts || this.hasSubcategories;
  }

  cancel(): void {
    this.dialogRef.close(false);
  }

  confirm(): void {
    if (this.cannotDelete) {
      return;
    }

    this.dialogRef.close(true);
  }
}