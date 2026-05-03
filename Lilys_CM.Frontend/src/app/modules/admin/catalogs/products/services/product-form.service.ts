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

      slug: ['', [Validators.maxLength(160)]],

      brand: [
        product?.brand ?? '',
        [Validators.maxLength(120)]
      ],

      shortDescription: [
        '',
        [Validators.maxLength(500)]
      ],

      description: [
        product?.description ?? '',
        [Validators.maxLength(4000)]
      ],

      ingredients: ['', [Validators.maxLength(4000)]],
      howToUse: ['', [Validators.maxLength(2000)]],
      benefits: ['', [Validators.maxLength(2000)]],

      size: ['', [Validators.maxLength(50)]],
      countryOfOrigin: ['', [Validators.maxLength(120)]],
      barcode: ['', [Validators.maxLength(100)]],

      price: [
        product?.price ?? 0,
        [Validators.required, Validators.min(0.01), Validators.max(1000000)]
      ],

      compareAtPrice: [
        null,
        [Validators.min(0)]
      ],

      stockQuantity: [
        product?.stockQuantity ?? 0,
        [Validators.required, Validators.min(0)]
      ],

      isEnabled: [product?.isEnabled ?? true],
      isFeatured: [product?.isFeatured ?? false],

      seoTitle: ['', [Validators.maxLength(200)]],
      seoDescription: ['', [Validators.maxLength(500)]],

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
      return 'This field is required';
    }
    if (errors['minlength']) {
      return `Minimum ${errors['minlength'].requiredLength} characters required`;
    }
    if (errors['maxlength']) {
      return `Maximum ${errors['maxlength'].requiredLength} characters allowed`;
    }
    if (errors['min']) {
      return `Minimum value is ${errors['min'].min}`;
    }
    if (errors['max']) {
      return `Maximum value is ${errors['max'].max}`;
    }
    if (errors['email']) {
      return 'Invalid email format';
    }

    return 'Invalid value';
  }
}
