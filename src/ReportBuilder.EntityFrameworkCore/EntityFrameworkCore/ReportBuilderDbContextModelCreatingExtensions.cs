using Microsoft.EntityFrameworkCore;
using ReportBuilder.Reports;
using ReportBuilder.Students;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace ReportBuilder.EntityFrameworkCore;

public static class ReportBuilderDbContextModelCreatingExtensions
{
    public static void ConfigureReportBuilder(this ModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));

        builder.Entity<Student>(b =>
        {
            b.ToTable("RB_Students");
            b.ConfigureByConvention();
            b.Property(x => x.Name).IsRequired().HasMaxLength(StudentConsts.MaxNameLength);
            b.Property(x => x.NationalId).IsRequired().HasMaxLength(StudentConsts.MaxNationalIdLength);
            b.Property(x => x.Gender).IsRequired();
            b.Property(x => x.DateOfBirth).IsRequired();
            b.Property(x => x.Address).IsRequired().HasMaxLength(StudentConsts.MaxAddressLength);
            b.Property(x => x.Country).IsRequired().HasMaxLength(StudentConsts.MaxCountryLength);
            b.Property(x => x.Region).IsRequired().HasMaxLength(StudentConsts.MaxRegionLength);
            b.Property(x => x.Mobile).IsRequired().HasMaxLength(StudentConsts.MaxMobileLength);
            b.Property(x => x.Email).IsRequired().HasMaxLength(StudentConsts.MaxEmailLength);
            b.Property(x => x.Status).IsRequired();
            b.Property(x => x.Grade).IsRequired().HasMaxLength(StudentConsts.MaxGradeLength);
            b.Property(x => x.EnrollmentDate).IsRequired();
            b.Property(x => x.GPA).IsRequired().HasColumnType("decimal(3,2)");
            b.Property(x => x.Notes).HasMaxLength(StudentConsts.MaxNotesLength);
            b.HasIndex(x => x.NationalId).IsUnique();
            b.HasIndex(x => x.Name);
        });

        builder.Entity<ReportDefinition>(b =>
        {
            b.ToTable("RB_ReportDefinitions");
            b.ConfigureByConvention();
            b.Property(x => x.Name).IsRequired().HasMaxLength(ReportConsts.MaxNameLength);
            b.Property(x => x.Description).HasMaxLength(ReportConsts.MaxDescriptionLength);
            b.Property(x => x.SqlQuery).IsRequired().HasMaxLength(ReportConsts.MaxSqlQueryLength);
            b.Property(x => x.DisplayMode).IsRequired();
            b.Property(x => x.IsActive).IsRequired();
            b.HasIndex(x => x.Name);

            b.HasMany(x => x.Columns).WithOne().HasForeignKey(c => c.ReportDefinitionId).OnDelete(DeleteBehavior.Cascade);
            b.HasMany(x => x.Parameters).WithOne().HasForeignKey(p => p.ReportDefinitionId).OnDelete(DeleteBehavior.Cascade);
            b.HasMany(x => x.Permissions).WithOne().HasForeignKey(p => p.ReportDefinitionId).OnDelete(DeleteBehavior.Cascade);
        });

        builder.Entity<ReportColumn>(b =>
        {
            b.ToTable("RB_ReportColumns");
            b.ConfigureByConvention();
            b.Property(x => x.FieldName).IsRequired().HasMaxLength(ReportConsts.MaxFieldNameLength);
            b.Property(x => x.DisplayName).IsRequired().HasMaxLength(ReportConsts.MaxDisplayNameLength);
            b.Property(x => x.DataType).IsRequired();
            b.Property(x => x.DisplayOrder).IsRequired();

            b.HasMany(x => x.ColumnPermissions).WithOne().HasForeignKey(p => p.ReportColumnId).OnDelete(DeleteBehavior.Cascade);
        });

        builder.Entity<ColumnPermission>(b =>
        {
            b.ToTable("RB_ColumnPermissions");
            b.ConfigureByConvention();
            b.Property(x => x.RoleName).IsRequired().HasMaxLength(ReportConsts.MaxRoleNameLength);
        });

        builder.Entity<ReportParameter>(b =>
        {
            b.ToTable("RB_ReportParameters");
            b.ConfigureByConvention();
            b.Property(x => x.ParameterName).IsRequired().HasMaxLength(ReportConsts.MaxParameterNameLength);
            b.Property(x => x.DisplayName).IsRequired().HasMaxLength(ReportConsts.MaxDisplayNameLength);
            b.Property(x => x.DataType).IsRequired();
        });

        builder.Entity<ReportPermission>(b =>
        {
            b.ToTable("RB_ReportPermissions");
            b.ConfigureByConvention();
            b.Property(x => x.RoleName).IsRequired().HasMaxLength(ReportConsts.MaxRoleNameLength);
            b.HasIndex(x => new { x.ReportDefinitionId, x.RoleName }).IsUnique();
        });
    }
}
