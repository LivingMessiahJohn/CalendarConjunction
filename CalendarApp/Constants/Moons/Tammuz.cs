namespace CalendarApp.Constants.Moons;

// Relevant FlattenByDates rows only. DateOnly.MinValue means that year has no row.
public static class Tammuz
{
	private static readonly DateOnly Year1 = DateOnly.MinValue;
	private static readonly DateOnly Year2 = DateOnly.Parse("2027-07-04");
	private static readonly DateOnly Year3 = DateOnly.Parse("2028-06-23");
	private static readonly DateOnly Year4 = DateOnly.Parse("2029-07-12");
	private static readonly DateOnly Year5 = DateOnly.Parse("2030-07-01");
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
