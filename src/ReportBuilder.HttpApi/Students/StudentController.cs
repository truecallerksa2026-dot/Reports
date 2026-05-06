using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ReportBuilder.Controllers;
using Volo.Abp.Application.Dtos;

namespace ReportBuilder.Students;

[Route("api/report-builder/students")]
public class StudentController : ReportBuilderController
{
    private readonly IStudentAppService _studentService;

    public StudentController(IStudentAppService studentService)
    {
        _studentService = studentService;
    }

    [HttpGet]
    public Task<PagedResultDto<StudentSummaryDto>> GetListAsync([FromQuery] GetStudentListInput input) =>
        _studentService.GetListAsync(input);

    [HttpGet("{id:guid}")]
    public Task<StudentDto> GetAsync(Guid id) =>
        _studentService.GetAsync(id);

    [HttpPost]
    public Task<StudentDto> CreateAsync([FromBody] CreateUpdateStudentDto input) =>
        _studentService.CreateAsync(input);

    [HttpPut("{id:guid}")]
    public Task<StudentDto> UpdateAsync(Guid id, [FromBody] CreateUpdateStudentDto input) =>
        _studentService.UpdateAsync(id, input);

    [HttpDelete("{id:guid}")]
    public Task DeleteAsync(Guid id) =>
        _studentService.DeleteAsync(id);
}
