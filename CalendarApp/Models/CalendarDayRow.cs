using System.Globalization;

namespace CalendarApp.Models;

public sealed class CalendarDayRow
{
    public int ImportId { get; init; }

    public DateOnly GregorianDate { get; init; }

    public byte YomDay { get; init; }

    public string? YomNote { get; init; }

    public int MoonId { get; init; }

    public string MonthDescription { get; init; } = "";

    public int Year => int.Parse(MonthDescription.Trim()[^4..], CultureInfo.InvariantCulture);

    public string Month => MonthDescription.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries)[1];

    public string DateText => GregorianDate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
}
