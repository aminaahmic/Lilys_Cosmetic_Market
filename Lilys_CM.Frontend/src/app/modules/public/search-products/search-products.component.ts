import { Component, OnInit, inject } from '@angular/core';
import {
  ListProductsQueryDto,
  ListProductsRequest
} from '../../../api-services/products/products-api.models';
import { ProductsApiService } from '../../../api-services/products/products-api.service';
import {
  ListProductCategoriesQueryDto,
  ListProductCategoriesRequest
} from '../../../api-services/product-categories/product-categories-api.model';
import { ProductCategoriesApiService } from '../../../api-services/product-categories/product-categories-api.service';
import { environment } from '../../../../environments/environment';
@Component({
  selector: 'app-search-products',
  standalone: false,
  templateUrl: './search-products.component.html',
  styleUrl: './search-products.component.scss',
})
export class SearchProductsComponent implements OnInit {
  private productsApi = inject(ProductsApiService);
  private categoriesApi = inject(ProductCategoriesApiService);

  products: ListProductsQueryDto[] = [];
  categories: ListProductCategoriesQueryDto[] = [];
  featuredProducts: ListProductsQueryDto[] = [];

  request = new ListProductsRequest();
  isLoading = false;
  errorMessage = '';

  ngOnInit(): void {
    this.request.paging.pageSize = 12;
    this.request.isEnabled = true;
    this.loadCategories();
    this.loadProducts();
  }

  onApplyFilters(): void {
    this.request.paging.page = 1;
    this.loadProducts();
  }

  onResetFilters(): void {
    this.request.search = null;
    this.request.brand = null;
    this.request.subcategory = null;
    this.request.categoryId = null;
    this.request.priceMin = null;
    this.request.priceMax = null;
    this.request.paging.page = 1;
    this.loadProducts();
  }

  private loadProducts(): void {
    this.isLoading = true;
    this.errorMessage = '';

    this.productsApi.list(this.request).subscribe({
      next: (response) => {
        this.products = response.items;
        this.featuredProducts = response.items.slice(0, 4);
        this.isLoading = false;
      },
      error: (err) => {
        this.errorMessage = 'Could not load products right now.';
        this.isLoading = false;
        console.error('Public product load error:', err);
      }
    });
  }
  getProductImage(product: ListProductsQueryDto): string | null {
    const imageUrl =
      product.imageUrl ||
      (product as any).mainImageUrl ||
      (product as any).imageUrls?.[0] ||
      null;

    if (!imageUrl) {
      return null;
    }

    if (imageUrl.startsWith('http')) {
      return imageUrl;
    }

    return `${environment.apiUrl}${imageUrl}`;
  }
  private loadCategories(): void {
    const request = new ListProductCategoriesRequest();
    request.onlyEnabled = true;
    request.paging.pageSize = 100;

    this.categoriesApi.list(request).subscribe({
      next: (response) => {
        this.categories = response.items;
      },
      error: (err) => {
        console.error('Public category load error:', err);
      }
    });
  }
}
