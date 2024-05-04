import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { AppComponent } from './app.component';

describe('AppComponent', () => {
  let component: AppComponent;
  let fixture: ComponentFixture<AppComponent>;
  let httpMock: HttpTestingController;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AppComponent],
      imports: [HttpClientTestingModule]
    }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AppComponent);
    component = fixture.componentInstance;
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
  });

  it('should create the app', () => {
    expect(component).toBeTruthy();
  });

  it('should retrieve categories from the server', () => {
    const mockCategories = [
      { Id: '74892eba-ae27-467b-8f4b-5060b46fd76c', Name: 'Electronics' },
      { Id: 'da221366-a4ad-45d0-a6ab-9716bd4e8625', Name: 'Clothing' }
    ];

    component.ngOnInit();

    const req = httpMock.expectOne('/categories');
    expect(req.request.method).toEqual('GET');
    req.flush(mockCategories);

    expect(component.categories).toEqual(mockCategories as any);
  });
});
