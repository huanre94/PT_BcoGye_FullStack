import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { Product, WishlistItem } from '../models/product.model';

@Injectable({
  providedIn: 'root'
})
export class WishlistService {
  private wishlistItems = new BehaviorSubject<WishlistItem[]>([]);
  public wishlistItems$ = this.wishlistItems.asObservable();

  constructor() {
    // Cargar wishlist desde localStorage
    const savedWishlist = localStorage.getItem('wishlistItems');
    if (savedWishlist) {
      const items = JSON.parse(savedWishlist);
      // Convertir las fechas de string a Date
      items.forEach((item: WishlistItem) => {
        item.addedDate = new Date(item.addedDate);
      });
      this.wishlistItems.next(items);
    }
  }

  addToWishlist(product: Product): void {
    const currentItems = this.wishlistItems.value;
    const existingItem = currentItems.find(item => item.product.id === product.id);

    if (!existingItem) {
      currentItems.push({ product, addedDate: new Date() });
      this.updateWishlist(currentItems);
    }
  }

  removeFromWishlist(productId: string): void {
    const currentItems = this.wishlistItems.value.filter(item => item.product.id !== productId);
    this.updateWishlist(currentItems);
  }

  isInWishlist(productId: string): boolean {
    return this.wishlistItems.value.some(item => item.product.id === productId);
  }

  clearWishlist(): void {
    this.updateWishlist([]);
  }

  getWishlistCount(): number {
    return this.wishlistItems.value.length;
  }

  private updateWishlist(items: WishlistItem[]): void {
    this.wishlistItems.next(items);
    localStorage.setItem('wishlistItems', JSON.stringify(items));
  }
}
