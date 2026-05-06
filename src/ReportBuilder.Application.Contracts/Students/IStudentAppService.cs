using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace ReportBuilder.Students;

public interface IStudentAppService : IApplicationService
{
    Task<PagedResultDto<StudentSummaryDto>> GetListAsync(GetStudentListInput input);
    Task<StudentDto> GetAsync(Guid id);
    Task<StudentDto> CreateAsync(CreateUpdateStudentDto input);
    Task<StudentDto> UpdateAsync(Guid id, CreateUpdateStudentDto input);
    Task DeleteAsync(Guid id);
}
