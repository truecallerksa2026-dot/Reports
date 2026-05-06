using Volo.Abp.Application.Dtos;

namespace ReportBuilder.Reports;

public class GetReportListInput : PagedAndSortedResultRequestDto
{
    public string? Filter { get; set; }
    public bool? IsActive { get; set; }
}
