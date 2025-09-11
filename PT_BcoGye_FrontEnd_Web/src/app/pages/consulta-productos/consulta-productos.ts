import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { LoginService, Product } from '../../services/login';

@Component({
  selector: 'app-consulta-productos',
  standalone: false,
  templateUrl: './consulta-productos.html',
  styleUrl: './consulta-productos.css'
})
export class ConsultaProductos implements OnInit {
  products: Product[] = [];
  filteredProducts: Product[] = [];
  isLoading = false;
  errorMessage = '';
  searchTerm = '';
  selectedCategory = '';
  categories: string[] = [];

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
        this.filteredProducts = products;
        this.extractCategories();
        this.isLoading = false;
      },
      error: (error) => {
        this.errorMessage = 'Error al cargar los productos';
        this.isLoading = false;
        console.error('Error loading products:', error);
      }
    });
  }

  extractCategories(): void {
    const uniqueCategories = [...new Set(this.products.map(p => p.category))];
    this.categories = uniqueCategories.sort();
  }

  filterProducts(): void {
    this.filteredProducts = this.products.filter(product => {
      const matchesSearch = !this.searchTerm || 
        product.name.toLowerCase().includes(this.searchTerm.toLowerCase()) ||
        product.description.toLowerCase().includes(this.searchTerm.toLowerCase());
      
      const matchesCategory = !this.selectedCategory || 
        product.category === this.selectedCategory;
      
      return matchesSearch && matchesCategory;
    });
  }

  onSearchChange(): void {
    this.filterProducts();
  }

  onCategoryChange(): void {
    this.filterProducts();
  }

  clearFilters(): void {
    this.searchTerm = '';
    this.selectedCategory = '';
    this.filteredProducts = [...this.products];
  }

  editProduct(productId: number): void {
    // Navegar a registro-productos con el ID del producto para editar
    this.router.navigate(['/registro-productos'], { 
      queryParams: { editId: productId }
    });
  }

  deleteProduct(productId: number): void {
    if (confirm('¿Está seguro de que desea eliminar este producto?')) {
      // Simulación de eliminación
      this.products = this.products.filter(p => p.id !== productId);
      this.filterProducts(); // Actualizar la vista filtrada
      // Aquí podrías hacer la llamada al servicio para eliminar del backend
    }
  }

  goToAddProduct(): void {
    this.router.navigate(['/registro-productos']);
  }

  goBack(): void {
    this.router.navigate(['/landing']);
  }

  logout(): void {
    this.loginService.logout();
  }
}
