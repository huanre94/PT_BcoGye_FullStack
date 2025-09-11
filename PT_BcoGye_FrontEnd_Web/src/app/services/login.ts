import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { Router } from '@angular/router';
import { delay } from 'rxjs/operators';
import { environment } from '../../environments/environment';

export interface LoginRequest {
  email: string;
  password: string;
}

export interface LoginResponse {
  success: boolean;
  token?: string;
  message?: string;
}

export interface Product {
  id: number;
  name: string;
  price: number;
  description: string;
  category: string;
}

export interface ProductRequest {
  name: string;
  price: number;
  description: string;
  category: string;
}

export interface ProductResponse {
  success: boolean;
  message?: string;
  product?: Product;
}

@Injectable({
  providedIn: 'root'
})
export class LoginService {
  private apiUrl = environment.url; // Cambia por tu URL de API

  constructor(private http: HttpClient, private router: Router) { }

  login(credentials: LoginRequest): Observable<LoginResponse> {
    // Bypass del login - simula respuesta exitosa sin backend
    if (this.isDevelopmentMode()) {
      const mockResponse: LoginResponse = {
        success: true,
        token: 'mock-jwt-token-' + Date.now(),
        message: 'Login exitoso (modo desarrollo - sin backend)'
      };

      // Simular delay de red y retornar respuesta mock
      return of(mockResponse).pipe(delay(800));
    }

    // Código original para producción con backend real
    return this.http.post<LoginResponse>(`${this.apiUrl}/login`, credentials);
  }

  private isDevelopmentMode(): boolean {
    // ========================================
    // BYPASS DEL LOGIN Y API
    // ========================================
    // true  = Modo desarrollo (sin backend)
    // false = Modo producción (con backend)
    // 
    // Cuando tengas tu backend listo, cambia este valor a false
    return true;
  }

  getProducts(): Observable<Product[]> {
    // Bypass para modo desarrollo - datos mock
    if (this.isDevelopmentMode()) {
      const mockProducts: Product[] = [
        {
          id: 1,
          name: 'Smartphone Samsung Galaxy',
          price: 899.99,
          description: 'Smartphone de última generación con cámara de 108MP y pantalla AMOLED de 6.7 pulgadas',
          category: 'Electrónicos'
        },
        {
          id: 2,
          name: 'Laptop Dell XPS 13',
          price: 1299.99,
          description: 'Laptop ultradelgada con procesador Intel i7, 16GB RAM y SSD de 512GB',
          category: 'Computadoras'
        },
        {
          id: 3,
          name: 'Auriculares Sony WH-1000XM4',
          price: 349.99,
          description: 'Auriculares inalámbricos con cancelación de ruido activa y hasta 30 horas de batería',
          category: 'Audio'
        },
        {
          id: 4,
          name: 'Smart TV LG 55" 4K',
          price: 599.99,
          description: 'Televisor inteligente 4K UHD con HDR y sistema operativo webOS',
          category: 'Electrónicos'
        },
        {
          id: 5,
          name: 'Cafetera Nespresso',
          price: 199.99,
          description: 'Cafetera automática con sistema de cápsulas y vaporizador de leche integrado',
          category: 'Hogar'
        },
        {
          id: 6,
          name: 'Reloj Apple Watch Series 9',
          price: 429.99,
          description: 'Smartwatch con GPS, monitor de salud y resistencia al agua hasta 50 metros',
          category: 'Wearables'
        }
      ];

      return of(mockProducts).pipe(delay(600));
    }

    // Código original para producción con backend real
    return this.http.get<Product[]>(`${this.apiUrl}/products`);
  }

  createProduct(product: ProductRequest): Observable<ProductResponse> {
    // Bypass para modo desarrollo - simular creación exitosa
    if (this.isDevelopmentMode()) {
      const mockResponse: ProductResponse = {
        success: true,
        message: 'Producto creado exitosamente (modo desarrollo)',
        product: {
          id: Date.now(), // ID simulado
          name: product.name,
          price: product.price,
          description: product.description,
          category: product.category
        }
      };

      return of(mockResponse).pipe(delay(1000));
    }

    // Código original para producción con backend real
    return this.http.post<ProductResponse>(`${this.apiUrl}/products`, product);
  }

  logout(): void {
    localStorage.removeItem('token');
    this.router.navigate(['/login']);
  }

  isAuthenticated(): boolean {
    return !!localStorage.getItem('token');
  }

  saveToken(token: string): void {
    localStorage.setItem('token', token);
  }
}
