import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DefautComponent } from './defaut.component';

describe('DefautComponent', () => {
  let component: DefautComponent;
  let fixture: ComponentFixture<DefautComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DefautComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DefautComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
