import { BasePagedQuery } from '../../core/models/paging/base-paged-query';
import { PageResult } from '../../core/models/paging/page-result';

// === QUERIES (READ) ===

/**
 * Query parameters for GET /ProductCategories
 */
export class ListProductCategoriesRequest extends BasePagedQuery {
  search?: string | null;
  onlyEnabled?: boolean | null;
}

/**
 * Response item for GET /ProductCategories
 */
export interface ListProductCategoriesQueryDto {
  id: number;
  name: string;
  isEnabled: boolean;
}

/**
 * Response for GET /ProductCategories/{id}
 */
export interface GetProductCategoryByIdQueryDto {
  id: number;
  name: string;
  isEnabled: boolean;
}

/**
 * Paged response for GET /ProductCategories
 */
export type ListProductCategoriesResponse = PageResult<ListProductCategoriesQueryDto>;

/**
 * Command for POST /ProductCategories
 */
export interface CreateProductCategoryCommand {
  name: string;
}

/**
 * Command for PUT /ProductCategories/{id}
 */
export interface UpdateProductCategoryCommand {
  name: string;
}
