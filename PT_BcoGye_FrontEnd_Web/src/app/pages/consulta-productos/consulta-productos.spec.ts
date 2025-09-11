import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ConsultaProductos } from './consulta-productos';

describe('ConsultaProductos', () => {
  let component: ConsultaProductos;
  let fixture: ComponentFixture<ConsultaProductos>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ConsultaProductos]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ConsultaProductos);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
