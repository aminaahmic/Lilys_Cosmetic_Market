import { Component, inject, OnInit } from '@angular/core';
import {
  BrandDto,
  CreateBrandCommand,
  UpdateBrandCommand
} from '../../../../api-services/brands/brands-api.models';
import { BrandsApiService } from '../../../../api-services/brands/brands-api.service';
import { ToasterService } from '../../../../core/services/toaster.service';
import { environment } from '../../../../../environments/environment';
import { MatDialog } from '@angular/material/dialog';
import { BrandDeleteDialogComponent } from './brand-delete-dialog/brand-delete-dialog.component';


@Component({
  selector: 'app-brands',
  standalone: false,
  templateUrl: './brands.component.html',
  styleUrl: './brands.component.scss'
})
export class BrandsComponent implements OnInit {
  private api = inject(BrandsApiService);
  private toaster = inject(ToasterService);
  private dialog = inject(MatDialog);

  brands: BrandDto[] = [];
  filteredBrands: BrandDto[] = [];

  search = '';
  statusFilter: boolean | null = null;

  isLoading = false;
  isSaving = false;
  showBrandForm = false;
  selectedLogoFile: File | null = null;
  logoPreviewUrl: string | null = null;

  editingBrandId: number | null = null;

  form = {
    name: '',
    slug: '',
    description: '',
    logoUrl: '',
    isEnabled: true
  };

  ngOnInit(): void {
    this.loadBrands();
  }

  loadBrands(): void {
    this.isLoading = true;

    this.api.getAll(null, null).subscribe({
      next: (response) => {
        this.brands = response;
        this.applyLocalFilters();
        this.isLoading = false;
      },
      error: (err) => {
        this.isLoading = false;
        this.toaster.error('Greška prilikom učitavanja brendova.');
        console.error('Load brands error:', err);
      }
    });
  }

  applyLocalFilters(): void {
    const searchTerm = this.search.trim().toLowerCase();

    this.filteredBrands = this.brands.filter(brand => {
      const matchesSearch =
        !searchTerm ||
        brand.name.toLowerCase().includes(searchTerm) ||
        (brand.description ?? '').toLowerCase().includes(searchTerm);

      const matchesStatus =
        this.statusFilter === null ||
        brand.isEnabled === this.statusFilter;

      return matchesSearch && matchesStatus;
    });
  }

  onSearchChanged(): void {
    this.applyLocalFilters();
  }

  onStatusChanged(): void {
    this.applyLocalFilters();
  }

  resetFilters(): void {
    this.search = '';
    this.statusFilter = null;
    this.applyLocalFilters();
  }

  startCreate(): void {
    this.showBrandForm = true;
    this.editingBrandId = null;
    this.form = {
      name: '',
      slug: '',
      description: '',
      logoUrl: '',
      isEnabled: true
    };
    this.selectedLogoFile = null;
    this.logoPreviewUrl = null;
  }

  startEdit(brand: BrandDto): void {
    this.showBrandForm = true;
    this.editingBrandId = brand.id;

    this.form = {
      name: brand.name,
      slug: brand.slug ?? '',
      description: brand.description ?? '',
      logoUrl: brand.logoUrl ?? '',
      isEnabled: brand.isEnabled
    };
    this.selectedLogoFile = null;
    this.logoPreviewUrl = brand.logoUrl
      ? `${environment.apiUrl}${brand.logoUrl}`
      : null;
  }

