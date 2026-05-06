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
  selector: 'app-report-viewer',
  templateUrl: './report-viewer.component.html',
  standalone: false,
})
export class ReportViewerComponent implements OnInit {
  report: ReportDefinitionDto | null = null;
  today = new Date();
  result: ReportResultDto | null = null;
  loading = false;
  currentParams: Record<string, any> = {};
  pageIndex = 0;
  pageSize = 50;

  constructor(private route: ActivatedRoute, private reportService: ReportService) {}

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id')!;
    this.reportService.get(id).subscribe(r => {
      this.report = r;
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

  loadMore(): void {
    if (!this.result) return;
    this.pageIndex++;
    this.loading = true;
    const input: ExecuteReportInput = {
      parameters: this.currentParams,
      skipCount: this.pageIndex * this.pageSize,
      maxResultCount: this.pageSize,
      sortDescending: false,
    };
    this.reportService.execute(this.report!.id, input).subscribe({
      next: result => {
        this.result = {
          ...result,
          rows: [...(this.result?.rows || []), ...result.rows],
        };
        this.loading = false;
      },
      error: () => { this.loading = false; }
    });
  }

  get hasMore(): boolean {
    if (!this.result) return false;
    return this.result.rows.length < this.result.totalCount;
  }

  getCellValue(row: any, col: ReportColumnDto): any {
    return row[col.fieldName] ?? row[col.fieldName.toLowerCase()] ?? '';
  }

  formatValue(value: any, col: ReportColumnDto): string {
    if (value === null || value === undefined || value === '') return '—';
    if (col.dataType === 2) {
      // Date
      const d = new Date(value);
      return isNaN(d.getTime()) ? value : d.toLocaleDateString();
    }
    if (col.dataType === 3) {
      // Bool
      return value ? 'Yes' : 'No';
    }
    return String(value);
  }

  print(): void {
    window.print();
  }
}
