import { Component, OnInit, ViewChild } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { IonicModule, IonContent, IonInfiniteScroll } from '@ionic/angular';
import { ProductService } from '../services/product.service';
import { CartService } from '../services/cart.service';
import { WishlistService } from '../services/wishlist.service';
import { AuthService } from '../services/auth.service';
import { Product } from '../models/product.model';

@Component({
  selector: 'app-home',
  templateUrl: './home.page.html',
  styleUrls: ['./home.page.scss'],
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    IonicModule
  ]
})
export class HomePage implements OnInit {
  @ViewChild(IonContent, { static: false }) content!: IonContent;
  @ViewChild(IonInfiniteScroll, { static: false }) infiniteScroll!: IonInfiniteScroll;

  products: Product[] = [];
  currentPage = 1;
  isLoading = false;
  hasMoreData = true;
  searchTerm = '';
  searchTimeout: any;

  constructor(
    private productService: ProductService,
    private cartService: CartService,
    private wishlistService: WishlistService,
    private authService: AuthService,
    private router: Router
  ) {}

  ngOnInit() {
    this.loadProducts();
  }

  loadProducts(event?: any) {
    if (this.isLoading) return;

    this.isLoading = true;
    
    this.productService.getProducts(this.currentPage, 10, this.searchTerm).subscribe({
      next: (newProducts) => {
        if (newProducts.length === 0) {
          this.hasMoreData = false;
        } else {
          this.products = [...this.products, ...newProducts];
          this.currentPage++;
        }
        
        this.isLoading = false;
        
        if (event) {
          event.target.complete();
          if (!this.hasMoreData) {
            event.target.disabled = true;
          }
        }
      },
      error: (error) => {
        console.error('Error loading products:', error);
        this.isLoading = false;
        if (event) {
          event.target.complete();
        }
      }
    });
  }

  addToCart(product: Product) {
    this.cartService.addToCart(product);
  }

  toggleWishlist(product: Product) {
    if (this.wishlistService.isInWishlist(product.id)) {
      this.wishlistService.removeFromWishlist(product.id);
    } else {
      this.wishlistService.addToWishlist(product);
    }
  }

  isInWishlist(productId: string): boolean {
    return this.wishlistService.isInWishlist(productId);
  }

  doRefresh(event: any) {
    this.products = [];
    this.currentPage = 1;
    this.hasMoreData = true;
    
    this.productService.getProducts(1, 10, this.searchTerm).subscribe({
      next: (products) => {
        this.products = products;
        this.currentPage = 2;
        event.target.complete();
      },
      error: (error) => {
        console.error('Error refreshing products:', error);
        event.target.complete();
      }
    });
  }

  onSearchChange() {
    // Clear the previous timeout
    if (this.searchTimeout) {
      clearTimeout(this.searchTimeout);
    }

    // Set a new timeout for debouncing
    this.searchTimeout = setTimeout(() => {
      this.searchProducts();
    }, 300);
  }

  searchProducts() {
    this.products = [];
    this.currentPage = 1;
    this.hasMoreData = true;
    
    if (this.infiniteScroll) {
      this.infiniteScroll.disabled = false;
    }
    
    this.loadProducts();
  }

  clearSearch() {
    this.searchTerm = '';
    this.searchProducts();
  }

  viewProductDetail(productId: string) {
    this.router.navigate(['/product-detail', productId]);
  }

  logout() {
    this.authService.logout();
    this.router.navigate(['/login']);
  }
}
