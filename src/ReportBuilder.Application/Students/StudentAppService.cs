using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using ReportBuilder.Permissions;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Guids;

namespace ReportBuilder.Students;

[Authorize(ReportBuilderPermissions.Students.Default)]
public class StudentAppService : ApplicationService, IStudentAppService
{
    private readonly IStudentRepository _studentRepository;
    private readonly IGuidGenerator _guidGenerator;

    public StudentAppService(IStudentRepository studentRepository, IGuidGenerator guidGenerator)
    {
        _studentRepository = studentRepository;
        _guidGenerator = guidGenerator;
    }

    public async Task<PagedResultDto<StudentSummaryDto>> GetListAsync(GetStudentListInput input)
    {
        var count = await _studentRepository.GetCountAsync(input.Filter, input.Gender, input.Status, input.Grade);
        var list = await _studentRepository.GetListAsync(
            input.Filter, input.Gender, input.Status, input.Grade,
            input.SkipCount, input.MaxResultCount);
        return new PagedResultDto<StudentSummaryDto>(
            count,
            ObjectMapper.Map<List<Student>, List<StudentSummaryDto>>(list));
    }

    public async Task<StudentDto> GetAsync(Guid id)
    {
        var student = await _studentRepository.GetAsync(id);
        return ObjectMapper.Map<Student, StudentDto>(student);
    }

    [Authorize(ReportBuilderPermissions.Students.Create)]
    public async Task<StudentDto> CreateAsync(CreateUpdateStudentDto input)
    {
        var student = new Student(
            _guidGenerator.Create(),
            input.Name, input.NationalId, input.Gender, input.DateOfBirth,
            input.Address, input.Country, input.Region, input.Mobile,
            input.Email, input.Status, input.Grade, input.EnrollmentDate,
            input.GPA, input.Notes);
        await _studentRepository.InsertAsync(student);
        return ObjectMapper.Map<Student, StudentDto>(student);
    }

    [Authorize(ReportBuilderPermissions.Students.Edit)]
    public async Task<StudentDto> UpdateAsync(Guid id, CreateUpdateStudentDto input)
    {
        var student = await _studentRepository.GetAsync(id);
        student.Update(
            input.Name, input.NationalId, input.Gender, input.DateOfBirth,
            input.Address, input.Country, input.Region, input.Mobile,
            input.Email, input.Status, input.Grade, input.EnrollmentDate,
            input.GPA, input.Notes);
        await _studentRepository.UpdateAsync(student);
        return ObjectMapper.Map<Student, StudentDto>(student);
    }

    [Authorize(ReportBuilderPermissions.Students.Delete)]
    public async Task DeleteAsync(Guid id)
    {
        await _studentRepository.DeleteAsync(id);
    }
}
