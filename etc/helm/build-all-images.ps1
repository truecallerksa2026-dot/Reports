./build-image.ps1 -ProjectPath "../../src/ReportBuilder.DbMigrator/ReportBuilder.DbMigrator.csproj" -ImageName reportbuilder/dbmigrator
./build-image.ps1 -ProjectPath "../../src/ReportBuilder.HttpApi.Host/ReportBuilder.HttpApi.Host.csproj" -ImageName reportbuilder/httpapihost
./build-image.ps1 -ProjectPath "../../angular" -ImageName reportbuilder/angular -ProjectType "angular"
