using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace ReportBuilder.Students;

public class Student : FullAuditedAggregateRoot<Guid>
{
    public string Name { get; private set; } = default!;
    public string NationalId { get; private set; } = default!;
    public StudentGender Gender { get; private set; }
    public DateTime DateOfBirth { get; private set; }
    public string Address { get; private set; } = default!;
    public string Country { get; private set; } = default!;
    public string Region { get; private set; } = default!;
    public string Mobile { get; private set; } = default!;
    public string Email { get; private set; } = default!;
    public StudentStatus Status { get; private set; }
    public string Grade { get; private set; } = default!;
    public DateTime EnrollmentDate { get; private set; }
    public decimal GPA { get; private set; }
    public string? Notes { get; private set; }

    protected Student() { }

    public Student(
        Guid id,
        string name,
        string nationalId,
        StudentGender gender,
        DateTime dateOfBirth,
        string address,
        string country,
        string region,
        string mobile,
        string email,
        StudentStatus status,
        string grade,
        DateTime enrollmentDate,
        decimal gpa,
        string? notes = null)
    {
        Id = id;
        Name = name;
        NationalId = nationalId;
        Gender = gender;
        DateOfBirth = dateOfBirth;
        Address = address;
        Country = country;
        Region = region;
        Mobile = mobile;
        Email = email;
        Status = status;
        Grade = grade;
        EnrollmentDate = enrollmentDate;
        GPA = gpa;
        Notes = notes;
    }

    public void Update(
        string name,
        string nationalId,
        StudentGender gender,
        DateTime dateOfBirth,
        string address,
        string country,
        string region,
        string mobile,
        string email,
        StudentStatus status,
        string grade,
        DateTime enrollmentDate,
        decimal gpa,
        string? notes = null)
    {
        Name = name;
        NationalId = nationalId;
        Gender = gender;
        DateOfBirth = dateOfBirth;
        Address = address;
        Country = country;
        Region = region;
        Mobile = mobile;
        Email = email;
        Status = status;
        Grade = grade;
        EnrollmentDate = enrollmentDate;
        GPA = gpa;
        Notes = notes;
    }
}
