using System;
using System.ComponentModel.DataAnnotations;

namespace ReportBuilder.Students;

public class CreateUpdateStudentDto
{
    [Required]
    [MaxLength(StudentConsts.MaxNameLength)]
    public string Name { get; set; } = default!;

    [Required]
    [MaxLength(StudentConsts.MaxNationalIdLength)]
    public string NationalId { get; set; } = default!;

    public StudentGender Gender { get; set; }

    public DateTime DateOfBirth { get; set; }

    [MaxLength(StudentConsts.MaxAddressLength)]
    public string Address { get; set; } = string.Empty;

    [MaxLength(StudentConsts.MaxCountryLength)]
    public string Country { get; set; } = string.Empty;

    [MaxLength(StudentConsts.MaxRegionLength)]
    public string Region { get; set; } = string.Empty;

    [MaxLength(StudentConsts.MaxMobileLength)]
    public string Mobile { get; set; } = string.Empty;

    [MaxLength(StudentConsts.MaxEmailLength)]
    public string Email { get; set; } = string.Empty;

    public StudentStatus Status { get; set; }

    [MaxLength(StudentConsts.MaxGradeLength)]
    public string Grade { get; set; } = string.Empty;

    public DateTime EnrollmentDate { get; set; }

    [Range(0.0, 4.0)]
    public decimal GPA { get; set; }

    [MaxLength(StudentConsts.MaxNotesLength)]
    public string? Notes { get; set; }
}
