import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdminBegrippenLijstComponent } from './admin-begrippen-lijst.component';

describe('AdminBegrippenLijstComponent', () => {
  let component: AdminBegrippenLijstComponent;
  let fixture: ComponentFixture<AdminBegrippenLijstComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [AdminBegrippenLijstComponent]
    });
    fixture = TestBed.createComponent(AdminBegrippenLijstComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
