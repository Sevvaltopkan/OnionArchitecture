export interface Category {
  id: number;
  categoryName: string;
  description: string;
}

export interface CreateCategoryRequest {
  categoryName: string;
  description: string;
}

export interface UpdateCategoryRequest {
  id: number;
  categoryName: string;
  description: string;
}

