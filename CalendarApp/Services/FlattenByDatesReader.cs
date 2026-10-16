using CalendarApp.Models;
using Microsoft.Data.SqlClient;

namespace CalendarApp.Services;

public sealed class FlattenByDatesReader(IConfiguration configuration)
{
    private readonly string _connectionString = configuration.GetConnectionString("Calendar")
        ?? throw new InvalidOperationException("Connection string 'Calendar' is missing.");

    public async Task<IReadOnlyList<CalendarDayRow>> GetAllAsync(CancellationToken cancellationToken)
    {
        const string sql = """
            SELECT
                ImportId,
                GregorianDate,
                YomDay,
                YomNote,
                RoshKodeshErev,
                RoshKodeshDay,
                MoonHalfWaning,
                MoonHalfWaxing,
                MoonFull,
                MonthDescription
            FROM dbo.FlattenByDates
            ORDER BY GregorianDate, YomDay;
            """;

        var rows = new List<CalendarDayRow>();

        await using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync(cancellationToken);
        await using var command = new SqlCommand(sql, connection);
        await using var reader = await command.ExecuteReaderAsync(cancellationToken);

        while (await reader.ReadAsync(cancellationToken))
        {
            rows.Add(new CalendarDayRow
            {
                ImportId = reader.GetInt32(0),
                GregorianDate = DateOnly.FromDateTime(reader.GetDateTime(1)),
                YomDay = reader.GetByte(2),
                YomNote = reader.IsDBNull(3) ? null : reader.GetString(3),
                RoshKodeshErev = reader.GetBoolean(4),
                RoshKodeshDay = reader.GetBoolean(5),
                MoonHalfWaning = reader.GetBoolean(6),
                MoonHalfWaxing = reader.GetBoolean(7),
                MoonFull = reader.GetBoolean(8),
                MonthDescription = reader.GetString(9)
            });
        }

        return rows;
    }
}
