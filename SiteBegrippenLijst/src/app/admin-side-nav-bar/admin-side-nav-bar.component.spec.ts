import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdminSideNavBarComponent } from './admin-side-nav-bar.component';

describe('AdminSideNavBarComponent', () => {
  let component: AdminSideNavBarComponent;
  let fixture: ComponentFixture<AdminSideNavBarComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [AdminSideNavBarComponent]
    });
    fixture = TestBed.createComponent(AdminSideNavBarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
