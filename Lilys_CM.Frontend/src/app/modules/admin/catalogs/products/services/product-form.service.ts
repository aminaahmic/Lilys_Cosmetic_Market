import { Injectable, inject } from '@angular/core';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';
import { GetProductByIdQueryDto } from '../../../../../api-services/products/products-api.models';

@Injectable()
export class ProductFormService {
  private fb = inject(FormBuilder);

  createProductForm(product?: GetProductByIdQueryDto): FormGroup {
    return this.fb.group({
      name: [
        product?.name ?? '',
        [Validators.required, Validators.minLength(3), Validators.maxLength(200)]
      ],

      sku: [
        product?.sku ?? '',
        [Validators.required, Validators.minLength(2), Validators.maxLength(64)]
      ],

      slug: [
        product?.slug ?? '',
        [Validators.maxLength(160)]
      ],

      // Novo profesionalno povezivanje:
      // Product više bira brand preko BrandEntity/BrandId.
      brandId: [
        product?.brandId ?? null
      ],

      // Ostavljamo privremeno radi kompatibilnosti sa starim backend/modelima.
      // UI više ne treba koristiti ovaj input, nego brandId dropdown.
      brand: [
        product?.brand ?? '',
        [Validators.maxLength(120)]
      ],

      shortDescription: [
        product?.shortDescription ?? '',
        [Validators.maxLength(500)]
      ],

      description: [
        product?.description ?? '',
        [Validators.maxLength(4000)]
      ],

      ingredients: [
        product?.ingredients ?? '',
        [Validators.maxLength(4000)]
      ],

      howToUse: [
        product?.howToUse ?? '',
        [Validators.maxLength(2000)]
      ],

      benefits: [
        product?.benefits ?? '',
        [Validators.maxLength(2000)]
      ],

      size: [
        product?.size ?? '',
        [Validators.maxLength(50)]
      ],

      countryOfOrigin: [
        product?.countryOfOrigin ?? '',
        [Validators.maxLength(120)]
      ],

      barcode: [
        product?.barcode ?? '',
        [Validators.maxLength(100)]
      ],

      price: [
        product?.price ?? 0,
        [Validators.required, Validators.min(0.01), Validators.max(1000000)]
      ],

      compareAtPrice: [
        product?.compareAtPrice ?? null,
        [Validators.min(0)]
      ],

      stockQuantity: [
        product?.stockQuantity ?? 0,
        [Validators.required, Validators.min(0)]
      ],

      isEnabled: [
        product?.isEnabled ?? true
      ],

      isFeatured: [
        product?.isFeatured ?? false
      ],

      seoTitle: [
        product?.seoTitle ?? '',
        [Validators.maxLength(200)]
      ],

      seoDescription: [
        product?.seoDescription ?? '',
        [Validators.maxLength(500)]
      ],

      categoryId: [
        product?.categoryId ?? null,
        [Validators.required]
      ],

      subcategoryId: [
        product?.subcategoryId ?? null
      ]
    });
  }

  getErrorMessage(form: FormGroup, controlName: string): string {
    const control = form.get(controlName);

    if (!control || !control.errors || !control.touched) {
      return '';
    }

    const errors = control.errors;

    if (errors['required']) {
      return 'Ovo polje je obavezno.';
    }

    if (errors['minlength']) {
      return `Minimalno ${errors['minlength'].requiredLength} karaktera.`;
    }

    if (errors['maxlength']) {
      return `Maksimalno ${errors['maxlength'].requiredLength} karaktera.`;
    }

    if (errors['min']) {
      return `Minimalna vrijednost je ${errors['min'].min}.`;
    }

    if (errors['max']) {
      return `Maksimalna vrijednost je ${errors['max'].max}.`;
    }

    if (errors['email']) {
      return 'Email format nije ispravan.';
    }

    return 'Neispravna vrijednost.';
  }
}