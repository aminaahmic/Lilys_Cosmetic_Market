import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { MatTableModule } from '@angular/material/table';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { SubcategoryDialogComponent } from './subcategory-dialog/subcategory-dialog.component';
import { ProductCategoriesApiService } from '../../../../api-services/product-categories/product-categories-api.service';
import { ListProductCategoriesRequest, ListProductCategoriesQueryDto } from '../../../../api-services/product-categories/product-categories-api.model';
import { SubcategoryDeleteDialogComponent } from './subcategory-delete-dialog/subcategory-delete-dialog.component';

import {
  CreateSubcategoryRequest,
  ListSubcategoriesRequest,
  SubcategoryDto,
  UpdateSubcategoryRequest
} from '../../../../api-services/subcategories/subcategories-api.models';
import { SubcategoriesApiService } from '../../../../api-services/subcategories/subcategories-api.service';

@Component({
  selector: 'app-subcategories',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    MatButtonModule,
    MatFormFieldModule,
    MatIconModule,
    MatInputModule,
    MatSelectModule,
    MatSlideToggleModule,
    MatTableModule,
    MatDialogModule
  ],
  templateUrl: './subcategories.component.html',
  styleUrl: './subcategories.component.scss'
})
export class SubcategoriesComponent implements OnInit {
  categories: ListProductCategoriesQueryDto[] = [];
  subcategories: SubcategoryDto[] = [];

  displayedColumns: string[] = ['name', 'categoryName', 'isEnabled', 'productCount', 'actions'];

  selectedCategoryId: number | null = null;
  selectedStatus: boolean | null = null;
  search = '';

  formModel = {
    id: null as number | null,
    name: '',
    categoryId: null as number | null,
    isEnabled: true
  };

  isEditMode = false;
  isLoading = false;

  constructor(
    private categoriesApi: ProductCategoriesApiService,
    private subcategoriesApi: SubcategoriesApiService,
    private dialog: MatDialog
  ) { }

  ngOnInit(): void {
    this.loadCategories();
    this.loadSubcategories();
  }

  loadCategories(): void {
    const request = new ListProductCategoriesRequest();
    request.onlyEnabled = true;
    request.paging.pageSize = 100;

    this.categoriesApi.list(request).subscribe({
      next: res => {
        this.categories = res.items;
      },
      error: err => console.error('Load categories error:', err)
    });
  }

  loadSubcategories(): void {
    this.isLoading = true;

    const request = new ListSubcategoriesRequest();
    request.categoryId = this.selectedCategoryId;
    request.onlyEnabled = this.selectedStatus;
    request.search = this.search;
    request.paging.pageSize = 100;

    this.subcategoriesApi.list(request).subscribe({
      next: res => {
        this.subcategories = res.items;
        this.isLoading = false;
      },
      error: err => {
        console.error('Load subcategories error:', err);
        this.isLoading = false;
      }
    });
  }

  applyFilters(): void {
    this.loadSubcategories();
  }

  resetFilters(): void {
    this.selectedCategoryId = null;
    this.selectedStatus = null;
    this.search = '';
    this.loadSubcategories();
  }

  save(): void {
    if (!this.formModel.name.trim() || !this.formModel.categoryId) {
      return;
    }

    if (this.isEditMode && this.formModel.id) {
      const request: UpdateSubcategoryRequest = {
        name: this.formModel.name.trim(),
        categoryId: this.formModel.categoryId,
        isEnabled: this.formModel.isEnabled
      };

      this.subcategoriesApi.update(this.formModel.id, request).subscribe({
        next: () => {
          this.resetForm();
          this.loadSubcategories();
        },
        error: err => console.error('Update subcategory error:', err)
      });

      return;
    }

    const request: CreateSubcategoryRequest = {
      name: this.formModel.name.trim(),
      categoryId: this.formModel.categoryId,
      isEnabled: this.formModel.isEnabled
    };

    this.subcategoriesApi.create(request).subscribe({
      next: () => {
        this.resetForm();
        this.loadSubcategories();
      },
      error: err => console.error('Create subcategory error:', err)
    });
  }

  edit(item: SubcategoryDto): void {
    this.isEditMode = true;

    this.formModel = {
      id: item.id,
      name: item.name,
      categoryId: item.categoryId,
      isEnabled: item.isEnabled
    };
  }

  delete(item: SubcategoryDto): void {
    if (!confirm(`Obrisati potkategoriju "${item.name}"?`)) {
      return;
    }

    this.subcategoriesApi.delete(item.id).subscribe({
      next: () => this.loadSubcategories(),
      error: err => console.error('Delete subcategory error:', err)
    });
  }

  resetForm(): void {
    this.isEditMode = false;

    this.formModel = {
      id: null,
      name: '',
      categoryId: null,
      isEnabled: true
    };
  }
  openCreateDialog(): void {
    const dialogRef = this.dialog.open(SubcategoryDialogComponent, {
      width: '430px',
      panelClass: 'subcategory-dialog-panel',
      data: {
        mode: 'create',
        categories: this.categories,
        subcategory: null
      }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (!result) {
        return;
      }

      this.subcategoriesApi.create(result).subscribe({
        next: () => this.loadSubcategories(),
        error: err => console.error('Create subcategory error:', err)
      });
    });
  }

  openEditDialog(item: SubcategoryDto): void {
    const dialogRef = this.dialog.open(SubcategoryDialogComponent, {
      width: '430px',
      panelClass: 'subcategory-dialog-panel',
      data: {
        mode: 'edit',
        categories: this.categories,
        subcategory: item
      }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (!result) {
        return;
      }

      this.subcategoriesApi.update(item.id, result).subscribe({
        next: () => this.loadSubcategories(),
        error: err => console.error('Update subcategory error:', err)
      });
    });
  }
  openDeleteDialog(item: SubcategoryDto): void {
    const dialogRef = this.dialog.open(SubcategoryDeleteDialogComponent, {
      width: '380px',
      panelClass: 'subcategory-delete-dialog-panel',
      data: item
    });

    dialogRef.afterClosed().subscribe(confirmed => {
      if (!confirmed) {
        return;
      }

      this.subcategoriesApi.delete(item.id).subscribe({
        next: () => this.loadSubcategories(),
        error: err => console.error('Delete subcategory error:', err)
      });
    });
  }
  get activeCount(): number {
    return this.subcategories.filter(x => x.isEnabled).length;
  }

  get inactiveCount(): number {
    return this.subcategories.filter(x => !x.isEnabled).length;
  }
}