import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { CategoryService } from '../../services/category.service';
import { Category, CreateCategoryRequest } from '../../models/category.model';

@Component({
  selector: 'app-category-page',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './category-page.component.html',
  styleUrl: './category-page.component.css'
})
export class CategoryPageComponent implements OnInit {
  categories: Category[] = [];
  loading = true;
  
  // Form
  newCategory: CreateCategoryRequest = {
    categoryName: '',
    description: ''
  };
  saving = false;
  message = '';
  messageType = '';

  // Edit mode
  editingId: number | null = null;
  editCategory: CreateCategoryRequest = {
    categoryName: '',
    description: ''
  };

  constructor(private categoryService: CategoryService) {}

  ngOnInit(): void {
    this.loadCategories();
  }

  loadCategories(): void {
    this.loading = true;
    this.categoryService.getAll().subscribe({
      next: (data) => {
        this.categories = data;
        this.loading = false;
      },
      error: (err) => {
        this.showMessage('Kategoriler yüklenirken hata oluştu!', 'error');
        this.loading = false;
      }
    });
  }

  addCategory(): void {
    if (!this.newCategory.categoryName.trim()) {
      this.showMessage('Kategori adı zorunludur!', 'error');
      return;
    }

    this.saving = true;
    this.categoryService.create(this.newCategory).subscribe({
      next: () => {
        this.showMessage('Kategori başarıyla eklendi!', 'success');
        this.newCategory = { categoryName: '', description: '' };
        this.loadCategories();
        this.saving = false;
      },
      error: (err) => {
        this.showMessage('Kategori eklenirken hata oluştu!', 'error');
        this.saving = false;
      }
    });
  }

  startEdit(category: Category): void {
    this.editingId = category.id;
    this.editCategory = {
      categoryName: category.categoryName,
      description: category.description
    };
  }

  cancelEdit(): void {
    this.editingId = null;
  }

  saveEdit(id: number): void {
    if (!this.editCategory.categoryName.trim()) {
      this.showMessage('Kategori adı zorunludur!', 'error');
      return;
    }

    this.categoryService.update({ id, ...this.editCategory }).subscribe({
      next: () => {
        this.showMessage('Kategori güncellendi!', 'success');
        this.editingId = null;
        this.loadCategories();
      },
      error: () => {
        this.showMessage('Güncelleme hatası!', 'error');
      }
    });
  }

  deleteCategory(id: number, name: string): void {
    if (confirm(`"${name}" kategorisini silmek istiyor musunuz?`)) {
      this.categoryService.delete(id).subscribe({
        next: () => {
          this.showMessage('Kategori silindi!', 'success');
          this.loadCategories();
        },
        error: () => {
          this.showMessage('Silme hatası!', 'error');
        }
      });
    }
  }

  showMessage(msg: string, type: string): void {
    this.message = msg;
    this.messageType = type;
    setTimeout(() => {
      this.message = '';
    }, 3000);
  }
}

