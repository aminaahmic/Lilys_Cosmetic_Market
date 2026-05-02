import { SubcategoriesApiService, SubcategoryDto } from '../../../../../api-services/subcategories/subcategories-api.service';
import { Component, inject, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { forkJoin } from 'rxjs';
import { ProductFormService } from '../services/product-form.service';
import { BaseFormComponent } from '../../../../../core/components/base-classes/base-form-component';
import {
  AdjustProductStockCommand,
  GetProductByIdQueryDto,
  ListProductStockMovementsRequest,
  ProductStockMovementDto,
  UpdateProductCommand
} from '../../../../../api-services/products/products-api.models';
import { ProductsApiService } from '../../../../../api-services/products/products-api.service';
import { ProductCategoriesApiService } from '../../../../../api-services/product-categories/product-categories-api.service';
import { ToasterService } from '../../../../../core/services/toaster.service';
import {
  ListProductCategoriesQueryDto,
  ListProductCategoriesRequest
} from '../../../../../api-services/product-categories/product-categories-api.model';

@Component({
  selector: 'app-products-edit',
  standalone: false,
  templateUrl: './products-edit.component.html',
  styleUrl: './products-edit.component.scss',
  providers: [ProductFormService]
})
export class ProductsEditComponent
  extends BaseFormComponent<GetProductByIdQueryDto>
  implements OnInit {

  private api = inject(ProductsApiService);
  private categoriesApi = inject(ProductCategoriesApiService);
  private formService = inject(ProductFormService);
  private route = inject(ActivatedRoute);
  private router = inject(Router);
  private toaster = inject(ToasterService);

  productId!: number;
  categories: ListProductCategoriesQueryDto[] = [];
  stockMovements: ProductStockMovementDto[] = [];
  stockDelta = 0;
  stockReason = 'Manual correction';
  stockNote = '';
  isStockBusy = false;
  images: any[] = [];
  selectedFile: File | null = null;
  subcategories: SubcategoryDto[] = [];

  private subApi = inject(SubcategoriesApiService);
  ngOnInit(): void {
    this.productId = +this.route.snapshot.params['id'];
    this.initForm(true);
    this.loadImages();
  }

  protected loadData(): void {
  this.startLoading();

  const categoriesRequest = new ListProductCategoriesRequest();
  categoriesRequest.onlyEnabled = true;
  categoriesRequest.paging.pageSize = 100;

  const movementsRequest = new ListProductStockMovementsRequest();
  movementsRequest.paging.pageSize = 8;

  forkJoin({
    product: this.api.getById(this.productId),
    categories: this.categoriesApi.list(categoriesRequest),
    movements: this.api.getStockMovements(this.productId, movementsRequest)
  }).subscribe({
    next: ({ product, categories, movements }) => {
      this.model = product;
      this.categories = categories.items.filter(x => x.isEnabled);
      this.stockMovements = movements.items;
      this.form = this.formService.createProductForm(product);

      if (product.categoryId) {
        this.subApi.getByCategory(product.categoryId).subscribe((res: SubcategoryDto[]) => {
          this.subcategories = res;
        });
      }

      this.stopLoading();
    },
    error: (err) => {
      this.stopLoading('Failed to load product');
      this.toaster.error('Product not found');
      console.error('Load product error:', err);
      this.router.navigate(['/admin/products']);
    }
  });
}

  protected save(): void {
    if (this.form.invalid || this.isLoading) {
      return;
    }

    this.startLoading();

    const command: UpdateProductCommand = {
      name: this.form.value.name,
      description: this.form.value.description,
      brand: this.form.value.brand,
      subcategoryId: this.form.value.subcategoryId,
      price: this.form.value.price,
      isEnabled: this.form.value.isEnabled,
      categoryId: this.form.value.categoryId
    };

    this.api.update(this.productId, command).subscribe({
      next: () => {
        this.stopLoading();
        this.toaster.success('Product updated successfully');
        this.router.navigate(['/admin/products']);
      },
      error: (err) => {
        this.stopLoading('Failed to update product');
        console.error('Update product error:', err);
      }
    });
  }

  applyStockChange(): void {
    if (this.isStockBusy || this.stockDelta === 0) {
      return;
    }

    this.isStockBusy = true;

    const payload: AdjustProductStockCommand = {
      quantityDelta: this.stockDelta,
      reason: this.stockReason.trim() || 'Manual correction',
      note: this.stockNote?.trim() || null
    };

    this.api.adjustStock(this.productId, payload).subscribe({
      next: () => {
        this.toaster.success('Stock updated');
        this.stockDelta = 0;
        this.stockNote = '';
        this.reloadStockPanel();
      },
      error: (err) => {
        this.isStockBusy = false;
        this.toaster.error('Failed to update stock');
        console.error('Adjust stock error:', err);
      }
    });
  }

  private reloadStockPanel(): void {
    const movementsRequest = new ListProductStockMovementsRequest();
    movementsRequest.paging.pageSize = 8;

    forkJoin({
      product: this.api.getById(this.productId),
      movements: this.api.getStockMovements(this.productId, movementsRequest)
    }).subscribe({
      next: ({ product, movements }) => {
        this.model = product;
        this.stockMovements = movements.items;
        this.isStockBusy = false;
      },
      error: (err) => {
        this.isStockBusy = false;
        this.toaster.error('Stock updated but refresh failed');
        console.error('Reload stock panel error:', err);
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
  loadImages(): void {
    if (!this.productId) return;

    this.api.getImages(this.productId).subscribe(res => {
      this.images = res;
    });
  }

  onFileSelected(event: Event): void {
    const input = event.target as HTMLInputElement;
    this.selectedFile = input.files?.[0] ?? null;
  }
  onCategoryChanged(categoryId: number): void {
    this.form.patchValue({ subcategoryId: null });

    this.subApi.getByCategory(categoryId).subscribe((res: SubcategoryDto[]) => {
      this.subcategories = res;
    });
  }
  uploadImage(): void {
    if (!this.selectedFile || !this.productId) return;

    this.api.uploadImage(this.productId, this.selectedFile).subscribe(() => {
      this.selectedFile = null;
      this.loadImages();
    });
  }

  deleteImage(imageId: number): void {
    if (!this.productId) return;

    this.api.deleteImage(this.productId, imageId).subscribe(() => {
      this.loadImages();
    });
  }

  setMain(imageId: number): void {
    if (!this.productId) return;

    this.api.setMainImage(this.productId, imageId).subscribe(() => {
      this.loadImages();
    });
  }
}
