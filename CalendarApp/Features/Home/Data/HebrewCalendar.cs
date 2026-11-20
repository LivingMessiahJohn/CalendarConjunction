using CalendarApp.Enums;
using CalendarApp.Models;
using CalendarApp.Services;

namespace CalendarApp.Features.Home.Data;

public static class HebrewCalendar
{
	private static readonly IReadOnlyList<EventBadge> NoEvents = [];
	private static readonly IReadOnlyList<CountBadge> NoCounts = [];
	private static readonly Dictionary<DateOnly, HebrewDay> Days = Build();

	public static HebrewDay For(DateOnly date)
	{
		if (Days.TryGetValue(date, out var day))
		{
			return day;
		}

		return new HebrewDay(date, null, null, null, NoEvents, NoCounts);
	}

	public static MonthLabels LabelsFor(DateOnly date)
	{
		if (date.Year < 1 || date.Month is < 1 or > 12)
		{
			return default;
		}

		var names = new List<string>();
		var daysInMonth = DateTime.DaysInMonth(date.Year, date.Month);
		for (var dayNumber = 1; dayNumber <= daysInMonth; dayNumber++)
		{
			var name = For(new DateOnly(date.Year, date.Month, dayNumber)).MonthName;
			if (name is null)
			{
				continue;
			}

			if (names.Count == 0 || names[^1] != name)
			{
				names.Add(name);
			}
		}

		if (names.Count == 0)
		{
			return default;
		}

		if (names.Count == 1)
		{
			var first = For(new DateOnly(date.Year, date.Month, 1));
			if (first.MonthName == names[0] && first.Yom is > 1)
			{
				return new MonthLabels(names[0], null);
			}

			return new MonthLabels(null, names[0]);
		}

		return new MonthLabels(names[0], names[^1]);
	}

	// A date in [span.Date, span.NextDate) takes that moon. A gap has no Hebrew month.
	private static Dictionary<DateOnly, HebrewDay> Build()
	{
		var rows = ConstantCalendar.Build().ToDictionary(row => row.GregorianDate);
		var spans = ConstantCalendar.MonthSpans();
		var days = new Dictionary<DateOnly, HebrewDay>();
		var spanIndex = 0;

		for (var date = YearHelper.MinDate; date <= YearHelper.MaxDate; date = date.AddDays(1))
		{
			while (spanIndex < spans.Count && spans[spanIndex].NextDate <= date)
			{
				spanIndex++;
			}

			MonthSpan? span = null;
			if (spanIndex < spans.Count
					&& date >= spans[spanIndex].Date
					&& date < spans[spanIndex].NextDate)
			{
				span = spans[spanIndex];
			}

			rows.TryGetValue(date, out var row);
			days.Add(date, new HebrewDay(
					date,
					span?.Year,
					span?.Month.Name,
					span is null ? null : date.DayNumber - span.Date.DayNumber + 1,
					row?.Events ?? NoEvents,
					row?.Counts ?? NoCounts));
		}

		return days;
	}
}
