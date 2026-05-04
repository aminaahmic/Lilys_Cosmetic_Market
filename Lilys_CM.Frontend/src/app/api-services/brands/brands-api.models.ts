export interface BrandDto {
  id: number;
  name: string;
  slug?: string | null;
  description?: string | null;
  logoUrl?: string | null;
  isEnabled: boolean;
  productsCount: number;
}

export interface CreateBrandCommand {
  name: string;
  slug?: string | null;
  description?: string | null;
  logoUrl?: string | null;
  isEnabled: boolean;
}

export interface UpdateBrandCommand {
  name: string;
  slug?: string | null;
  description?: string | null;
  logoUrl?: string | null;
  isEnabled: boolean;
}