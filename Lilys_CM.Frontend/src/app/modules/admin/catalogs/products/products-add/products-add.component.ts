import { Component, inject, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { distinctUntilChanged, forkJoin } from 'rxjs';
import { MatStepper } from '@angular/material/stepper';

import { ProductFormService } from '../services/product-form.service';
import {
  CreateProductCommand,
  CreateProductVariantCommand,
  GetProductByIdQueryDto
} from '../../../../../api-services/products/products-api.models';
import { OptionsApiService, OptionDto } from '../../../../../api-services/options/options-api.service';
import { BaseFormComponent } from '../../../../../core/components/base-classes/base-form-component';
import { ProductsApiService } from '../../../../../api-services/products/products-api.service';
import { ProductCategoriesApiService } from '../../../../../api-services/product-categories/product-categories-api.service';
import { ToasterService } from '../../../../../core/services/toaster.service';
import { BrandsApiService } from '../../../../../api-services/brands/brands-api.service';
import { BrandDto } from '../../../../../api-services/brands/brands-api.models';
import {
  ListProductCategoriesQueryDto,
  ListProductCategoriesRequest
} from '../../../../../api-services/product-categories/product-categories-api.model';
import { SubcategoriesApiService } from '../../../../../api-services/subcategories/subcategories-api.service';

type VariantOptionDraft = {
  optionId: number;
  optionName: string;
  value: string;
};

type ProductVariantDraft = {
  options: VariantOptionDraft[];
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
  private brandsApi = inject(BrandsApiService);
  private optionsApi = inject(OptionsApiService);
  private subApi = inject(SubcategoriesApiService);
  private formService = inject(ProductFormService);
  private router = inject(Router);
  private toaster = inject(ToasterService);

  brands: BrandDto[] = [];
  options: OptionDto[] = [];
  categories: ListProductCategoriesQueryDto[] = [];
  subcategories: any[] = [];

  selectedImages: File[] = [];

  protected variantOptionId: number | null = null;
  protected variantValue = '';
  protected variantPrice: number | null = null;
  protected variantStock: number | null = null;
  protected selectedVariantOptions: VariantOptionDraft[] = [];
  protected variants: ProductVariantDraft[] = [];

  ngOnInit(): void {
    this.initForm(false);
    this.loadCategories();
    this.loadBrands();
    this.loadOptions();

  this.form.get('categoryId')?.valueChanges
  .pipe(distinctUntilChanged())
  .subscribe(categoryId => {
    if (categoryId) {
      this.onCategoryChanged(Number(categoryId));
    } else {
      this.subcategories = [];
      this.form.get('subcategoryId')?.setValue(null, { emitEvent: false });
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

    const brandId =
      value.brandId !== null &&
      value.brandId !== undefined &&
      value.brandId !== ''
        ? Number(value.brandId)
        : null;

    const subcategoryId =
      value.subcategoryId !== null &&
      value.subcategoryId !== undefined &&
      value.subcategoryId !== ''
        ? Number(value.subcategoryId)
        : null;

    const categoryId =
      value.categoryId !== null &&
      value.categoryId !== undefined &&
      value.categoryId !== ''
        ? Number(value.categoryId)
        : null;

    if (!categoryId) {
      this.stopLoading();
      this.toaster.warning('Odaberite kategoriju proizvoda.');
      return;
    }

    const price = Number(value.price);

    const compareAtPrice =
      value.compareAtPrice !== null &&
      value.compareAtPrice !== undefined &&
      value.compareAtPrice !== ''
        ? Number(value.compareAtPrice)
        : null;

    if (compareAtPrice !== null && compareAtPrice < price) {
      this.stopLoading();
      this.toaster.warning('Stara cijena mora biti veća ili jednaka trenutnoj cijeni.');
      return;
    }

    const command: CreateProductCommand = {
      name: value.name,
      sku: value.sku,
      slug: value.slug || null,

      shortDescription: value.shortDescription || null,
      description: value.description || null,
      ingredients: value.ingredients || null,
      howToUse: value.howToUse || null,
      benefits: value.benefits || null,

      brandId,
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

      categoryId,
      subcategoryId
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

  private loadOptions(): void {
    this.optionsApi.getAll().subscribe({
      next: (response) => {
        this.options = response;
      },
      error: (err) => {
        this.toaster.error('Greška prilikom učitavanja opcija.');
        console.error('Load options error:', err);
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

  protected onImagesSelected(event: Event): void {
    const input = event.target as HTMLInputElement;

    if (!input.files || input.files.length === 0) {
      return;
    }

    this.selectedImages = Array.from(input.files);
  }

  protected addVariantOption(): void {
    const optionId = Number(this.variantOptionId);
    const selectedOption = this.options.find(option => option.id === optionId);
    const optionName = selectedOption?.name?.trim() ?? '';
    const optionValue = this.variantValue.trim();

    if (!optionId || !selectedOption || !optionValue) {
      this.toaster.warning('Odaberite opciju i unesite vrijednost opcije.');
      return;
    }

    const optionAlreadyAdded = this.selectedVariantOptions.some(option =>
      option.optionId === optionId
    );

    if (optionAlreadyAdded) {
      this.toaster.warning('Ova opcija je već dodana u trenutnu varijantu.');
      return;
    }

    this.selectedVariantOptions = [
      ...this.selectedVariantOptions,
      {
        optionId,
        optionName,
        value: optionValue
      }
    ];

    this.variantOptionId = null;
    this.variantValue = '';
  }

  protected removeVariantOption(index: number): void {
    this.selectedVariantOptions = this.selectedVariantOptions.filter((_, i) => i !== index);
  }

  protected addVariant(): void {
    const defaultPrice = Number(this.form.get('price')?.value ?? 0);
    const defaultStock = Number(this.form.get('stockQuantity')?.value ?? 0);

    const price = this.variantPrice !== null
      ? Number(this.variantPrice)
      : defaultPrice;

    const stock = this.variantStock !== null
      ? Number(this.variantStock)
      : defaultStock;

    if (this.selectedVariantOptions.length === 0) {
      this.toaster.warning('Dodajte barem jednu opciju za varijantu.');
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

    const newVariantKey = this.getVariantKey(this.selectedVariantOptions);

    const alreadyExists = this.variants.some(variant =>
      this.getVariantKey(variant.options) === newVariantKey
    );

    if (alreadyExists) {
      this.toaster.warning('Ova kombinacija opcija već postoji.');
      return;
    }

    this.variants = [
      ...this.variants,
      {
        options: [...this.selectedVariantOptions],
        price,
        stock
      }
    ];

    this.selectedVariantOptions = [];
    this.variantOptionId = null;
    this.variantValue = '';
    this.variantPrice = null;
    this.variantStock = null;
  }

  protected removeVariant(index: number): void {
    this.variants = this.variants.filter((_, i) => i !== index);
  }

  private getVariantKey(options: VariantOptionDraft[]): string {
    return options
      .map(option => `${option.optionId}:${option.value.trim().toLowerCase()}`)
      .sort()
      .join('|');
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
        options: variant.options.map(option => ({
          optionId: option.optionId,
          value: option.value
        }))
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
  const subcategoryControl = this.form.get('subcategoryId');

  subcategoryControl?.setValue(null, { emitEvent: false });
  this.subcategories = [];

  this.subApi.getByCategory(categoryId).subscribe({
    next: (response) => {
      this.subcategories = response;
    },
    error: (err) => {
      this.subcategories = [];
      this.toaster.error('Greška prilikom učitavanja potkategorija.');
      console.error('Load subcategories error:', err);
    }
  });
}

  protected goToNextStep(stepper: MatStepper, controlNames: string[]): void {
    const isStepValid = this.validateStep(controlNames);

    if (!isStepValid) {
      this.toaster.warning('Popunite ispravno polja u ovom koraku prije nastavka.');
      return;
    }

    stepper.next();
  }

  private validateStep(controlNames: string[]): boolean {
    let isValid = true;

    for (const controlName of controlNames) {
      const control = this.form.get(controlName);

      if (!control) {
        continue;
      }

      control.markAsTouched();
      control.updateValueAndValidity();

      if (control.invalid) {
        isValid = false;
      }
    }

    return isValid;
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
      !!this.form.get('brandId')?.valid &&
      !!this.form.get('shortDescription')?.valid &&
      !!this.form.get('description')?.valid;
  }

  protected getSelectedBrandName(): string {
    const brandId = this.form.get('brandId')?.value;

    if (!brandId) {
      return 'Bez brenda';
    }

    return this.brands.find(x => x.id === Number(brandId))?.name ?? 'Bez brenda';
  }

  protected isPricingStepValid(): boolean {
    return !!this.form.get('categoryId')?.valid &&
      !!this.form.get('price')?.valid &&
      !!this.form.get('stockQuantity')?.valid;
  }
}