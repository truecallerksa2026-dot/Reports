import { Component, OnInit, ViewChild } from '@angular/core';
import { StudentService } from '../services/student.service';
import { StudentSummaryDto, StudentGender, StudentStatus, CreateUpdateStudentDto, StudentDto } from '../models/student.models';

@Component({
  selector: 'app-student-list',
  templateUrl: './student-list.component.html',
  standalone: false,
})
export class StudentListComponent implements OnInit {
  students: StudentSummaryDto[] = [];
  totalCount = 0;
  loading = false;

  modalVisible = false;
  modalTitle = 'New Student';
  editingId: string | null = null;
  saving = false;

  form: CreateUpdateStudentDto = this.emptyForm();

  genderOptions = [
    { value: StudentGender.Male, label: 'Male' },
    { value: StudentGender.Female, label: 'Female' },
  ];

  statusOptions = [
    { value: StudentStatus.Active, label: 'Active' },
    { value: StudentStatus.Inactive, label: 'Inactive' },
    { value: StudentStatus.Graduated, label: 'Graduated' },
    { value: StudentStatus.Suspended, label: 'Suspended' },
  ];

  gradeOptions = ['Grade 1','Grade 2','Grade 3','Grade 4','Grade 5','Grade 6',
                  'Grade 7','Grade 8','Grade 9','Grade 10','Grade 11','Grade 12'];

  filterText = '';
  filterStatus: StudentStatus | '' = '';
  filterGender: StudentGender | '' = '';

  constructor(private studentService: StudentService) {}

  ngOnInit(): void {
    this.loadStudents();
  }

  loadStudents(skipCount = 0, maxResultCount = 20): void {
    this.loading = true;
    const input: any = { skipCount, maxResultCount };
    if (this.filterText) input.filter = this.filterText;
    if (this.filterStatus !== '') input.status = this.filterStatus;
    if (this.filterGender !== '') input.gender = this.filterGender;

    this.studentService.getList(input).subscribe({
      next: result => {
        this.students = result.items;
        this.totalCount = result.totalCount;
        this.loading = false;
      },
      error: () => { this.loading = false; }
    });
  }

  openCreate(): void {
    this.editingId = null;
    this.modalTitle = 'New Student';
    this.form = this.emptyForm();
    this.modalVisible = true;
  }

  openEdit(id: string): void {
    this.editingId = id;
    this.modalTitle = 'Edit Student';
    this.studentService.get(id).subscribe(dto => {
      this.form = {
        name: dto.name,
        nationalId: dto.nationalId,
        gender: dto.gender,
        dateOfBirth: dto.dateOfBirth?.substring(0, 10),
        address: dto.address,
        country: dto.country,
        region: dto.region,
        mobile: dto.mobile,
        email: dto.email,
        status: dto.status,
        grade: dto.grade,
        enrollmentDate: dto.enrollmentDate?.substring(0, 10),
        gpa: dto.gpa,
        notes: dto.notes,
      };
      this.modalVisible = true;
    });
  }

  save(): void {
    this.saving = true;
    const action = this.editingId
      ? this.studentService.update(this.editingId, this.form)
      : this.studentService.create(this.form);

    action.subscribe({
      next: () => {
        this.modalVisible = false;
        this.saving = false;
        this.loadStudents();
      },
      error: () => { this.saving = false; }
    });
  }

  delete(id: string): void {
    if (confirm('Delete this student?')) {
      this.studentService.delete(id).subscribe(() => this.loadStudents());
    }
  }

  getGenderLabel(gender: StudentGender): string {
    return StudentGender[gender];
  }

  getStatusLabel(status: StudentStatus): string {
    return StudentStatus[status];
  }

  getStatusBadgeClass(status: StudentStatus): string {
    switch (status) {
      case StudentStatus.Active:    return 'badge bg-success';
      case StudentStatus.Inactive:  return 'badge bg-secondary';
      case StudentStatus.Graduated: return 'badge bg-primary';
      case StudentStatus.Suspended: return 'badge bg-danger';
      default: return 'badge bg-secondary';
    }
  }

  onPageChange(e: any): void {
    this.loadStudents(e.skip, e.take);
  }

  private emptyForm(): CreateUpdateStudentDto {
    const today = new Date().toISOString().substring(0, 10);
    return {
      name: '', nationalId: '', gender: StudentGender.Male,
      dateOfBirth: '2000-01-01', address: '', country: 'Saudi Arabia',
      region: '', mobile: '', email: '', status: StudentStatus.Active,
      grade: 'Grade 10', enrollmentDate: today, gpa: 0, notes: '',
    };
  }
}
