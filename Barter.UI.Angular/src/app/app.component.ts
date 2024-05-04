import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Category } from './models/category.model';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent implements OnInit {
  categories: Category[] = [];

  constructor(private http: HttpClient) { }

  ngOnInit() {
    this.getCategories();
  }

  getCategories() {
    this.http.get<Category[]>('https://localhost:7027/categories').subscribe({
      next: (categories) => {
        this.categories = categories;
        console.log('Categories:', this.categories);
      },
      error: (error) => {
        console.error('Error fetching categories:', error);
      }
    });
  }
}
