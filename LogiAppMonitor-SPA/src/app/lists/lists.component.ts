import { Component, OnInit } from '@angular/core';
import { LogMessage } from '../_models/logMessage';
import { AlertifyService } from '../_services/alertify.service';
import { LogMessageService } from '../_services/logMessage.service';
import { AuthService } from '../_services/auth.service';
import { ActivatedRoute } from '@angular/router';
import { PaginatedResult, Pagination } from '../_models/pagination';

@Component({
  selector: 'app-lists',
  templateUrl: './lists.component.html',
  styleUrls: ['./lists.component.css']
})
export class ListsComponent implements OnInit {
  logMessages: LogMessage[];
  pagination: Pagination;

  constructor(private logMessageService: LogMessageService, private alertify: AlertifyService,
    private authService: AuthService, private route: ActivatedRoute) { }

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.logMessages = data['logMessages'].result;
      this.pagination = data['logMessages'].pagination;
    });
  }

  loadLogs() {
    this.logMessageService.getPosts(this.pagination.currentPage, this.pagination.itemsPerPage)
      .subscribe((res: PaginatedResult<LogMessage[]>) => {
      this.logMessages = res.result;
      this.pagination = res.pagination;
    }, error => {
      this.alertify.error(error);
    });
  }

  pageChanged(event: any): void {
    this.pagination.currentPage = event.page;
    this.loadLogs();
    // window.scrollTo(0, 0);
  }

  logCategory(value: string) {
    switch (value.toLowerCase()) {
      case 'debug': {
        return 'table-info';
      }
      case 'exception': {
        return 'table-danger';
      }
      case 'info': {
        return 'table-secondary';
      }
      case 'warn': {
        return 'table-warning';
      }
      default: {
        return null;
      }
    }
  }
}
