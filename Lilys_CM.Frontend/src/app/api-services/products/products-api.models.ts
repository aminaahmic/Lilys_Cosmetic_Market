import { PageResult } from '../../core/models/paging/page-result';
import { BasePagedQuery } from '../../core/models/paging/base-paged-query';

// === QUERIES (READ) ===

/**
 * Query parameters for GET /Products
 * Corresponds to: ListProductsQuery.cs
 */
export class ListProductsRequest extends BasePagedQuery {
  search?: string | null;
  brand?: string | null;
  brandId?: number | null;
  brandName?: string | null;
  brandLogoUrl?: string | null;
  subcategory?: string | null;
  categoryId?: number | null;
  priceMin?: number | null;
  priceMax?: number | null;
  isEnabled?: boolean | null;
}

/**
 * Response item for GET /Products
 * Corresponds to: ListProductsQueryDto.cs
 */
export interface ListProductsQueryDto {
  id: number;
  name: string;
  description?: string | null;
  brand?: string | null;
  brandId?: number | null;
  brandName?: string | null;
  brandLogoUrl?: string | null;
  subcategory?: string | null;
  price: number;
  stockQuantity: number;
  categoryId: number;
  categoryName: string;
  isEnabled: boolean;
}

/**
 * Response for GET /Products/{id}
 * Corresponds to: GetProductByIdQueryDto.cs
 */
export interface GetProductByIdQueryDto {
  id: number;

  name: string;
  sku?: string | null;
  slug?: string | null;

  imageUrl?: string | null;

  shortDescription?: string | null;
  description?: string | null;
  ingredients?: string | null;
  howToUse?: string | null;
  benefits?: string | null;

  brand?: string | null;
  brandId?: number | null;
  brandName?: string | null;
  brandLogoUrl?: string | null;
  size?: string | null;
  countryOfOrigin?: string | null;
  barcode?: string | null;

  subcategory?: string | null;
  subcategoryId?: number | null;

  price: number;
  compareAtPrice?: number | null;

  stockQuantity: number;

  categoryName: string;
  categoryId: number;

  isEnabled: boolean;
  isFeatured?: boolean;
  seoTitle?: string | null;
  seoDescription?: string | null;
}

/**
 * Paged response for GET /Products
 */
export type ListProductsResponse = PageResult<ListProductsQueryDto>;

export class ListProductStockMovementsRequest extends BasePagedQuery { }

export interface ProductFilterOptionsDto {
  brands: string[];
  subcategories: string[];
}

// === COMMANDS (WRITE) ===

/**
 * Command for POST /Products
 * Corresponds to: CreateProductCommand.cs
 */
export interface CreateProductCommand {
  name: string;
  sku: string;
  slug?: string | null;

  imageUrl?: string | null;
  shortDescription?: string | null;
  description?: string | null;
  ingredients?: string | null;
  howToUse?: string | null;
  benefits?: string | null;


  brandId?: number | null;
  size?: string | null;
  countryOfOrigin?: string | null;
  barcode?: string | null;

  price: number;
  compareAtPrice?: number | null;

  stockQuantity: number;

  isEnabled: boolean;
  isFeatured: boolean;

  seoTitle?: string | null;
  seoDescription?: string | null;

  categoryId: number;
  subcategoryId?: number | null;
}

/**
 * Command for PUT /Products/{id}
 * Corresponds to: UpdateProductCommand.cs
 */
export interface UpdateProductCommand {
  name: string;
  sku: string;
  slug?: string | null;

  imageUrl?: string | null;

  shortDescription?: string | null;
  description?: string | null;
  ingredients?: string | null;
  howToUse?: string | null;
  benefits?: string | null;

  brand?: string | null;
  brandId?: number | null;
  size?: string | null;
  countryOfOrigin?: string | null;
  barcode?: string | null;

  price: number;
  compareAtPrice?: number | null;

  stockQuantity: number;

  isEnabled: boolean;
  isFeatured: boolean;

  seoTitle?: string | null;
  seoDescription?: string | null;

  categoryId: number;
  subcategoryId?: number | null;
}

export interface AdjustProductStockCommand {
  quantityDelta: number;
  reason: string;
  note?: string | null;
}

export interface ProductStockMovementDto {
  id: number;
  quantityDelta: number;
  previousQuantity: number;
  newQuantity: number;
  reason: string;
  note?: string | null;
  createdAtUtc: string;
}
export interface CreateProductVariantCommand {
  price: number;
  stock: number;
  options: CreateProductVariantOptionCommand[];
}

export interface CreateProductVariantOptionCommand {
  optionName: string;
  value: string;
}
export interface UpdateProductVariantCommand {
  price: number;
  stock: number;
  options: UpdateProductVariantOptionCommand[];
}

export interface UpdateProductVariantOptionCommand {
  optionName: string;
  value: string;
}
export interface ProductVariantDto {
  id: number;
  productId: number;
  price: number;
  stock: number;
  options: ProductVariantOptionDto[];
}

export interface ProductVariantOptionDto {
  optionId: number;
  optionName: string;
  optionValueId: number;
  value: string;
}
export type ListProductStockMovementsResponse = PageResult<ProductStockMovementDto>;
