using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ReportBuilder.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace ReportBuilder.Students;

public class EfCoreStudentRepository : EfCoreRepository<ReportBuilderDbContext, Student, Guid>, IStudentRepository
{
    public EfCoreStudentRepository(IDbContextProvider<ReportBuilderDbContext> dbContextProvider)
        : base(dbContextProvider) { }

    public async Task<List<Student>> GetListAsync(
        string? filter = null,
        StudentGender? gender = null,
        StudentStatus? status = null,
        string? grade = null,
        int skipCount = 0,
        int maxResultCount = 10,
        CancellationToken cancellationToken = default)
    {
        var query = await BuildQueryAsync(filter, gender, status, grade);
        return await query
            .OrderBy(s => s.Name)
            .Skip(skipCount)
            .Take(maxResultCount)
            .ToListAsync(cancellationToken);
    }

    public async Task<int> GetCountAsync(
        string? filter = null,
        StudentGender? gender = null,
        StudentStatus? status = null,
        string? grade = null,
        CancellationToken cancellationToken = default)
    {
        var query = await BuildQueryAsync(filter, gender, status, grade);
        return await query.CountAsync(cancellationToken);
    }

    private async Task<IQueryable<Student>> BuildQueryAsync(
        string? filter = null,
        StudentGender? gender = null,
        StudentStatus? status = null,
        string? grade = null)
    {
        var dbContext = await GetDbContextAsync();
        return dbContext.Students
            .WhereIf(!filter.IsNullOrWhiteSpace(), s =>
                s.Name.Contains(filter!) ||
                s.NationalId.Contains(filter!) ||
                s.Email.Contains(filter!) ||
                s.Mobile.Contains(filter!))
            .WhereIf(gender.HasValue, s => s.Gender == gender!.Value)
            .WhereIf(status.HasValue, s => s.Status == status!.Value)
            .WhereIf(!grade.IsNullOrWhiteSpace(), s => s.Grade == grade);
    }
}
