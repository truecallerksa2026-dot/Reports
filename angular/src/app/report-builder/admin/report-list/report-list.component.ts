import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ReportService } from '../../services/report.service';
import { ReportDefinitionSummaryDto, DisplayMode } from '../../models/report.models';

@Component({
  selector: 'app-report-list',
  templateUrl: './report-list.component.html',
  standalone: false,
})
export class ReportListComponent implements OnInit {
  reports: ReportDefinitionSummaryDto[] = [];
  totalCount = 0;
  loading = false;

  constructor(private reportService: ReportService, private router: Router) {}

  ngOnInit(): void {
    this.loadReports();
  }

  loadReports(skipCount = 0, maxResultCount = 10): void {
    this.loading = true;
    this.reportService.getList({ skipCount, maxResultCount }).subscribe({
      next: result => {
        this.reports = result.items;
        this.totalCount = result.totalCount;
        this.loading = false;
      },
      error: () => { this.loading = false; }
    });
  }

  createNew(): void {
    this.router.navigate(['/report-builder/admin/new']);
  }

  edit(id: string): void {
    this.router.navigate(['/report-builder/admin', id]);
  }

  delete(id: string): void {
    if (confirm('Are you sure you want to delete this report?')) {
      this.reportService.delete(id).subscribe(() => this.loadReports());
    }
  }

  toggleActive(report: ReportDefinitionSummaryDto): void {
    const action = report.isActive
      ? this.reportService.deactivate(report.id)
      : this.reportService.activate(report.id);
    action.subscribe(() => this.loadReports());
  }

  getDisplayModeLabel(mode: DisplayMode): string {
    return DisplayMode[mode];
  }
}
