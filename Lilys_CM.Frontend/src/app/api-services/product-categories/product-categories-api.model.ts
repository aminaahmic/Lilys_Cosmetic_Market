import { BasePagedQuery } from '../../core/models/paging/base-paged-query';
import { PageResult } from '../../core/models/paging/page-result';

// === QUERIES (READ) ===

/**
 * Query parameters for GET /ProductCategories
 */
export class ListProductCategoriesRequest extends BasePagedQuery {
  search?: string | null;
  onlyEnabled?: boolean | null;
    sortBy?: string;
}

/**
 * Response item for GET /ProductCategories
 */
export interface ListProductCategoriesQueryDto {
  id: number;
  name: string;
  isEnabled: boolean;
  icon?: string;
}

/**
 * Response for GET /ProductCategories/{id}
 */
export interface GetProductCategoryByIdQueryDto {
  id: number;
  name: string;
  isEnabled: boolean;
  icon?: string;
}

/**
 * Paged response for GET /ProductCategories
 */
export type ListProductCategoriesResponse = PageResult<ListProductCategoriesQueryDto>;

export interface CreateProductCategoryCommand {
  name: string;
  icon?: string;
  isEnabled?: boolean;
}

export interface UpdateProductCategoryCommand {
  name: string;
  icon?: string;
  isEnabled?: boolean;
}
