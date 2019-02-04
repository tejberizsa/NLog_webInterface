import { Injectable } from '@angular/core';
import { Resolve, Router, ActivatedRouteSnapshot } from '@angular/router';
import { LogMessage } from '../_models/logMessage';
import { LogMessageService } from '../_services/logMessage.service';
import { AlertifyService } from '../_services/alertify.service';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';

@Injectable()
export class LogMessageListResolver implements Resolve<LogMessage[]> {
    pageNumber = 1;
    pageSize = 10;

    constructor(private logMessageService: LogMessageService, private router: Router, private alertify: AlertifyService) {}

    resolve(route: ActivatedRouteSnapshot): Observable<LogMessage[]> {
        return this.logMessageService.getPosts(this.pageNumber, this.pageSize).pipe(
            catchError(error => {
                this.alertify.error('Problem retrieving data');
                this.router.navigate(['/home']);
                return of(null);
            })
        );
    }
}
