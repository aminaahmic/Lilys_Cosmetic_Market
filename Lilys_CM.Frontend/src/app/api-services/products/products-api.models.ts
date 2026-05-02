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
  description?: string | null;
  brand?: string | null;
  subcategory?: string | null;
  price: number;
  stockQuantity: number;
  categoryName: string;
  categoryId: number;
  isEnabled: boolean;
}

/**
 * Paged response for GET /Products
 */
export type ListProductsResponse = PageResult<ListProductsQueryDto>;

export class ListProductStockMovementsRequest extends BasePagedQuery {}

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
  description?: string | null;
  brand?: string | null;
  subcategory?: string | null;
  price: number;
  isEnabled: boolean;
  categoryId: number;
}

/**
 * Command for PUT /Products/{id}
 * Corresponds to: UpdateProductCommand.cs
 */
export interface UpdateProductCommand {
  name: string;
  description?: string | null;
  brand?: string | null;
  subcategory?: string | null;
  price: number;
  isEnabled: boolean;
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

export type ListProductStockMovementsResponse = PageResult<ProductStockMovementDto>;
