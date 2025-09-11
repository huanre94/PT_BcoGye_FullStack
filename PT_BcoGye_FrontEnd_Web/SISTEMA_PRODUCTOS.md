# Sistema de Login y Gestión de Productos

## 🔧 **Modo Desarrollo - Bypass del Login**

Para facilitar el desarrollo sin necesidad de backend, se ha implementado un **bypass completo del sistema de autenticación**:

### ✅ **Características del Bypass**

- **Login automático**: Acepta cualquier email y contraseña
- **Datos mock**: Productos de ejemplo precargados
- **Simulación realista**: Delays de red simulados
- **Mensaje informativo**: Indica que está en modo desarrollo

### 🎯 **Cómo Usar el Bypass**

1. **Accede al login** en `http://localhost:4200`
2. **Ingresa cualquier email y contraseña** (ejemplo: `test@test.com` / `123456`)
3. **Haz clic en "Iniciar Sesión"**
4. **¡Listo!** El sistema te llevará automáticamente al dashboard

### ⚙️ **Configuración del Bypass**

Para activar/desactivar el modo desarrollo, modifica el archivo:
```typescript
// src/app/services/login.ts
private isDevelopmentMode(): boolean {
  return true;  // true = bypass activo, false = backend real
}
```

### 📊 **Datos de Prueba Incluidos**

El sistema incluye **6 productos de ejemplo**:
- Smartphone Samsung Galaxy ($899.99)
- Laptop Dell XPS 13 ($1,299.99)
- Auriculares Sony WH-1000XM4 ($349.99)
- Smart TV LG 55" 4K ($599.99)
- Cafetera Nespresso ($199.99)
- Reloj Apple Watch Series 9 ($429.99)

---
Aplicación web desarrollada en Angular para gestión de productos con sistema de autenticación. Incluye funcionalidades de login, listado de productos y registro de nuevos productos.

## Funcionalidades Implementadas

### 🔐 **Sistema de Login**
- Formulario de login con validación de email y contraseña
- Validaciones de campos requeridos y formato de email
- Manejo de estados de carga y mensajes de error
- Redireccionamiento automático a landing después del login exitoso

### 🏠 **Landing Page**
- Navbar con título "Productos" y botones de acción
- Listado de productos en tarjetas con:
  - Nombre del producto
  - Precio
  - Descripción
  - Categoría
  - Botones de acción (Ver detalles, Editar)
- Botón "Agregar Producto" que redirige al formulario de registro
- Botón "Cerrar Sesión" para logout
- Estado de carga mientras se obtienen los productos
- Manejo de errores con datos de prueba como fallback

### ➕ **Registro de Productos**
- Formulario completo con validaciones para:
  - Nombre del producto (mínimo 3 caracteres)
  - Precio (debe ser mayor a 0)
  - Categoría (selección obligatoria)
  - Descripción (mínimo 10 caracteres)
- Validaciones en tiempo real
- Mensajes de éxito y error
- Redireccionamiento automático al landing después del registro

### 🔒 **Seguridad**
- Guard de autenticación para proteger rutas privadas
- Interceptor HTTP para agregar token de autorización automáticamente
- Manejo de localStorage para persistencia del token
- Redireccionamiento automático al login si no está autenticado

## Estructura de la Aplicación

```
src/app/
├── guards/
│   └── auth.guard.ts           # Guard para proteger rutas
├── interceptors/
│   └── auth.interceptor.ts     # Interceptor para HTTP requests
├── pages/
│   ├── login/                  # Componente de login
│   ├── landing/                # Landing page con listado de productos
│   ├── registro-productos/     # Formulario de registro de productos
│   └── consulta-productos/     # (Para futuras funcionalidades)
├── services/
│   └── login.ts               # Servicio principal con API calls
├── app-routing-module.ts       # Configuración de rutas
└── app-module.ts              # Módulo principal
```

## Configuración de API

El servicio está configurado para conectarse a una API en `http://localhost:3000/api` con los siguientes endpoints:

- `POST /api/login` - Autenticación de usuario
- `GET /api/products` - Obtener listado de productos
- `POST /api/products` - Crear nuevo producto

### Ejemplo de respuesta esperada para login:
```json
{
  "success": true,
  "token": "jwt_token_here",
  "message": "Login exitoso"
}
```

### Ejemplo de respuesta esperada para productos:
```json
[
  {
    "id": 1,
    "name": "Producto 1",
    "price": 100,
    "description": "Descripción del producto",
    "category": "Categoría A"
  }
]
```

## Modo de Prueba

La aplicación incluye datos de prueba para cuando la API no esté disponible:
- El login mostrará datos mock en caso de error de conexión
- El listado de productos mostrará productos de ejemplo
- El registro de productos simulará una respuesta exitosa

## Rutas Disponibles

- `/` - Redirige al login
- `/login` - Página de inicio de sesión
- `/landing` - Dashboard principal (requiere autenticación)
- `/registro-productos` - Formulario de registro (requiere autenticación)
- `/consulta-productos` - (Preparado para futuras funcionalidades)

## Estilos y Diseño

- Diseño responsive para dispositivos móviles y desktop
- Gradientes y efectos visuales modernos
- Validaciones visuales en formularios
- Estados de carga y retroalimentación al usuario
- Navbar consistente en todas las páginas

## Instrucciones de Uso

1. **Iniciar la aplicación:**
   ```bash
   npm start
   ```

2. **Acceder al login:**
   - Abrir http://localhost:4200
   - Ingresar email y contraseña
   - Hacer clic en "Iniciar Sesión"

3. **Navegar en el sistema:**
   - Después del login exitoso, se redirige al landing
   - Ver listado de productos disponibles
   - Usar "Agregar Producto" para registrar nuevos productos
   - Usar "Cerrar Sesión" para logout

4. **Registrar productos:**
   - Hacer clic en "Agregar Producto" desde el landing
   - Completar todos los campos requeridos
   - Hacer clic en "Guardar Producto"
   - El sistema redirige automáticamente al landing

## Tecnologías Utilizadas

- Angular 18+
- TypeScript
- Reactive Forms
- Angular Router
- HTTP Client
- CSS3 con Flexbox y Grid
- Font Awesome (iconos)

## Próximas Funcionalidades

- Edición de productos existentes
- Eliminación de productos
- Búsqueda y filtrado de productos
- Paginación en el listado
- Gestión de usuarios y roles
- Carga de imágenes para productos
