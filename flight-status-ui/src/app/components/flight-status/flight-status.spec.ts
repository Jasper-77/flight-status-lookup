import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlightStatus } from './flight-status';

describe('FlightStatus', () => {
  let component: FlightStatus;
  let fixture: ComponentFixture<FlightStatus>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [FlightStatus],
    }).compileComponents();

    fixture = TestBed.createComponent(FlightStatus);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
