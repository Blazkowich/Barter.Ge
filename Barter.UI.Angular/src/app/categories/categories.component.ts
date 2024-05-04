import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Category } from '../models/category.model';

@Component({
  selector: 'app-categories',
  templateUrl: './categories.component.html',
  styleUrl: './categories.component.css'
})
export class CategoriesComponent implements OnInit {
  categories: Category[] = [];

  constructor(private http: HttpClient) { }

  ngOnInit() {
    this.getCategories();
  }

  getCategories() {
    this.http.get<Category[]>('https://localhost:7027/categories').subscribe({
      next: (categories) => {
        this.categories = categories;
        console.log('Categories: ', this.categories);
      },
      error: (error) => {
        console.error('Error fetching categories:', error);
      }
    })
  }
}
