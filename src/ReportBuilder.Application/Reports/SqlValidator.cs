using System.Text.RegularExpressions;
using Volo.Abp;
using Volo.Abp.DependencyInjection;

namespace ReportBuilder.Reports;

public class SqlValidator : ITransientDependency
{
    // Blocked keywords (case-insensitive, whole word match)
    private static readonly string[] BlockedKeywords =
    [
        "INSERT", "UPDATE", "DELETE", "DROP", "ALTER", "TRUNCATE", "CREATE",
        "EXEC", "EXECUTE", "GRANT", "REVOKE", "COPY", "BEGIN", "COMMIT",
        "ROLLBACK", "pg_read_file", "pg_write_file", "lo_import", "lo_export",
        "pg_sleep", "information_schema"
    ];

    public void Validate(string sql)
    {
        if (string.IsNullOrWhiteSpace(sql))
            throw new UserFriendlyException("SQL query cannot be empty.");

        var normalized = sql.Trim();

        // Must start with SELECT or WITH (CTEs)
        if (!Regex.IsMatch(normalized, @"^\s*(SELECT|WITH)\s", RegexOptions.IgnoreCase))
            throw new UserFriendlyException("SQL query must start with SELECT or WITH.");

        // Check for blocked keywords
        foreach (var keyword in BlockedKeywords)
        {
            if (Regex.IsMatch(normalized, $@"\b{Regex.Escape(keyword)}\b", RegexOptions.IgnoreCase))
                throw new UserFriendlyException($"SQL query contains a forbidden keyword: {keyword}.");
        }

        // Check for semicolons (statement chaining)
        if (normalized.Contains(';'))
            throw new UserFriendlyException("SQL query cannot contain semicolons.");

        // Check for SQL comments
        if (normalized.Contains("--") || Regex.IsMatch(normalized, @"/\*.*?\*/", RegexOptions.Singleline))
            throw new UserFriendlyException("SQL query cannot contain comments.");

        // Check for pg_* system catalog references
        if (Regex.IsMatch(normalized, @"\bpg_\w+", RegexOptions.IgnoreCase))
            throw new UserFriendlyException("SQL query cannot reference system catalogs.");
    }
}
