import { Routes } from '@angular/router';
import { CategoryPageComponent } from './components/category-page/category-page.component';

export const routes: Routes = [
  { path: '', component: CategoryPageComponent },
  { path: '**', redirectTo: '' }
];
