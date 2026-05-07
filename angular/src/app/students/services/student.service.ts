import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { EnvironmentService } from '@abp/ng.core';
import {
  StudentDto,
  StudentSummaryDto,
  CreateUpdateStudentDto,
  GetStudentListInput,
  PagedResult,
} from '../models/student.models';

@Injectable({ providedIn: 'root' })
export class StudentService {
  private get baseUrl(): string {
    return this.environment.getApiUrl('default') + '/api/report-builder/students';
  }

  constructor(private http: HttpClient, private environment: EnvironmentService) {}

  getList(input: GetStudentListInput = {}): Observable<PagedResult<StudentSummaryDto>> {
    const params: any = {};
    if (input.filter) params.filter = input.filter;
    if (input.gender !== undefined && input.gender !== null) params.gender = input.gender;
    if (input.status !== undefined && input.status !== null) params.status = input.status;
    if (input.grade) params.grade = input.grade;
    if (input.skipCount !== undefined) params.skipCount = input.skipCount;
    if (input.maxResultCount !== undefined) params.maxResultCount = input.maxResultCount;
    return this.http.get<PagedResult<StudentSummaryDto>>(this.baseUrl, { params });
  }

  get(id: string): Observable<StudentDto> {
    return this.http.get<StudentDto>(`${this.baseUrl}/${id}`);
  }

  create(input: CreateUpdateStudentDto): Observable<StudentDto> {
    return this.http.post<StudentDto>(this.baseUrl, input);
  }

  update(id: string, input: CreateUpdateStudentDto): Observable<StudentDto> {
    return this.http.put<StudentDto>(`${this.baseUrl}/${id}`, input);
  }

  delete(id: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`);
  }
}
