using CalendarApp.Constants;
using CalendarApp.Enums;

namespace CalendarApp.Services;

public static class YearDefault
{
	public static YearEnum For(DateOnly today)
	{
		YearEnum? lastWithSukkot = null;
		foreach (var year in Enum.GetValues<YearEnum>())
		{
			if (year == YearEnum.All)
			{
				continue;
			}

			var end = SukkotLastDay(year);
			if (end == DateOnly.MinValue)
			{
				continue;
			}

			lastWithSukkot = year;
			if (today <= end)
			{
				return year;
			}
		}

		if (lastWithSukkot is null)
		{
			return YearEnum.All;
		}

		return Next(lastWithSukkot.Value) ?? YearEnum.All;
	}

	private static DateOnly SukkotLastDay(YearEnum year)
	{
		var start = FeastDay.Sukkot.DateFor(year);
		return start == DateOnly.MinValue
				? DateOnly.MinValue
				: start.AddDays(DayCount.Sukkot.TotalDays - 1);
	}

	private static YearEnum? Next(YearEnum year)
	{
		var seen = false;
		foreach (var candidate in Enum.GetValues<YearEnum>())
		{
			if (seen)
			{
				return candidate;
			}

			seen = candidate == year;
		}

		return null;
	}
}
