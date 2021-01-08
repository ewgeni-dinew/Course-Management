import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { HttpErrorListComponent } from './http-error-list.component';

describe('HttpErrorListComponent', () => {
  let component: HttpErrorListComponent;
  let fixture: ComponentFixture<HttpErrorListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ HttpErrorListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(HttpErrorListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
