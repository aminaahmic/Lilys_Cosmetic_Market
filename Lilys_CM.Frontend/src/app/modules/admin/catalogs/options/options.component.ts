import { Component, inject, OnInit } from '@angular/core';
import { OptionsApiService, OptionDto } from '../../../../api-services/options/options-api.service';
import { ToasterService } from '../../../../core/services/toaster.service';

@Component({
  selector: 'app-options',
  standalone: false,
  templateUrl: './options.component.html',
  styleUrl: './options.component.scss'
})
export class OptionsComponent implements OnInit {
  private api = inject(OptionsApiService);
  private toaster = inject(ToasterService);

  options: OptionDto[] = [];
  filteredOptions: OptionDto[] = [];

  search = '';
  isLoading = false;
  isSaving = false;

  showForm = false;
  editingOptionId: number | null = null;

  form = {
    name: ''
  };

  ngOnInit(): void {
    this.loadOptions();
  }

  loadOptions(): void {
    this.isLoading = true;

    this.api.getAll().subscribe({
      next: (response) => {
        this.options = response;
        this.applyFilters();
        this.isLoading = false;
      },
      error: (err) => {
        this.isLoading = false;
        this.toaster.error('Greška prilikom učitavanja opcija.');
        console.error(err);
      }
    });
  }

  applyFilters(): void {
    const term = this.search.trim().toLowerCase();

    this.filteredOptions = this.options.filter(option =>
      !term || option.name.toLowerCase().includes(term)
    );
  }

  startCreate(): void {
    this.showForm = true;
    this.editingOptionId = null;
    this.form = { name: '' };
  }

  startEdit(option: OptionDto): void {
    this.showForm = true;
    this.editingOptionId = option.id;
    this.form = { name: option.name };
  }

  cancelForm(): void {
    this.showForm = false;
    this.editingOptionId = null;
    this.form = { name: '' };
  }

  saveOption(): void {
    const name = this.form.name.trim();

    if (!name) {
      this.toaster.warning('Naziv opcije je obavezan.');
      return;
    }

    this.isSaving = true;

    if (this.editingOptionId === null) {
      this.api.create({ name }).subscribe({
        next: () => {
          this.toaster.success('Opcija je uspješno kreirana.');
          this.isSaving = false;
          this.cancelForm();
          this.loadOptions();
        },
        error: (err) => {
          this.isSaving = false;
          this.toaster.error(err?.error || 'Greška prilikom kreiranja opcije.');
          console.error(err);
        }
      });

      return;
    }

    this.api.update(this.editingOptionId, { name }).subscribe({
      next: () => {
        this.toaster.success('Opcija je uspješno ažurirana.');
        this.isSaving = false;
        this.cancelForm();
        this.loadOptions();
      },
      error: (err) => {
        this.isSaving = false;
        this.toaster.error(err?.error || 'Greška prilikom ažuriranja opcije.');
        console.error(err);
      }
    });
  }

  deleteOption(option: OptionDto): void {
    if (option.usageCount > 0) {
      this.toaster.warning('Ne možeš obrisati opciju koja se koristi.');
      return;
    }

    const confirmed = confirm(`Obrisati opciju "${option.name}"?`);

    if (!confirmed) {
      return;
    }

    this.api.delete(option.id).subscribe({
      next: () => {
        this.toaster.success('Opcija je obrisana.');
        this.loadOptions();
      },
      error: (err) => {
        this.toaster.error(err?.error || 'Greška prilikom brisanja opcije.');
        console.error(err);
      }
    });
  }

  get totalUsageCount(): number {
    return this.options.reduce((sum, option) => sum + option.usageCount, 0);
  }
}