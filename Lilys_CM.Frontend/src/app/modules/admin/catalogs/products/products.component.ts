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
import {
  ListProductCategoriesQueryDto,
  ListProductCategoriesRequest
} from '../../../../api-services/product-categories/product-categories-api.model';

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
  private router = inject(Router);
  private dialogHelper = inject(DialogHelperService);

  displayedColumns: string[] = [
    'name',
    'brand',
    'subcategory',
    'categoryName',
    'price',
    'stockQuantity',
    'isEnabled',
    'actions'
  ];

  categories: ListProductCategoriesQueryDto[] = [];
  brands: string[] = [];
  subcategories: string[] = [];
  private filterDebounceTimer: ReturnType<typeof setTimeout> | null = null;

  constructor() {
    super();
    this.request = new ListProductsRequest();
    this.request.paging.pageSize = 10;
    this.request.isEnabled = null;
  }

  ngOnInit(): void {
    this.initList();
    this.loadCategories();
    this.loadFilterOptions();
    this.loadStats();
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
    this.loadFilterOptions();
    this.onApplyFilters();
  }

  onSubcategoryChanged(): void {
    this.onApplyFilters();
  }

  onResetFilters(): void {
    this.request.search = null;
    this.request.brand = null;
    this.request.subcategory = null;
    this.request.categoryId = null;
    this.request.priceMin = null;
    this.request.priceMax = null;
    this.request.isEnabled = null;
    this.request.paging.page = 1;
    this.loadFilterOptions();
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
    this.api.getFilterOptions(this.request.categoryId).subscribe({
      next: (response) => {
        this.brands = response.brands;
        this.subcategories = response.subcategories;

        if (this.request.brand && !this.brands.includes(this.request.brand)) {
          this.request.brand = null;
        }

        if (this.request.subcategory && !this.subcategories.includes(this.request.subcategory)) {
          this.request.subcategory = null;
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

}
