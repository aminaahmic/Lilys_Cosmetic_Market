import { BasePagedQuery } from '../../core/models/paging/base-paged-query';

export interface SubcategoryDto {
  id: number;
  name: string;
  isEnabled: boolean;
  categoryId: number;
  categoryName: string;
  productCount: number;
}

export class ListSubcategoriesRequest extends BasePagedQuery {
  categoryId?: number | null = null;
  onlyEnabled?: boolean | null = null;
  search?: string | null = null;
}

export interface CreateSubcategoryRequest {
  name: string;
  categoryId: number;
  isEnabled: boolean;
}

export interface UpdateSubcategoryRequest {
  name: string;
  categoryId: number;
  isEnabled: boolean;
}