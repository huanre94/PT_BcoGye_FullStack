import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { delay, map } from 'rxjs/operators';
import { Product } from '../models/product.model';

@Injectable({
  providedIn: 'root'
})
export class ProductService {
  private apiUrl = 'https://api.example.com'; // Cambia por tu API real
  private mockProducts: Product[] = [];

  constructor(private http: HttpClient) {
    this.generateMockProducts();
  }

  private generateMockProducts(): void {
    for (let i = 1; i <= 50; i++) {
      this.mockProducts.push({
        id: i.toString(),
        name: `Producto ${i}`,
        price: Math.floor(Math.random() * 1000) + 10,
        image: `https://picsum.photos/200/200?random=${i}`,
        description: `Descripción del producto ${i}`
      });
    }
  }

  getProducts(page: number = 1, limit: number = 10): Observable<Product[]> {
    // Simulación de paginación
    const startIndex = (page - 1) * limit;
    const endIndex = startIndex + limit;
    const paginatedProducts = this.mockProducts.slice(startIndex, endIndex);

    return of(paginatedProducts).pipe(
      delay(500) // Simular delay de red
    );

    // Para API real, descomenta esta línea:
    // return this.http.get<Product[]>(`${this.apiUrl}/products?page=${page}&limit=${limit}`);
  }

  getProductById(id: string): Observable<Product | undefined> {
    const product = this.mockProducts.find(p => p.id === id);
    return of(product).pipe(delay(300));

    // Para API real, descomenta esta línea:
    // return this.http.get<Product>(`${this.apiUrl}/products/${id}`);
  }
}
