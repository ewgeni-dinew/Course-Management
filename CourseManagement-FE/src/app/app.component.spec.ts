import { HttpClientModule } from '@angular/common/http';
import { TestBed, waitForAsync } from '@angular/core/testing';
import { BrowserModule } from '@angular/platform-browser';
import { AccountModule } from './account/account.module';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { CoreModule } from './core/core.module';
import { HomeComponent } from './home/home.component';

describe('AppComponent', () => {
  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      imports: [
        BrowserModule,
        HttpClientModule,
        AppRoutingModule,
        CoreModule,
        AccountModule,
        CoreModule],
      declarations: [AppComponent, HomeComponent],
    }).compileComponents();
  }));

  it('should create the app', () => {
    const fixture = TestBed.createComponent(AppComponent);
    const app = fixture.componentInstance;
    expect(app).toBeTruthy();
  });

  /* it(`should have as title 'CourseManagement-FE'`, () => {
    const fixture = TestBed.createComponent(AppComponent);
    const app = fixture.componentInstance;
    expect(app.title).toEqual('CourseManagement-FE');
  });

  it('should render title', () => {
    const fixture = TestBed.createComponent(AppComponent);
    fixture.detectChanges();
    const compiled = fixture.nativeElement;
    expect(compiled.querySelector('.content span').textContent).toContain('CourseManagement-FE app is running!');
  }); */
});
