import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { NgbAlertModule } from '@ng-bootstrap/ng-bootstrap';
import { AlertComponent } from '../alert/alert.component';

import { AlertListComponent } from './alert-list.component';

describe('AlertListComponent', () => {
  let component: AlertListComponent;
  let fixture: ComponentFixture<AlertListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [NgbAlertModule],
      declarations: [AlertListComponent, AlertComponent]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AlertListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
