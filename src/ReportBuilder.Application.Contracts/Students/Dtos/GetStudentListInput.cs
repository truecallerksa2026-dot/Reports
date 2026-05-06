using Volo.Abp.Application.Dtos;

namespace ReportBuilder.Students;

public class GetStudentListInput : PagedAndSortedResultRequestDto
{
    public string? Filter { get; set; }
    public StudentGender? Gender { get; set; }
    public StudentStatus? Status { get; set; }
    public string? Grade { get; set; }
}
