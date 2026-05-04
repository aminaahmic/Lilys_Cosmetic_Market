import { SubcategoriesApiService } from '../../../../../api-services/subcategories/subcategories-api.service';
import { SubcategoryDto } from '../../../../../api-services/subcategories/subcategories-api.models';
import { Component, inject, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { forkJoin } from 'rxjs';
import { ProductFormService } from '../services/product-form.service';
import { BaseFormComponent } from '../../../../../core/components/base-classes/base-form-component';
import { ProductsApiService } from '../../../../../api-services/products/products-api.service';
import { ProductCategoriesApiService } from '../../../../../api-services/product-categories/product-categories-api.service';
import { ToasterService } from '../../../../../core/services/toaster.service';
import { MatDialog } from '@angular/material/dialog';
import { ProductVariantDeleteDialogComponent } from '../product-variant-delete-dialog/product-variant-delete-dialog.component';
import { BrandsApiService } from '../../../../../api-services/brands/brands-api.service';
import { BrandDto } from '../../../../../api-services/brands/brands-api.models';
import {
  ListProductCategoriesQueryDto,
  ListProductCategoriesRequest
} from '../../../../../api-services/product-categories/product-categories-api.model';
import {
  AdjustProductStockCommand,
  CreateProductVariantCommand,
  GetProductByIdQueryDto,
  ListProductStockMovementsRequest,
  ProductStockMovementDto,
  ProductVariantDto,
  UpdateProductCommand,
  UpdateProductVariantCommand
} from '../../../../../api-services/products/products-api.models';
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
  private dialog = inject(MatDialog);
  private brandsApi = inject(BrandsApiService);
  brands: BrandDto[] = [];

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
  variants: ProductVariantDto[] = [];

  variantName = '';
  variantValue = '';
  variantPrice: number | null = null;
  variantStock: number | null = null;
  isVariantBusy = false;
  editingVariantId: number | null = null;

  private subApi = inject(SubcategoriesApiService);
  ngOnInit(): void {
    this.productId = +this.route.snapshot.params['id'];
    this.initForm(true);
    this.loadImages();
    this.loadVariants();
    this.loadBrands();
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
  private loadBrands(): void {
    this.brandsApi.getAll(true, null).subscribe({
      next: (response) => {
        this.brands = response;
      },
      error: (err) => {
        this.toaster.error('Greška prilikom učitavanja brendova.');
        console.error('Load brands error:', err);
      }
    });
  }

  getSelectedBrandName(): string {
    const brandId = this.form.get('brandId')?.value;

    if (!brandId) {
      return 'Bez brenda';
    }

    return this.brands.find(x => x.id === Number(brandId))?.name ?? 'Bez brenda';
  }

  protected override save(): void {
    if (this.form.invalid || this.isLoading) {
      this.form.markAllAsTouched();
      return;
    }

    this.startLoading();

    const value = this.form.getRawValue();

    const price = Number(value.price);
    const compareAtPrice = value.compareAtPrice
      ? Number(value.compareAtPrice)
      : null;

    if (compareAtPrice !== null && compareAtPrice < price) {
      this.stopLoading();
      this.toaster.warning('Stara cijena mora biti veća ili jednaka trenutnoj cijeni.');
      return;
    }

    const command: UpdateProductCommand = {
      name: value.name,
      sku: value.sku,

      slug: value.slug || null,
      imageUrl: value.imageUrl || null,

      shortDescription: value.shortDescription || null,
      description: value.description || null,
      ingredients: value.ingredients || null,
      howToUse: value.howToUse || null,
      benefits: value.benefits || null,

      brand: null,
      brandId: value.brandId ? Number(value.brandId) : null,
      size: value.size || null,
      countryOfOrigin: value.countryOfOrigin || null,
      barcode: value.barcode || null,

      price,
      compareAtPrice,

      stockQuantity: Number(value.stockQuantity ?? 0),

      isEnabled: Boolean(value.isEnabled),
      isFeatured: Boolean(value.isFeatured),

      seoTitle: value.seoTitle || null,
      seoDescription: value.seoDescription || null,

      categoryId: Number(value.categoryId),
      subcategoryId: value.subcategoryId ? Number(value.subcategoryId) : null
    };

    this.api.update(this.productId, command).subscribe({
      next: () => {
        this.stopLoading();
        this.toaster.success('Product updated successfully');
        this.router.navigate(['/admin/products']);
      },
      error: (err) => {
        this.stopLoading('Failed to update product');
        this.toaster.error('Failed to update product');
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

        this.form.patchValue({
          stockQuantity: product.stockQuantity
        });

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
      !this.form.get('brandId')?.invalid &&
      !this.form.get('description')?.invalid;
  }

  isPricingStepValid(): boolean {
    return !!this.form.get('price')?.valid && !!this.form.get('categoryId')?.valid;
  }
  loadVariants(): void {
    if (!this.productId) {
      return;
    }

    this.api.getVariants(this.productId).subscribe({
      next: (response) => {
        this.variants = response;
      },
      error: (err) => {
        this.toaster.error('Greška prilikom učitavanja varijanti.');
        console.error('Load variants error:', err);
      }
    });
  }
  addVariant(): void {
    if (this.isVariantBusy) {
      return;
    }

    const optionName = this.variantName.trim();
    const optionValue = this.variantValue.trim();

    const productPrice = Number(this.form.get('price')?.value ?? 0);
    const productStock = Number(this.form.get('stockQuantity')?.value ?? 0);

    const price = this.variantPrice !== null
      ? Number(this.variantPrice)
      : productPrice;

    const stock = this.variantStock !== null
      ? Number(this.variantStock)
      : productStock;

    if (Number.isNaN(price) || Number.isNaN(stock)) {
      this.toaster.warning('Cijena i stanje varijante moraju biti validni brojevi.');
      return;
    }

    if (!optionName || !optionValue) {
      this.toaster.warning('Unesite naziv opcije i vrijednost opcije.');
      return;
    }

    if (price < 0) {
      this.toaster.warning('Cijena varijante ne može biti negativna.');
      return;
    }

    if (stock < 0) {
      this.toaster.warning('Stanje varijante ne može biti negativno.');
      return;
    }

    const alreadyExists = this.variants.some(variant =>
      variant.options.some(option =>
        option.optionName.toLowerCase() === optionName.toLowerCase() &&
        option.value.toLowerCase() === optionValue.toLowerCase()
      )
    );

    if (alreadyExists) {
      this.toaster.warning('Ova varijanta već postoji.');
      return;
    }

    const command: CreateProductVariantCommand = {
      price,
      stock,
      options: [
        {
          optionName,
          value: optionValue
        }
      ]
    };

    this.isVariantBusy = true;

    this.api.createVariant(this.productId, command).subscribe({
      next: () => {
        this.toaster.success('Varijanta je uspješno dodana.');
        this.variantName = '';
        this.variantValue = '';
        this.variantPrice = null;
        this.variantStock = null;
        this.isVariantBusy = false;
        this.loadVariants();
      },
      error: (err) => {
        this.isVariantBusy = false;
        this.toaster.error('Greška prilikom dodavanja varijante.');
        console.error('Create variant error:', err);
      }
    });
  }
  startEditVariant(variant: ProductVariantDto): void {
    const firstOption = variant.options[0];

    if (!firstOption) {
      this.toaster.warning('Ova varijanta nema opciju za uređivanje.');
      return;
    }

    this.editingVariantId = variant.id;
    this.variantName = firstOption.optionName;
    this.variantValue = firstOption.value;
    this.variantPrice = variant.price;
    this.variantStock = variant.stock;
  }

  cancelVariantEdit(): void {
    this.editingVariantId = null;
    this.variantName = '';
    this.variantValue = '';
    this.variantPrice = null;
    this.variantStock = null;
  }
  saveVariant(): void {
    if (this.editingVariantId === null) {
      this.addVariant();
      return;
    }

    if (this.isVariantBusy) {
      return;
    }

    const optionName = this.variantName.trim();
    const optionValue = this.variantValue.trim();

    const productPrice = Number(this.form.get('price')?.value ?? 0);
    const productStock = Number(this.form.get('stockQuantity')?.value ?? 0);

    const price = this.variantPrice !== null
      ? Number(this.variantPrice)
      : productPrice;

    const stock = this.variantStock !== null
      ? Number(this.variantStock)
      : productStock;

    if (!optionName || !optionValue) {
      this.toaster.warning('Unesite naziv opcije i vrijednost opcije.');
      return;
    }

    if (Number.isNaN(price) || Number.isNaN(stock)) {
      this.toaster.warning('Cijena i stanje varijante moraju biti validni brojevi.');
      return;
    }

    if (price < 0) {
      this.toaster.warning('Cijena varijante ne može biti negativna.');
      return;
    }

    if (stock < 0) {
      this.toaster.warning('Stanje varijante ne može biti negativno.');
      return;
    }

    const alreadyExists = this.variants.some(variant =>
      variant.id !== this.editingVariantId &&
      variant.options.some(option =>
        option.optionName.toLowerCase() === optionName.toLowerCase() &&
        option.value.toLowerCase() === optionValue.toLowerCase()
      )
    );

    if (alreadyExists) {
      this.toaster.warning('Ova varijanta već postoji.');
      return;
    }

    const command: UpdateProductVariantCommand = {
      price,
      stock,
      options: [
        {
          optionName,
          value: optionValue
        }
      ]
    };

    this.isVariantBusy = true;

    this.api.updateVariant(this.productId, this.editingVariantId, command).subscribe({
      next: () => {
        this.toaster.success('Varijanta je uspješno ažurirana.');
        this.cancelVariantEdit();
        this.isVariantBusy = false;
        this.loadVariants();
      },
      error: (err) => {
        this.isVariantBusy = false;
        this.toaster.error('Greška prilikom ažuriranja varijante.');
        console.error('Update variant error:', err);
      }
    });
  }
  deleteVariant(variantId: number): void {
    if (!this.productId || this.isVariantBusy) {
      return;
    }

    const variant = this.variants.find(x => x.id === variantId);

    const variantLabel = variant?.options?.length
      ? variant.options.map(option => `${option.optionName}: ${option.value}`).join(', ')
      : 'Varijanta proizvoda';

    const dialogRef = this.dialog.open(ProductVariantDeleteDialogComponent, {
      width: '440px',
      data: {
        variantLabel
      }
    });

    dialogRef.afterClosed().subscribe(confirmed => {
      if (!confirmed) {
        return;
      }

      this.isVariantBusy = true;

      this.api.deleteVariant(this.productId, variantId).subscribe({
        next: () => {
          this.toaster.success('Varijanta je obrisana.');
          this.isVariantBusy = false;
          this.loadVariants();
        },
        error: (err) => {
          this.isVariantBusy = false;
          this.toaster.error('Greška prilikom brisanja varijante.');
          console.error('Delete variant error:', err);
        }
      });
    });
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
