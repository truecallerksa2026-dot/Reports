using System;
using Volo.Abp.Application.Dtos;

namespace ReportBuilder.Students;

public class StudentDto : AuditedEntityDto<Guid>
{
    public string Name { get; set; } = default!;
    public string NationalId { get; set; } = default!;
    public StudentGender Gender { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string Address { get; set; } = default!;
    public string Country { get; set; } = default!;
    public string Region { get; set; } = default!;
    public string Mobile { get; set; } = default!;
    public string Email { get; set; } = default!;
    public StudentStatus Status { get; set; }
    public string Grade { get; set; } = default!;
    public DateTime EnrollmentDate { get; set; }
    public decimal GPA { get; set; }
    public string? Notes { get; set; }
}
