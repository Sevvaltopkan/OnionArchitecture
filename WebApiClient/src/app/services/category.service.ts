import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Category, CreateCategoryRequest, UpdateCategoryRequest } from '../models/category.model';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {
  private apiUrl = 'http://localhost:5245/api/Category';

  constructor(private http: HttpClient) { }

  // Tüm kategorileri getir
  getAll(): Observable<Category[]> {
    return this.http.get<Category[]>(this.apiUrl);
  }

  // ID'ye göre kategori getir
  getById(id: number): Observable<Category> {
    return this.http.get<Category>(`${this.apiUrl}/${id}`);
  }

  // Yeni kategori oluştur
  create(category: CreateCategoryRequest): Observable<string> {
    return this.http.post(this.apiUrl, category, { responseType: 'text' });
  }

  // Kategori güncelle
  update(category: UpdateCategoryRequest): Observable<string> {
    return this.http.put(this.apiUrl, category, { responseType: 'text' });
  }

  // Kategori sil
  delete(id: number): Observable<string> {
    return this.http.delete(`${this.apiUrl}/${id}`, { responseType: 'text' });
  }
}

