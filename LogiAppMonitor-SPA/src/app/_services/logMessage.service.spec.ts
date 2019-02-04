/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { LogMessageService } from './logMessage.service';

describe('Service: LogMessage', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [LogMessageService]
    });
  });

  it('should ...', inject([LogMessageService], (service: LogMessageService) => {
    expect(service).toBeTruthy();
  }));
});
