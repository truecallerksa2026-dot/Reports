using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace ReportBuilder.Students;

public interface IStudentRepository : IRepository<Student, Guid>
{
    Task<List<Student>> GetListAsync(
        string? filter = null,
        StudentGender? gender = null,
        StudentStatus? status = null,
        string? grade = null,
        int skipCount = 0,
        int maxResultCount = 10,
        CancellationToken cancellationToken = default);

    Task<int> GetCountAsync(
        string? filter = null,
        StudentGender? gender = null,
        StudentStatus? status = null,
        string? grade = null,
        CancellationToken cancellationToken = default);
}
