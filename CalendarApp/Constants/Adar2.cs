namespace CalendarApp.Constants;

// Relevant FlattenByDates rows only. DateOnly.MinValue means that year has no row.
public static class Adar2
{
	private static readonly DateOnly Year1 = DateOnly.Parse("2027-03-08");
	private static readonly DateOnly Year2 = DateOnly.MinValue;
	private static readonly DateOnly Year3 = DateOnly.Parse("2029-03-15");
	private static readonly DateOnly Year4 = DateOnly.MinValue;
	private static readonly DateOnly Year5 = DateOnly.MinValue;
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
