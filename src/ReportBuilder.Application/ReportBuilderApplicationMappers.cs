using Riok.Mapperly.Abstractions;
using ReportBuilder.Reports;
using ReportBuilder.Students;
using Volo.Abp.Mapperly;

namespace ReportBuilder;

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
public partial class ReportDefinitionToReportDefinitionDtoMapper : MapperBase<ReportDefinition, ReportDefinitionDto>
{
    public override partial ReportDefinitionDto Map(ReportDefinition source);

    public override partial void Map(ReportDefinition source, ReportDefinitionDto destination);
}

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
public partial class ReportDefinitionToReportDefinitionSummaryDtoMapper : MapperBase<ReportDefinition, ReportDefinitionSummaryDto>
{
    public override partial ReportDefinitionSummaryDto Map(ReportDefinition source);

    public override partial void Map(ReportDefinition source, ReportDefinitionSummaryDto destination);
}

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
public partial class ReportColumnToReportColumnDtoMapper : MapperBase<ReportColumn, ReportColumnDto>
{
    public override partial ReportColumnDto Map(ReportColumn source);

    public override partial void Map(ReportColumn source, ReportColumnDto destination);
}

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
public partial class ColumnPermissionToColumnPermissionDtoMapper : MapperBase<ColumnPermission, ColumnPermissionDto>
{
    public override partial ColumnPermissionDto Map(ColumnPermission source);

    public override partial void Map(ColumnPermission source, ColumnPermissionDto destination);
}

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
public partial class ReportParameterToReportParameterDtoMapper : MapperBase<ReportParameter, ReportParameterDto>
{
    public override partial ReportParameterDto Map(ReportParameter source);

    public override partial void Map(ReportParameter source, ReportParameterDto destination);
}

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
public partial class ReportPermissionToReportPermissionDtoMapper : MapperBase<ReportPermission, ReportPermissionDto>
{
    public override partial ReportPermissionDto Map(ReportPermission source);

    public override partial void Map(ReportPermission source, ReportPermissionDto destination);
}

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
public partial class StudentToStudentDtoMapper : MapperBase<Student, StudentDto>
{
    public override partial StudentDto Map(Student source);

    public override partial void Map(Student source, StudentDto destination);
}

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
public partial class StudentToStudentSummaryDtoMapper : MapperBase<Student, StudentSummaryDto>
{
    public override partial StudentSummaryDto Map(Student source);

    public override partial void Map(Student source, StudentSummaryDto destination);
}
