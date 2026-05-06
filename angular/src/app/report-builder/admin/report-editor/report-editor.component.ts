import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ToasterService } from '@abp/ng.theme.shared';
import { ReportService } from '../../services/report.service';
import {
  ReportDefinitionDto,
  CreateUpdateReportDefinitionDto,
  DisplayMode,
} from '../../models/report.models';

@Component({
  selector: 'app-report-editor',
  templateUrl: './report-editor.component.html',
  standalone: false,
})
export class ReportEditorComponent implements OnInit {
  report: Partial<ReportDefinitionDto> = {
    name: '',
    description: '',
    sqlQuery: 'SELECT * FROM ',
    displayMode: DisplayMode.Both,
    isActive: true,
    columns: [],
    parameters: [],
    permissions: [],
  };

  isNew = true;
  activeTab = 'general';
  discoveringColumns = false;
  saving = false;
  roles: string[] = [];

  displayModes = [
    { value: DisplayMode.Grid, label: 'Grid' },
    { value: DisplayMode.Report, label: 'Report' },
    { value: DisplayMode.Both, label: 'Both' },
  ];

  monacoOptions = {
    language: 'sql',
    theme: 'vs-dark',
    minimap: { enabled: false },
    fontSize: 14,
    wordWrap: 'on',
    automaticLayout: true,
  };

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private reportService: ReportService,
    private toaster: ToasterService
  ) {}

  ngOnInit(): void {
    this.reportService.getRoles().subscribe({
      next: r => { this.roles = r.items.map(i => i.name).sort(); },
      error: () => { /* roles dropdown degrades gracefully */ }
    });

    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.isNew = false;
      this.reportService.get(id).subscribe(r => { this.report = { ...r }; });
    }
  }

  discoverColumns(): void {
    if (!this.report.sqlQuery) return;
    this.discoveringColumns = true;
    this.reportService.discoverColumns(this.report.sqlQuery).subscribe({
      next: columns => {
        this.report.columns = columns;
        this.activeTab = 'columns';
        this.discoveringColumns = false;
      },
      error: (err) => {
        this.discoveringColumns = false;
        const msg = err?.error?.error?.message || 'Failed to discover columns. Check your SQL query.';
        this.toaster.error(msg, 'Discover Columns');
      }
    });
  }

  save(): void {
    this.saving = true;
    const input: CreateUpdateReportDefinitionDto = {
      name: this.report.name!,
      description: this.report.description,
      sqlQuery: this.report.sqlQuery!,
      displayMode: this.report.displayMode!,
      isActive: this.report.isActive!,
      columns: this.report.columns || [],
      parameters: this.report.parameters || [],
      permissions: this.report.permissions || [],
    };

    const action = this.isNew
      ? this.reportService.create(input)
      : this.reportService.update(this.report.id!, input);

    action.subscribe({
      next: () => { this.saving = false; this.router.navigate(['/report-builder/admin']); },
      error: (err) => {
        this.saving = false;
        const msg = err?.error?.error?.message || 'Failed to save report.';
        this.toaster.error(msg, 'Save Report');
      }
    });
  }

  cancel(): void {
    this.router.navigate(['/report-builder/admin']);
  }

  addColPerm(col: any): void {
    if (!col.columnPermissions) col.columnPermissions = [];
    col.columnPermissions.push({ roleName: '', isVisible: true, isFilterable: true });
  }

  removeColPerm(col: any, index: number): void {
    col.columnPermissions?.splice(index, 1);
  }

  addPermission(): void {
    if (!this.report.permissions) this.report.permissions = [];
    this.report.permissions.push({ roleName: '', canExport: false } as any);
  }

  removePermission(index: number): void {
    this.report.permissions?.splice(index, 1);
  }

  fixDisplayNames(): void {
    if (!this.report.columns) return;
    this.report.columns.forEach(col => {
      col.displayName = this.toDisplayName(col.fieldName);
    });
  }

  moveUp(col: any): void {
    const cols = this.report.columns!;
    const idx = cols.indexOf(col);
    if (idx <= 0) return;
    const prev = cols[idx - 1];
    const tmp = prev.displayOrder;
    prev.displayOrder = col.displayOrder;
    col.displayOrder = tmp;
    cols.sort((a, b) => a.displayOrder - b.displayOrder);
    this.report.columns = [...cols];
  }

  moveDown(col: any): void {
    const cols = this.report.columns!;
    const idx = cols.indexOf(col);
    if (idx >= cols.length - 1) return;
    const next = cols[idx + 1];
    const tmp = next.displayOrder;
    next.displayOrder = col.displayOrder;
    col.displayOrder = tmp;
    cols.sort((a, b) => a.displayOrder - b.displayOrder);
    this.report.columns = [...cols];
  }

  private toDisplayName(fieldName: string): string {
    let spaced = fieldName.replace(/([a-z])([A-Z])/g, '$1 $2');
    spaced = spaced.replace(/([A-Z]+)([A-Z][a-z])/g, '$1 $2');
    return spaced.replace(/_/g, ' ').replace(/\b\w/g, c => c.toUpperCase());
  }
}
