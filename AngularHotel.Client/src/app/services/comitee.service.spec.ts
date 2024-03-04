import { TestBed } from '@angular/core/testing';
import { CommitteeService } from './comitee.service';

describe('CurrencyService', () => {
  let service: CommitteeService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(CommitteeService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
