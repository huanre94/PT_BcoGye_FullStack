import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { Login } from './pages/login/login';
import { Landing } from './pages/landing/landing';
import { RegistroProductos } from './pages/registro-productos/registro-productos';
import { ConsultaProductos } from './pages/consulta-productos/consulta-productos';
import { AuthGuard } from './guards/auth.guard';

const routes: Routes = [
  { path: '', redirectTo: '/login', pathMatch: 'full' },
  { path: 'login', component: Login },
  { path: 'landing', component: Landing, canActivate: [AuthGuard] },
  { path: 'registro-productos', component: RegistroProductos, canActivate: [AuthGuard] },
  { path: 'consulta-productos', component: ConsultaProductos, canActivate: [AuthGuard] },
  { path: '**', redirectTo: '/login' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
