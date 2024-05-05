import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Category } from '../models/category.model';

@Component({
  selector: 'app-categories',
  templateUrl: './categories.component.html',
  styleUrls: ['./categories.component.css']
})
export class CategoriesComponent implements OnInit {
  categories: Category[] = [];
  @Output() categorySelected: EventEmitter<string> = new EventEmitter<string>(); // დაემატა კატეგორიის ნივთების ჩვენება დაქლიქებისას

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
    });
  }

  onCategoryClick(categoryId: string) {
    this.categorySelected.emit(categoryId); // დაემატა კატეგორიის ნივთების ჩვენება დაქლიქებისას
  }
}
