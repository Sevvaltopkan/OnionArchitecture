import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { CategoryService } from '../../services/category.service';
import { CreateCategoryRequest } from '../../models/category.model';

@Component({
  selector: 'app-category-create',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterLink],
  templateUrl: './category-create.component.html',
  styleUrl: './category-create.component.css'
})
export class CategoryCreateComponent {
  category: CreateCategoryRequest = {
    categoryName: '',
    description: ''
  };
  
  loading = false;
  error = '';
  success = '';

  constructor(
    private categoryService: CategoryService,
    private router: Router
  ) {}

  onSubmit(): void {
    if (!this.category.categoryName.trim()) {
      this.error = 'Kategori adı zorunludur!';
      return;
    }

    this.loading = true;
    this.error = '';
    this.success = '';

    this.categoryService.create(this.category).subscribe({
      next: (response) => {
        this.success = 'Kategori başarıyla oluşturuldu!';
        this.loading = false;
        
        setTimeout(() => {
          this.router.navigate(['/categories']);
        }, 1500);
      },
      error: (err) => {
        this.error = 'Kategori oluşturulurken bir hata oluştu.';
        this.loading = false;
        console.error('Error creating category:', err);
      }
    });
  }
}

