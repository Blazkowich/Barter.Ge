import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { CategoriesComponent } from './categories.component';

describe('CategoriesComponent', () => {
  let component: CategoriesComponent;
  let fixture: ComponentFixture<CategoriesComponent>;
  let httpMock: HttpTestingController;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [CategoriesComponent],
      imports: [HttpClientTestingModule]
    }).compileComponents();

    beforeEach(() => {
      fixture = TestBed.createComponent(CategoriesComponent);
      component = fixture.componentInstance;
      fixture.detectChanges();
    });

    afterEach(() => {
      httpMock.verify();
    })

    it('should create', () => {
      expect(component).toBeTruthy();
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
});
