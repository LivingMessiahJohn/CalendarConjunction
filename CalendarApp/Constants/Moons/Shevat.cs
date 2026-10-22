using CalendarApp.Constants.Feasts;

namespace CalendarApp.Constants.Moons;

// Relevant FlattenByDates rows only. DateOnly.MinValue means that year has no row.
public static class Shevat
{
	private static readonly DateOnly Year1 = DateOnly.Parse("2027-01-08");
	private static readonly DateOnly Year2 = DateOnly.Parse("2028-01-27");
	private static readonly DateOnly Year3 = DateOnly.Parse("2029-01-15");
	private static readonly DateOnly Year4 = DateOnly.Parse("2030-02-03");
	private static readonly DateOnly Year5 = DateOnly.Parse("2031-01-23");
	private static readonly DateOnly Year6 = DateOnly.MinValue;

	public static readonly Dictionary<YearEnum, DateOnly> Dates = new()
	{
		[YearEnum.Year1] = Year1,
		[YearEnum.Year2] = Year2,
		[YearEnum.Year3] = Year3,
		[YearEnum.Year4] = Year4,
		[YearEnum.Year5] = Year5,
		[YearEnum.Year6] = Year6
	};
}
