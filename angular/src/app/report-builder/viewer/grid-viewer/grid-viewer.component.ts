import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ReportService } from '../../services/report.service';
import {
  ReportDefinitionDto,
  ReportColumnDto,
  ReportResultDto,
  ExecuteReportInput,
} from '../../models/report.models';

@Component({
  selector: 'app-grid-viewer',
  templateUrl: './grid-viewer.component.html',
  standalone: false,
})
export class GridViewerComponent implements OnInit {
  report: ReportDefinitionDto | null = null;
  result: ReportResultDto | null = null;
  loading = false;
  currentParams: Record<string, any> = {};
  pageIndex = 0;
  pageSize = 20;

  constructor(private route: ActivatedRoute, private reportService: ReportService) {}

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id')!;
    this.reportService.get(id).subscribe(r => {
      this.report = r;
      // Auto-execute if no required parameters
      const hasRequired = r.parameters.some(p => p.isRequired);
      if (!hasRequired) this.execute({});
    });
  }

  execute(params: Record<string, any>): void {
    if (!this.report) return;
    this.currentParams = params;
    this.loading = true;
    const input: ExecuteReportInput = {
      parameters: params,
      skipCount: this.pageIndex * this.pageSize,
      maxResultCount: this.pageSize,
      sortDescending: false,
    };
    this.reportService.execute(this.report.id, input).subscribe({
      next: result => { this.result = result; this.loading = false; },
      error: () => { this.loading = false; }
    });
  }

  onPageChanged(e: any): void {
    this.pageIndex = e.component.pageIndex();
    this.pageSize = e.component.pageSize();
    this.execute(this.currentParams);
  }

  exportPdf(): void {
    if (!this.report) return;
    window.open(`/api/report-builder/reports/${this.report.id}/export?format=pdf`, '_blank');
  }

  exportExcel(): void {
    if (!this.report) return;
    window.open(`/api/report-builder/reports/${this.report.id}/export?format=excel`, '_blank');
  }
}
