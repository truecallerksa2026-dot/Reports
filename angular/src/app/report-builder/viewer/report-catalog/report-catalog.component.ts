import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ReportService } from '../../services/report.service';
import { ReportDefinitionSummaryDto, DisplayMode } from '../../models/report.models';

@Component({
  selector: 'app-report-catalog',
  templateUrl: './report-catalog.component.html',
  standalone: false,
})
export class ReportCatalogComponent implements OnInit {
  reports: ReportDefinitionSummaryDto[] = [];
  loading = false;
  DisplayMode = DisplayMode;

  constructor(private reportService: ReportService, private router: Router) {}

  ngOnInit(): void {
    this.loading = true;
    this.reportService.getList({ isActive: true, maxResultCount: 100 }).subscribe({
      next: result => { this.reports = result.items; this.loading = false; },
      error: () => { this.loading = false; }
    });
  }

  viewGrid(id: string): void {
    this.router.navigate(['/report-builder/view/grid', id]);
  }

  viewReport(id: string): void {
    this.router.navigate(['/report-builder/view/report', id]);
  }

  getDisplayModeLabel(mode: DisplayMode): string {
    return DisplayMode[mode];
  }
}
