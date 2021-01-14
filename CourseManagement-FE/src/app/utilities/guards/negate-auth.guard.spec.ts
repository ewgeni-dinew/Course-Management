import { TestBed } from '@angular/core/testing';
import { Router } from '@angular/router';

import { NegateAuthGuard } from './negate-auth.guard';

describe('NegateAuthGuard', () => {
  let guard: NegateAuthGuard;

  beforeEach(() => {
    TestBed.configureTestingModule({
    });
    guard = TestBed.inject(NegateAuthGuard);
  });

  it('should be created', () => {
    expect(guard).toBeTruthy();
  });
});
