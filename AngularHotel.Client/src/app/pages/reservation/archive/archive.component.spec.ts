import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ArchivedReservationsComponent } from './archive.component';

describe('ReservationComponent', () => {
  let component: ArchivedReservationsComponent;
  let fixture: ComponentFixture<ArchivedReservationsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ArchivedReservationsComponent]
    })
      .compileComponents();

    fixture = TestBed.createComponent(ArchivedReservationsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
