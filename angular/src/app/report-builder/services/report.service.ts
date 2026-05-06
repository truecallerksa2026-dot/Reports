import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import {
  ReportDefinitionDto,
  ReportDefinitionSummaryDto,
  PagedResult,
  GetReportListInput,
  CreateUpdateReportDefinitionDto,
  ReportColumnDto,
  ExecuteReportInput,
  ReportResultDto,
} from '../models/report.models';

@Injectable({ providedIn: 'root' })
export class ReportService {
  private readonly baseUrl = '/api/report-builder/reports';

  constructor(private http: HttpClient) {}

  getList(input: GetReportListInput = {}): Observable<PagedResult<ReportDefinitionSummaryDto>> {
    return this.http.get<PagedResult<ReportDefinitionSummaryDto>>(this.baseUrl, {
      params: { ...input } as any,
    });
  }

  get(id: string): Observable<ReportDefinitionDto> {
    return this.http.get<ReportDefinitionDto>(`${this.baseUrl}/${id}`);
  }

  create(input: CreateUpdateReportDefinitionDto): Observable<ReportDefinitionDto> {
    return this.http.post<ReportDefinitionDto>(this.baseUrl, input);
  }

  update(id: string, input: CreateUpdateReportDefinitionDto): Observable<ReportDefinitionDto> {
    return this.http.put<ReportDefinitionDto>(`${this.baseUrl}/${id}`, input);
  }

  delete(id: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`);
  }

  activate(id: string): Observable<ReportDefinitionDto> {
    return this.http.post<ReportDefinitionDto>(`${this.baseUrl}/${id}/activate`, {});
  }

  deactivate(id: string): Observable<ReportDefinitionDto> {
    return this.http.post<ReportDefinitionDto>(`${this.baseUrl}/${id}/deactivate`, {});
  }

  discoverColumns(sqlQuery: string): Observable<ReportColumnDto[]> {
    return this.http.post<ReportColumnDto[]>(`${this.baseUrl}/discover-columns`, { sqlQuery });
  }

  execute(id: string, input: ExecuteReportInput): Observable<ReportResultDto> {
    return this.http.post<ReportResultDto>(`${this.baseUrl}/${id}/execute`, input);
  }

  getRoles(): Observable<{ items: Array<{ name: string }>; totalCount: number }> {
    return this.http.get<{ items: Array<{ name: string }>; totalCount: number }>(
      '/api/identity/roles?maxResultCount=1000'
    );
  }
}
