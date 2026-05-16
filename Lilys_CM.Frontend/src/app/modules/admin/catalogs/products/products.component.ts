import { Component, inject, OnDestroy, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import {
  ListProductsRequest,
  ListProductsQueryDto
} from '../../../../api-services/products/products-api.models';
import { ProductsApiService } from '../../../../api-services/products/products-api.service';
import { BaseListPagedComponent } from '../../../../core/components/base-classes/base-list-paged-component';
import { DialogHelperService } from '../../../shared/services/dialog-helper.service';
import { DialogButton } from '../../../shared/models/dialog-config.model';
import { ProductCategoriesApiService } from '../../../../api-services/product-categories/product-categories-api.service';
import { BrandsApiService } from '../../../../api-services/brands/brands-api.service';
import { BrandDto } from '../../../../api-services/brands/brands-api.models';
import { environment } from '../../../../../environments/environment';
import { debounceTime, distinctUntilChanged, Subject, switchMap, of } from 'rxjs';
import {
  ListProductCategoriesQueryDto,
  ListProductCategoriesRequest
} from '../../../../api-services/product-categories/product-categories-api.model';
import { Sort } from '@angular/material/sort';
@Component({
  selector: 'app-products',
  standalone: false,
  templateUrl: './products.component.html',
  styleUrl: './products.component.scss'
})
export class ProductsComponent
  extends BaseListPagedComponent<ListProductsQueryDto, ListProductsRequest>
  implements OnInit, OnDestroy {
  statsItems: ListProductsQueryDto[] = [];
  private api = inject(ProductsApiService);
  private categoriesApi = inject(ProductCategoriesApiService);
  private brandsApi = inject(BrandsApiService);
  private router = inject(Router);
  private dialogHelper = inject(DialogHelperService);

  displayedColumns: string[] = [
    'product',
    'catalog',
    'brand',
    'price',
    'stock',
    'status',
    'actions'
  ];

  categories: ListProductCategoriesQueryDto[] = [];
  brands: BrandDto[] = [];
  subcategories: { id: number; name: string }[] = [];
  searchSuggestions: string[] = [];
  private searchSuggestionInput$ = new Subject<string>();
  private filterDebounceTimer: ReturnType<typeof setTimeout> | null = null;

  constructor() {
    super();
    this.request = new ListProductsRequest();
    this.request.paging.pageSize = 10;
    this.request.isEnabled = null;
    this.request.sortBy = 'name';
    this.request.sortDirection = 'asc';
  }

  ngOnInit(): void {
    this.initList();
    this.searchSuggestionInput$
      .pipe(
        debounceTime(250),
        distinctUntilChanged(),
        switchMap(search => {
          const value = search?.trim();

          if (!value || value.length < 2) {
            return of([]);
          }

          return this.api.getSearchSuggestions(value);
        })
      )
      .subscribe({
        next: suggestions => {
          this.searchSuggestions = suggestions;
        },
        error: err => {
          this.searchSuggestions = [];
          console.error('Search suggestions error:', err);
        }
      });
    this.loadCategories();
    this.loadFilterOptions();
    this.loadBrands();
    this.loadStats();
  }
  onSearchInputChanged(value: string): void {
    this.request.search = value;
    this.searchSuggestionInput$.next(value);
  }

  onSuggestionSelected(value: string): void {
    this.request.search = value;
    this.searchSuggestions = [];
    this.request.paging.page = 1;
    this.loadPagedData();
  }
  ngOnDestroy(): void {
    if (this.filterDebounceTimer) {
      clearTimeout(this.filterDebounceTimer);
      this.filterDebounceTimer = null;
    }
  }

  protected loadPagedData(): void {
    this.startLoading();

    this.api.list(this.request).subscribe({
      next: (response) => {
        this.handlePageResult(response);
        this.stopLoading();
      },
      error: (err) => {
        this.stopLoading('Failed to load products');
        console.error('Load products error:', err);
      }
    });
  }
  private loadBrands(): void {
    this.brandsApi.getAll(true, null).subscribe({
      next: (response) => {
        this.brands = response;

        if (
          this.request.brand &&
          !this.brands.some(brand => brand.name === this.request.brand)
        ) {
          this.request.brand = null;
        }
      },
      error: (err) => {
        console.error('Load brands error:', err);
      }
    });
  }

  onCreate(): void {
    this.router.navigate(['/admin/products/add']);
  }

  onEdit(product: ListProductsQueryDto): void {
    this.router.navigate(['/admin/products', product.id, 'edit']);
  }

  onDelete(product: ListProductsQueryDto): void {
    this.dialogHelper.product.confirmDelete(product.name).subscribe(result => {
      if (result && result.button === DialogButton.DELETE) {
        this.performDelete(product);
      }
    });
  }
  get activeProductsCount(): number {
    return this.statsItems.filter(x => x.isEnabled).length;
  }

  get lowStockCount(): number {
    return this.statsItems.filter(x => x.stockQuantity > 0 && x.stockQuantity < 5).length;
  }

  get outOfStockCount(): number {
    return this.statsItems.filter(x => x.stockQuantity === 0).length;
  }

  get totalProductsCount(): number {
    return this.statsItems.length;
  }
  private loadStats(): void {
    const statsRequest = new ListProductsRequest();

    statsRequest.paging.page = 1;
    statsRequest.paging.pageSize = 1000;

    statsRequest.search = null;
    statsRequest.brand = null;
    statsRequest.brandId = null;
    statsRequest.subcategory = null;
    statsRequest.categoryId = null;
    statsRequest.priceMin = null;
    statsRequest.priceMax = null;
    statsRequest.isEnabled = null;

    this.api.list(statsRequest).subscribe({
      next: (response) => {
        this.statsItems = response.items;
      },
      error: (err) => {
        console.error('Load product stats error:', err);
      }
    });
  }
  onApplyFilters(): void {
    this.request.paging.page = 1;
    this.loadPagedData();
  }
  get currentSortActive(): string {
    return this.request.sortBy || 'name';
  }

  get currentSortDirection(): 'asc' | 'desc' {
    return this.request.sortDirection === 'desc' ? 'desc' : 'asc';
  }

  onSortChanged(sort: Sort): void {
    if (!sort.active) {
      return;
    }

    this.request.sortBy = sort.active;
    this.request.sortDirection = sort.direction === 'desc' ? 'desc' : 'asc';
    this.request.paging.page = 1;

    this.loadPagedData();
  }
  onSearchChanged(): void {
    this.triggerDebouncedRefresh();
  }

  onPriceRangeChanged(): void {
    this.triggerDebouncedRefresh();
  }

  onStatusChanged(): void {
    this.onApplyFilters();
  }

  onBrandChanged(): void {
    this.onApplyFilters();
  }

  onCategoryChanged(): void {
    this.request.subcategory = null;
    this.request.subcategoryId = null;
    this.loadFilterOptions();
    this.onApplyFilters();
  }

  onSubcategoryChanged(): void {
    this.onApplyFilters();
  }

  onResetFilters(): void {
    this.request.sortBy = 'name';
    this.request.sortDirection = 'asc';
    this.request.search = null;
    this.request.brand = null;
    this.request.brandId = null;
    this.request.subcategory = null;
    this.request.subcategoryId = null;
    this.request.categoryId = null;
    this.request.priceMin = null;
    this.request.priceMax = null;
    this.request.isEnabled = null;
    this.request.paging.page = 1;
    this.loadFilterOptions();
    this.loadBrands();
    this.loadPagedData();
    this.loadStats();

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
        console.error('Load categories error:', err);
      }
    });
  }

  private loadFilterOptions(): void {
    if (!this.request.categoryId) {
      this.subcategories = [];
      this.request.subcategoryId = null;
      return;
    }

    this.api.getFilterOptions(this.request.categoryId).subscribe({
      next: (response) => {
        this.subcategories = response.subcategories;

        if (
          this.request.subcategoryId &&
          !this.subcategories.some(x => x.id === this.request.subcategoryId)
        ) {
          this.request.subcategoryId = null;
        }
      },
      error: (err) => {
        console.error('Load product filter options error:', err);
      }
    });
  }

  private performDelete(product: ListProductsQueryDto): void {
    this.startLoading();


    this.api.delete(product.id).subscribe({
      next: () => {
        this.dialogHelper.product.showDeleteSuccess().subscribe();
        this.loadPagedData();
      },
      error: (err) => {
        this.stopLoading();

        this.dialogHelper.showError(
          'DIALOGS.TITLES.ERROR',
          'PRODUCTS.DIALOGS.ERROR_DELETE'
        ).subscribe();

        console.error('Delete product error:', err);
      }

    });
    this.loadStats();
  }

  private triggerDebouncedRefresh(): void {
    if (this.filterDebounceTimer) {
      clearTimeout(this.filterDebounceTimer);
    }

    this.filterDebounceTimer = setTimeout(() => {
      this.onApplyFilters();
    }, 300);
  }
  getProductSku(product: ListProductsQueryDto): string {
    return (product as any).sku || 'SKU nije unesen';
  }

  getProductImage(product: ListProductsQueryDto): string | null {
    const imageUrl =
      (product as any).imageUrl ||
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


  getCompareAtPrice(product: ListProductsQueryDto): number | null {
    return (product as any).compareAtPrice ?? null;
  }

  getCategoryLabel(product: ListProductsQueryDto): string {
    return product.categoryName || 'Bez kategorije';
  }

  getSubcategoryLabel(product: ListProductsQueryDto): string {
    return product.subcategoryName || product.subcategory || 'Bez potkategorije';
  }
  getStockLabel(product: ListProductsQueryDto): string {
    if (product.stockQuantity <= 0) {
      return 'Nema zalihe';
    }

    if (product.stockQuantity <= 10) {
      return 'Niska zaliha';
    }

    return 'Na stanju';
  }

  getStockClass(product: ListProductsQueryDto): string {
    if (product.stockQuantity <= 0) {
      return 'out';
    }

    if (product.stockQuantity <= 10) {
      return 'low';
    }

    return 'ok';
  }

}
