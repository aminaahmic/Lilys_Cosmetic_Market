import { Component, inject, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { forkJoin } from 'rxjs';

import { ProductFormService } from '../services/product-form.service';
import {
  CreateProductCommand,
  CreateProductVariantCommand,
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

import { SubcategoriesApiService } from '../../../../../api-services/subcategories/subcategories-api.service';

type ProductVariantDraft = {
  optionName: string;
  optionValue: string;
  price: number;
  stock: number;
};

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
  private subApi = inject(SubcategoriesApiService);
  private formService = inject(ProductFormService);
  private router = inject(Router);
  private toaster = inject(ToasterService);

  categories: ListProductCategoriesQueryDto[] = [];
  subcategories: any[] = [];

  selectedImages: File[] = [];

  protected variantName = '';
  protected variantValue = '';
  protected variantPrice: number | null = null;
  protected variantStock: number | null = null;

  protected variants: ProductVariantDraft[] = [];

  ngOnInit(): void {
    this.initForm(false);
    this.loadCategories();

    this.form.get('categoryId')?.valueChanges.subscribe(categoryId => {
      if (categoryId) {
        this.onCategoryChanged(Number(categoryId));
      } else {
        this.subcategories = [];
        this.form.get('subcategoryId')?.setValue(null);
      }
    });
  }

  protected loadData(): void {
    // Nije potrebno u add mode-u.
  }

  protected override initForm(isEdit: boolean): void {
    super.initForm(isEdit);
    this.form = this.formService.createProductForm();
  }

  override save(): void {
    this.onSubmit();
  }

  override onSubmit(): void {
    if (this.form.invalid || this.isLoading) {
      this.form.markAllAsTouched();
      this.toaster.warning('Popunite obavezna polja prije kreiranja proizvoda.');
      return;
    }

    this.startLoading();

    const value = this.form.getRawValue();

    const command: CreateProductCommand = {
      name: value.name,
      sku: value.sku,
      slug: value.slug || null,

      shortDescription: value.shortDescription || null,
      description: value.description || null,
      ingredients: value.ingredients || null,
      howToUse: value.howToUse || null,
      benefits: value.benefits || null,

      brand: value.brand || null,
      size: value.size || null,
      countryOfOrigin: value.countryOfOrigin || null,
      barcode: value.barcode || null,

      price: Number(value.price),
      compareAtPrice: value.compareAtPrice ? Number(value.compareAtPrice) : null,

      stockQuantity: Number(value.stockQuantity ?? 0),

      isEnabled: Boolean(value.isEnabled),
      isFeatured: Boolean(value.isFeatured),

      seoTitle: value.seoTitle || null,
      seoDescription: value.seoDescription || null,

      categoryId: Number(value.categoryId),
      subcategoryId: value.subcategoryId ? Number(value.subcategoryId) : null
    };

    this.api.create(command).subscribe({
      next: (productId) => {
        this.createVariantsAfterCreate(productId);
      },
      error: (err) => {
        this.stopLoading('Greška prilikom kreiranja proizvoda.');
        this.toaster.error('Greška prilikom kreiranja proizvoda.');
        console.error('Create product error:', err);
      }
    });
  }

  protected onImagesSelected(event: Event): void {
    const input = event.target as HTMLInputElement;

    if (!input.files || input.files.length === 0) {
      return;
    }

    this.selectedImages = Array.from(input.files);
  }

  protected addVariant(): void {
    const optionName = this.variantName.trim();
    const optionValue = this.variantValue.trim();

    const defaultPrice = Number(this.form.get('price')?.value ?? 0);
    const defaultStock = Number(this.form.get('stockQuantity')?.value ?? 0);

    const price = this.variantPrice !== null
      ? Number(this.variantPrice)
      : defaultPrice;

    const stock = this.variantStock !== null
      ? Number(this.variantStock)
      : defaultStock;

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
      variant.optionName.toLowerCase() === optionName.toLowerCase() &&
      variant.optionValue.toLowerCase() === optionValue.toLowerCase()
    );

    if (alreadyExists) {
      this.toaster.warning('Ova varijanta je već dodana.');
      return;
    }

    this.variants = [
      ...this.variants,
      {
        optionName,
        optionValue,
        price,
        stock
      }
    ];

    this.variantName = '';
    this.variantValue = '';
    this.variantPrice = null;
    this.variantStock = null;
  }

  protected removeVariant(index: number): void {
    this.variants = this.variants.filter((_, i) => i !== index);
  }

  private createVariantsAfterCreate(productId: number): void {
    if (this.variants.length === 0) {
      this.uploadImagesAfterCreate(productId);
      return;
    }

    const requests = this.variants.map(variant => {
      const command: CreateProductVariantCommand = {
        price: Number(variant.price),
        stock: Number(variant.stock),
        options: [
          {
            optionName: variant.optionName,
            value: variant.optionValue
          }
        ]
      };

      return this.api.createVariant(productId, command);
    });

    forkJoin(requests).subscribe({
      next: () => {
        this.uploadImagesAfterCreate(productId);
      },
      error: (err) => {
        this.stopLoading('Proizvod je kreiran, ali varijante nisu spašene.');
        this.toaster.error('Proizvod je kreiran, ali varijante nisu spašene.');
        console.error('Create variants error:', err);
      }
    });
  }

  private uploadImagesAfterCreate(productId: number): void {
    if (this.selectedImages.length === 0) {
      this.finishCreate();
      return;
    }

    const uploads = this.selectedImages.map(file =>
      this.api.uploadImage(productId, file)
    );

    forkJoin(uploads).subscribe({
      next: () => {
        this.finishCreate();
      },
      error: (err) => {
        this.stopLoading();
        this.toaster.warning('Proizvod je kreiran, ali neke slike nisu uploadovane.');
        console.error('Upload images error:', err);
        this.router.navigate(['/admin/products']);
      }
    });
  }

  private finishCreate(): void {
    this.stopLoading();
    this.toaster.success('Proizvod, varijante i slike su uspješno obrađeni.');
    this.router.navigate(['/admin/products']);
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
        this.toaster.error('Greška prilikom učitavanja kategorija.');
        console.error('Load categories error:', err);
      }
    });
  }

  protected onCategoryChanged(categoryId: number): void {
    this.subApi.getByCategory(categoryId).subscribe({
      next: (response) => {
        this.subcategories = response;
        this.form.get('subcategoryId')?.setValue(null);
      },
      error: (err) => {
        this.subcategories = [];
        this.toaster.error('Greška prilikom učitavanja potkategorija.');
        console.error('Load subcategories error:', err);
      }
    });
  }

  protected onCancel(): void {
    this.router.navigate(['/admin/products']);
  }

  protected getErrorMessage(controlName: string): string {
    return this.formService.getErrorMessage(this.form, controlName);
  }

  protected isBasicStepValid(): boolean {
    return !!this.form.get('name')?.valid &&
      !!this.form.get('sku')?.valid &&
      !!this.form.get('brand')?.valid &&
      !!this.form.get('shortDescription')?.valid &&
      !!this.form.get('description')?.valid;
  }

  protected isPricingStepValid(): boolean {
    return !!this.form.get('categoryId')?.valid &&
      !!this.form.get('price')?.valid &&
      !!this.form.get('stockQuantity')?.valid;
  }
}