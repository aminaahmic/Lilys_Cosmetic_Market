import { Component, inject, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { of, switchMap } from 'rxjs';
import { ProductFormService } from '../services/product-form.service';
import {
  AdjustProductStockCommand,
  CreateProductCommand,
  GetProductByIdQueryDto
} from '../../../../../api-services/products/products-api.models';
import { BaseFormComponent } from '../../../../../core/components/base-classes/base-form-component';
import { ProductsApiService } from '../../../../../api-services/products/products-api.service';
import { ProductCategoriesApiService } from '../../../../../api-services/product-categories/product-categories-api.service';
import { ToasterService } from '../../../../../core/services/toaster.service';
import {
  ListProductCategoriesQueryDto,
  ListProductCategoriesRequest
} from '../../../../../api-services/product-categories/product-categories-api.model';

@Component({
  selector: 'app-products-add',
  standalone: false,
  templateUrl: './products-add.component.html',
  styleUrl: './products-add.component.scss',
  providers: [ProductFormService]
})
export class ProductsAddComponent
  extends BaseFormComponent<GetProductByIdQueryDto>
  implements OnInit {

  private api = inject(ProductsApiService);
  private categoriesApi = inject(ProductCategoriesApiService);
  private formService = inject(ProductFormService);
  private router = inject(Router);
  private toaster = inject(ToasterService);

  categories: ListProductCategoriesQueryDto[] = [];
  initialStock = 0;
  initialStockReason = 'Initial stock load';

  ngOnInit(): void {
    this.initForm(false);
    this.loadCategories();
  }

  protected loadData(): void {
    // not used in add mode
  }

  protected override initForm(isEdit: boolean): void {
    super.initForm(isEdit);
    this.form = this.formService.createProductForm();
  }

  protected save(): void {
    if (this.form.invalid || this.isLoading) {
      return;
    }

    this.startLoading();

    const command: CreateProductCommand = {
      name: this.form.value.name,
      description: this.form.value.description,
      brand: this.form.value.brand,
      subcategory: this.form.value.subcategory,
      price: this.form.value.price,
      isEnabled: this.form.value.isEnabled,
      categoryId: this.form.value.categoryId
    };

    this.api.create(command)
      .pipe(
        switchMap((productId) => {
          if (this.initialStock <= 0) {
            return of(null);
          }

          const stockPayload: AdjustProductStockCommand = {
            quantityDelta: this.initialStock,
            reason: this.initialStockReason.trim() || 'Initial stock load',
            note: 'Set during product creation'
          };

          return this.api.adjustStock(productId, stockPayload);
        })
      )
      .subscribe({
      next: () => {
        this.stopLoading();
        this.toaster.success('Product created successfully');
        this.router.navigate(['/admin/products']);
      },
      error: (err) => {
        this.stopLoading('Failed to create product');
        console.error('Create product error:', err);
      }
    });
  }

  private loadCategories(): void {
    const request = new ListProductCategoriesRequest();
    request.onlyEnabled = true;
    request.paging.pageSize = 100;

    this.categoriesApi.list(request).subscribe({
      next: (response) => {
        this.categories = response.items.filter(x => x.isEnabled);
      },
      error: (err) => {
        this.toaster.error('Failed to load categories');
        console.error('Load categories error:', err);
      }
    });
  }

  onCancel(): void {
    this.router.navigate(['/admin/products']);
  }

  getErrorMessage(controlName: string): string {
    return this.formService.getErrorMessage(this.form, controlName);
  }

  isBasicStepValid(): boolean {
    return !this.form.get('name')?.invalid &&
      !this.form.get('brand')?.invalid &&
      !this.form.get('description')?.invalid;
  }

  isPricingStepValid(): boolean {
    return !!this.form.get('price')?.valid && !!this.form.get('categoryId')?.valid;
  }
}
