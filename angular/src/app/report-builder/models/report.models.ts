export enum DisplayMode {
  Grid = 0,
  Report = 1,
  Both = 2,
}

export enum ColumnDataType {
  String = 0,
  Number = 1,
  Date = 2,
  Bool = 3,
}

export enum ParameterDataType {
  String = 0,
  Number = 1,
  Date = 2,
  Bool = 3,
}

export interface ColumnPermissionDto {
  id?: string;
  roleName: string;
  isVisible?: boolean;
  isFilterable?: boolean;
}

export interface ReportColumnDto {
  id?: string;
  reportDefinitionId?: string;
  fieldName: string;
  displayName: string;
  dataType: ColumnDataType;
  isVisible: boolean;
  isFilterable: boolean;
  isSortable: boolean;
  isGroupable: boolean;
  displayOrder: number;
  width?: number;
  columnPermissions: ColumnPermissionDto[];
}

export interface ReportParameterDto {
  id?: string;
  parameterName: string;
  displayName: string;
  dataType: ParameterDataType;
  defaultValue?: string;
  isRequired: boolean;
}

export interface ReportPermissionDto {
  id?: string;
  roleName: string;
  canExport: boolean;
}

export interface ReportDefinitionDto {
  id: string;
  name: string;
  description?: string;
  sqlQuery: string;
  displayMode: DisplayMode;
  isActive: boolean;
  columns: ReportColumnDto[];
  parameters: ReportParameterDto[];
  permissions: ReportPermissionDto[];
  creationTime?: string;
  lastModificationTime?: string;
}

export interface ReportDefinitionSummaryDto {
  id: string;
  name: string;
  description?: string;
  displayMode: DisplayMode;
  isActive: boolean;
  creationTime?: string;
}

export interface PagedResult<T> {
  items: T[];
  totalCount: number;
}

export interface GetReportListInput {
  filter?: string;
  isActive?: boolean;
  skipCount?: number;
  maxResultCount?: number;
  sorting?: string;
}

export interface CreateUpdateReportDefinitionDto {
  name: string;
  description?: string;
  sqlQuery: string;
  displayMode: DisplayMode;
  isActive: boolean;
  columns: Omit<ReportColumnDto, 'id' | 'reportDefinitionId'>[];
  parameters: Omit<ReportParameterDto, 'id'>[];
  permissions: Omit<ReportPermissionDto, 'id'>[];
}

export interface ExecuteReportInput {
  parameters: Record<string, any>;
  skipCount: number;
  maxResultCount: number;
  sortField?: string;
  sortDescending: boolean;
}

export interface ReportResultDto {
  columns: ReportColumnDto[];
  rows: Record<string, any>[];
  totalCount: number;
}
