import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';

export interface BrandDeleteDialogData {
  brandName: string;
}

@Component({
  selector: 'app-brand-delete-dialog',
  standalone: false,
  templateUrl: './brand-delete-dialog.component.html',
  styleUrl: './brand-delete-dialog.component.scss'
})
export class BrandDeleteDialogComponent {
  constructor(
    @Inject(MAT_DIALOG_DATA)
    public data: BrandDeleteDialogData
  ) {}
}