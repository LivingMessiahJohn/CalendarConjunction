using System.Globalization;

namespace CalendarApp.Models;

public sealed class CalendarDayRow
{
    public int ImportId { get; init; }

    public DateOnly GregorianDate { get; init; }

    public byte YomDay { get; init; }

    public string? YomNote { get; init; }

    public bool RoshKodeshErev { get; init; }

    public bool RoshKodeshDay { get; init; }

    public bool MoonHalfWaning { get; init; }

    public bool MoonHalfWaxing { get; init; }

    public bool MoonFull { get; init; }

    public string MonthDescription { get; init; } = "";

    public int Year => int.Parse(MonthDescription.Trim()[^4..], CultureInfo.InvariantCulture);

    public string DateText => GregorianDate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);

    public string Month => GregorianDate.ToString("MMM", CultureInfo.InvariantCulture);
}
