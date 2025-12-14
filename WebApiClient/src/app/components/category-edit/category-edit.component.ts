import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router, RouterLink, ActivatedRoute } from '@angular/router';
import { CategoryService } from '../../services/category.service';
import { UpdateCategoryRequest } from '../../models/category.model';

@Component({
  selector: 'app-category-edit',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterLink],
  templateUrl: './category-edit.component.html',
  styleUrl: './category-edit.component.css'
})
export class CategoryEditComponent implements OnInit {
  category: UpdateCategoryRequest = {
    id: 0,
    categoryName: '',
    description: ''
  };
  
  loading = false;
  loadingData = true;
  error = '';
  success = '';

  constructor(
    private categoryService: CategoryService,
    private router: Router,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.loadCategory(+id);
    } else {
      this.error = 'Geçersiz kategori ID!';
      this.loadingData = false;
    }
  }

  loadCategory(id: number): void {
    this.categoryService.getById(id).subscribe({
      next: (data) => {
        this.category = {
          id: data.id,
          categoryName: data.categoryName,
          description: data.description
        };
        this.loadingData = false;
      },
      error: (err) => {
        this.error = 'Kategori bulunamadı veya bir hata oluştu.';
        this.loadingData = false;
        console.error('Error loading category:', err);
      }
    });
  }

  onSubmit(): void {
    if (!this.category.categoryName.trim()) {
      this.error = 'Kategori adı zorunludur!';
      return;
    }

    this.loading = true;
    this.error = '';
    this.success = '';

    this.categoryService.update(this.category).subscribe({
      next: (response) => {
        this.success = 'Kategori başarıyla güncellendi!';
        this.loading = false;
        
        setTimeout(() => {
          this.router.navigate(['/categories']);
        }, 1500);
      },
      error: (err) => {
        this.error = 'Kategori güncellenirken bir hata oluştu.';
        this.loading = false;
        console.error('Error updating category:', err);
      }
    });
  }
}

