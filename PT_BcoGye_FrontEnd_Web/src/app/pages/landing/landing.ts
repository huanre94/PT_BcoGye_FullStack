import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { LoginService, Product } from '../../services/login';

@Component({
  selector: 'app-landing',
  standalone: false,
  templateUrl: './landing.html',
  styleUrl: './landing.css'
})
export class Landing implements OnInit {
  products: Product[] = [];
  isLoading = false;
  errorMessage = '';

  constructor(
    private loginService: LoginService,
    private router: Router
  ) {}

  ngOnInit(): void {
    if (!this.loginService.isAuthenticated()) {
      this.router.navigate(['/login']);
      return;
    }
    this.loadProducts();
  }

  loadProducts(): void {
    this.isLoading = true;
    this.loginService.getProducts().subscribe({
      next: (products) => {
        this.products = products;
        this.isLoading = false;
      },
      error: (error) => {
        this.errorMessage = 'Error al cargar los productos';
        this.isLoading = false;
        console.error('Error loading products:', error);
      }
    });
  }

  goToConsultaProductos(): void {
    this.router.navigate(['/consulta-productos']);
  }

  goToAddProduct(): void {
    this.router.navigate(['/registro-productos']);
  }

  logout(): void {
    this.loginService.logout();
  }
}
