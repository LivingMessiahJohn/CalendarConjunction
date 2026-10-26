namespace CalendarApp.Models;

public sealed record CountBadge(string Title, string Text, string BadgeCss);

public sealed record EventBadge(string Text, string BadgeCss, string? Icon = null, string Details = "");

public sealed class CalendarGridRow
{
	public int Year { get; init; }
	public string Month { get; init; } = "";
	public string MonthDescription { get; init; } = "";
	public DateOnly GregorianDate { get; init; }
	public string DateText { get; init; } = "";
	public int? Day { get; init; }
	public int MoonPhaseId { get; init; }
	public IReadOnlyList<CountBadge> Counts { get; init; } = [];
	public IReadOnlyList<EventBadge> Events { get; init; } = [];
}
