import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  selectedCategoryId: string | null = null; // დაემატა კატეგორიის ნივთების ჩვენება დაქლიქებისას

  onCategorySelected(categoryId: string) {
    this.selectedCategoryId = categoryId;
  }
}
