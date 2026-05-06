using System;
using Volo.Abp.Application.Dtos;

namespace ReportBuilder.Students;

public class StudentSummaryDto : EntityDto<Guid>
{
    public string Name { get; set; } = default!;
    public string NationalId { get; set; } = default!;
    public StudentGender Gender { get; set; }
    public string Country { get; set; } = default!;
    public string Region { get; set; } = default!;
    public string Mobile { get; set; } = default!;
    public string Email { get; set; } = default!;
    public StudentStatus Status { get; set; }
    public string Grade { get; set; } = default!;
    public decimal GPA { get; set; }
}
