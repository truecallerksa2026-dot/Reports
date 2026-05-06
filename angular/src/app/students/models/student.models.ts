export enum StudentGender {
  Male = 0,
  Female = 1,
}

export enum StudentStatus {
  Active = 0,
  Inactive = 1,
  Graduated = 2,
  Suspended = 3,
}

export interface StudentSummaryDto {
  id: string;
  name: string;
  nationalId: string;
  gender: StudentGender;
  country: string;
  region: string;
  mobile: string;
  email: string;
  status: StudentStatus;
  grade: string;
  gpa: number;
}

export interface StudentDto {
  id: string;
  name: string;
  nationalId: string;
  gender: StudentGender;
  dateOfBirth: string;
  address: string;
  country: string;
  region: string;
  mobile: string;
  email: string;
  status: StudentStatus;
  grade: string;
  enrollmentDate: string;
  gpa: number;
  notes?: string;
  creationTime?: string;
  lastModificationTime?: string;
}

export interface CreateUpdateStudentDto {
  name: string;
  nationalId: string;
  gender: StudentGender;
  dateOfBirth: string;
  address: string;
  country: string;
  region: string;
  mobile: string;
  email: string;
  status: StudentStatus;
  grade: string;
  enrollmentDate: string;
  gpa: number;
  notes?: string;
}

export interface GetStudentListInput {
  filter?: string;
  gender?: StudentGender;
  status?: StudentStatus;
  grade?: string;
  skipCount?: number;
  maxResultCount?: number;
}

export interface PagedResult<T> {
  items: T[];
  totalCount: number;
}
