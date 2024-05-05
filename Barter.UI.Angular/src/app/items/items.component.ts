import { Component, Input, OnChanges, SimpleChanges } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Item } from '../models/item.model';

@Component({
  selector: 'app-items',
  templateUrl: './items.component.html',
  styleUrls: ['./items.component.css']
})
export class ItemsComponent implements OnChanges {
  @Input() selectedCategoryId: string | null = null;
  items: Item[] = [];

  constructor(private http: HttpClient) { }

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['selectedCategoryId'] && !changes['selectedCategoryId'].firstChange) { // დაემატა კატეგორიის ნივთების ჩვენება დაქლიქებისას
      const newCategoryId = changes['selectedCategoryId'].currentValue;
      this.fetchItemsByCategoryId(newCategoryId);
    }
  }

  fetchItemsByCategoryId(categoryId: string) {
    this.http.get<Item[]>(`https://localhost:7027/items/${categoryId}/items`).subscribe({
      next: (items) => {
        this.items = items;
        console.log('Items for category ', categoryId, ': ', this.items);
      },
      error: (error) => {
        console.error('Error fetching items:', error);
      }
    });
  }
}