  cancelEdit(): void {
    this.editingBrandId = null;

    this.form = {
      name: '',
      slug: '',
      description: '',
      logoUrl: '',
      isEnabled: true
    };

    this.selectedLogoFile = null;
    this.logoPreviewUrl = null;
    this.showBrandForm = false;
  }
  onLogoSelected(event: Event): void {
    const input = event.target as HTMLInputElement;
    const file = input.files?.[0] ?? null;

    if (!file) {
      this.selectedLogoFile = null;
      this.logoPreviewUrl = this.form.logoUrl || null;
      return;
    }

    const allowedTypes = ['image/jpeg', 'image/png', 'image/webp'];

    if (!allowedTypes.includes(file.type)) {
      this.toaster.warning('Dozvoljeni formati su JPG, PNG i WEBP.');
      input.value = '';
      return;
    }

    this.selectedLogoFile = file;
    this.logoPreviewUrl = URL.createObjectURL(file);
  }
  getBrandLogoUrl(brand: BrandDto): string | null {
    if (!brand.logoUrl) {
      return null;
    }

    if (brand.logoUrl.startsWith('http')) {
      return brand.logoUrl;
    }

    return `${environment.apiUrl}${brand.logoUrl}`;
  }
  saveBrand(): void {
    const name = this.form.name.trim();

    if (!name) {
      this.toaster.warning('Naziv brenda je obavezan.');
      return;
    }

    if (name.length > 160) {
      this.toaster.warning('Naziv brenda ne smije biti duži od 160 karaktera.');
      return;
    }

    this.isSaving = true;

    if (this.editingBrandId === null) {
      const command: CreateBrandCommand = {
        name,
        slug: this.form.slug.trim() || null,
        description: this.form.description.trim() || null,
        logoUrl: this.form.logoUrl.trim() || null,
        isEnabled: this.form.isEnabled
      };

      this.api.create(command).subscribe({
        next: (brandId) => {
          this.uploadLogoIfSelected(brandId, 'Brend je uspješno kreiran.');
        },
        error: (err) => {
          this.isSaving = false;
          this.toaster.error('Greška prilikom kreiranja brenda.');
          console.error('Create brand error:', err);
        }
      });

      return;
    }

    const command: UpdateBrandCommand = {
      name,
      slug: this.form.slug.trim() || null,
      description: this.form.description.trim() || null,
      logoUrl: this.form.logoUrl.trim() || null,
      isEnabled: this.form.isEnabled
    };

    this.api.update(this.editingBrandId, command).subscribe({
      next: () => {
        this.uploadLogoIfSelected(this.editingBrandId!, 'Brend je uspješno ažuriran.');
      },
      error: (err) => {
        this.isSaving = false;
        this.toaster.error('Greška prilikom ažuriranja brenda.');
        console.error('Update brand error:', err);
      }
    });
  }
  private uploadLogoIfSelected(brandId: number, successMessage: string): void {
    if (!this.selectedLogoFile) {
      this.toaster.success(successMessage);
      this.isSaving = false;
      this.startCreate();
      this.showBrandForm = false;
      this.loadBrands();
      return;
    }

    this.api.uploadLogo(brandId, this.selectedLogoFile).subscribe({
      next: () => {
        this.toaster.success(successMessage);
        this.isSaving = false;
        this.startCreate();
        this.loadBrands();
      },
      error: (err) => {
        this.isSaving = false;
        this.toaster.warning('Brend je sačuvan, ali logo nije uploadovan.');
        console.error('Upload brand logo error:', err);
        this.startCreate();
        this.loadBrands();
      }
    });
  }
  deleteBrand(brand: BrandDto): void {
    if (brand.productsCount > 0) {
      this.toaster.warning('Brend se ne može obrisati jer ima povezane proizvode.');
      return;
    }

    const dialogRef = this.dialog.open(BrandDeleteDialogComponent, {
      width: '440px',
      data: {
        brandName: brand.name
      }
    });

    dialogRef.afterClosed().subscribe(confirmed => {
      if (!confirmed) {
        return;
      }

      this.api.delete(brand.id).subscribe({
        next: () => {
          this.toaster.success('Brend je obrisan.');
          this.loadBrands();
        },
        error: (err) => {
          this.toaster.error('Greška prilikom brisanja brenda.');
          console.error('Delete brand error:', err);
        }
      });
    });
  }

  get activeBrandsCount(): number {
    return this.brands.filter(x => x.isEnabled).length;
  }

  get disabledBrandsCount(): number {
    return this.brands.filter(x => !x.isEnabled).length;
  }

  get totalProductsAttached(): number {
    return this.brands.reduce((sum, brand) => sum + brand.productsCount, 0);
  }
}