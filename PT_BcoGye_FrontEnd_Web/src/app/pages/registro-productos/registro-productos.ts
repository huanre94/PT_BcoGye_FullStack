import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { LoginService, ProductRequest, Product } from '../../services/login';

@Component({
  selector: 'app-registro-productos',
  standalone: false,
  templateUrl: './registro-productos.html',
  styleUrl: './registro-productos.css'
})
export class RegistroProductos implements OnInit {
  productForm: FormGroup;
  products: Product[] = [];
  isLoading = false;
  isLoadingProducts = false;
  successMessage = '';
  errorMessage = '';
  editingProductId: number | null = null;

  constructor(
    private fb: FormBuilder,
    private loginService: LoginService,
    private router: Router
  ) {
    this.productForm = this.fb.group({
      name: ['', [Validators.required, Validators.minLength(3)]],
      price: ['', [Validators.required, Validators.min(0.01)]],
      description: ['', [Validators.required, Validators.minLength(10)]],
      category: ['', [Validators.required]]
    });
  }

  ngOnInit(): void {
    this.loadProducts();
  }

  loadProducts(): void {
    this.isLoadingProducts = true;
    this.loginService.getProducts().subscribe({
      next: (products) => {
        this.products = products;
        this.isLoadingProducts = false;
      },
      error: (error) => {
        this.errorMessage = 'Error al cargar los productos';
        this.isLoadingProducts = false;
        console.error('Error loading products:', error);
      }
    });
  }

  onSubmit(): void {
    if (this.productForm.valid) {
      this.isLoading = true;
      this.errorMessage = '';
      this.successMessage = '';

      const productData: ProductRequest = {
        name: this.productForm.get('name')?.value,
        price: this.productForm.get('price')?.value,
        description: this.productForm.get('description')?.value,
        category: this.productForm.get('category')?.value
      };

      if (this.editingProductId) {
        // Modo edición (simulado)
        this.updateProduct(productData);
      } else {
        // Modo creación
        this.createProduct(productData);
      }
    }
  }

  createProduct(productData: ProductRequest): void {
    this.loginService.createProduct(productData).subscribe({
      next: (response) => {
        if (response.success) {
          this.successMessage = response.message || 'Producto registrado exitosamente';
          this.productForm.reset();
          this.loadProducts(); // Recargar la tabla
        } else {
          this.errorMessage = response.message || 'Error al registrar el producto';
        }
        this.isLoading = false;
      },
      error: (error) => {
        this.errorMessage = 'Error de conexión. Intente nuevamente.';
        this.isLoading = false;
        console.error('Error creating product:', error);
      }
    });
  }

  updateProduct(productData: ProductRequest): void {
    // Simulación de actualización
    setTimeout(() => {
      const productIndex = this.products.findIndex(p => p.id === this.editingProductId);
      if (productIndex !== -1) {
        this.products[productIndex] = {
          ...this.products[productIndex],
          ...productData
        };
        this.successMessage = 'Producto actualizado exitosamente';
        this.productForm.reset();
        this.editingProductId = null;
      }
      this.isLoading = false;
    }, 1000);
  }

  editProduct(product: Product): void {
    this.editingProductId = product.id;
    this.productForm.patchValue({
      name: product.name,
      price: product.price,
      description: product.description,
      category: product.category
    });
    
    // Scroll al formulario
    document.querySelector('.form-container')?.scrollIntoView({ behavior: 'smooth' });
  }

  deleteProduct(productId: number): void {
    if (confirm('¿Está seguro de que desea eliminar este producto?')) {
      // Simulación de eliminación
      this.products = this.products.filter(p => p.id !== productId);
      this.successMessage = 'Producto eliminado exitosamente';
    }
  }

  cancelEdit(): void {
    this.editingProductId = null;
    this.productForm.reset();
  }

  goBack(): void {
    this.router.navigate(['/landing']);
  }

  logout(): void {
    this.loginService.logout();
  }

  get name() { return this.productForm.get('name'); }
  get price() { return this.productForm.get('price'); }
  get description() { return this.productForm.get('description'); }
  get category() { return this.productForm.get('category'); }
}
