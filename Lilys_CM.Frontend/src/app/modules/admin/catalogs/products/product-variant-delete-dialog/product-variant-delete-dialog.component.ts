import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';

export interface ProductVariantDeleteDialogData {
  variantLabel: string;
}

@Component({
  selector: 'app-product-variant-delete-dialog',
  standalone: false,
  templateUrl: './product-variant-delete-dialog.component.html',
  styleUrl: './product-variant-delete-dialog.component.scss'
})
export class ProductVariantDeleteDialogComponent {
  constructor(
    @Inject(MAT_DIALOG_DATA)
    public data: ProductVariantDeleteDialogData
  ) {}
}