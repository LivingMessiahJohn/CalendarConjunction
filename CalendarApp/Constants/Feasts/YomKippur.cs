namespace CalendarApp.Constants.Feasts;

// Relevant FlattenByDates rows only. DateOnly.MinValue means that year has no row.
public static class YomKippur
{
	private static readonly DateOnly Year1 = DateOnly.MinValue;
	private static readonly DateOnly Year2 = DateOnly.Parse("2027-10-10");
	private static readonly DateOnly Year3 = DateOnly.Parse("2028-09-29");
	private static readonly DateOnly Year4 = DateOnly.Parse("2029-10-18");
	private static readonly DateOnly Year5 = DateOnly.Parse("2030-10-07");
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
