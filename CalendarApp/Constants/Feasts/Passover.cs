namespace CalendarApp.Constants.Feasts;

// Relevant FlattenByDates rows only. DateOnly.MinValue means that year has no row.
public static class Passover
{
	private static readonly DateOnly Year1 = DateOnly.MinValue;
	private static readonly DateOnly Year2 = DateOnly.Parse("2027-04-21");
	private static readonly DateOnly Year3 = DateOnly.Parse("2028-04-09");
	private static readonly DateOnly Year4 = DateOnly.Parse("2029-04-28");
	private static readonly DateOnly Year5 = DateOnly.Parse("2030-04-17");
	private static readonly DateOnly Year6 = DateOnly.Parse("2031-04-06"); // 08

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
