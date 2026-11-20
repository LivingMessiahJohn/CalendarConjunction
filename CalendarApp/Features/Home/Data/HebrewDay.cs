using CalendarApp.Models;

namespace CalendarApp.Features.Home.Data;

public sealed record HebrewDay(
	DateOnly Date,
	int? HebrewYear,
	string? MonthName,
	int? Yom,
	IReadOnlyList<EventBadge> Events,
	IReadOnlyList<CountBadge> Counts);

public readonly record struct MonthLabels(string? Earlier, string? Later);
