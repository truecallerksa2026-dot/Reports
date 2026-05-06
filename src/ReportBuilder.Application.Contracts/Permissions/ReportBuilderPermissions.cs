namespace ReportBuilder.Permissions;

public static class ReportBuilderPermissions
{
    public const string GroupName = "ReportBuilder";

    public static class Dashboard
    {
        public const string DashboardGroup = GroupName + ".Dashboard";
        public const string Host = DashboardGroup + ".Host";
        public const string Tenant = DashboardGroup + ".Tenant";
    }

    public static class Students
    {
        public const string Default = GroupName + ".Students";
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
    }

    public static class Reports
    {
        public const string Default = GroupName + ".Reports";

        public static class Admin
        {
            public const string Default = Reports.Default + ".Admin";
            public const string Create = Default + ".Create";
            public const string Edit = Default + ".Edit";
            public const string Delete = Default + ".Delete";
            public const string TestRun = Default + ".TestRun";
        }

        public static class Viewer
        {
            public const string Default = Reports.Default + ".Viewer";
            public const string Grid = Default + ".Grid";
            public const string Report = Default + ".Report";
            public const string Export = Default + ".Export";
        }
    }
}
