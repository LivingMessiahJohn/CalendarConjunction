namespace CalendarApp.Constants.Feasts;

// Relevant FlattenByDates rows only. DateOnly.MinValue means that year has no row.
// ToDo: consider changing to Yom Teruah
public static class Trumpets
{
	private static readonly DateOnly Year1 = DateOnly.MinValue;
	private static readonly DateOnly Year2 = DateOnly.Parse("2027-10-01");
	private static readonly DateOnly Year3 = DateOnly.Parse("2028-09-20");
	private static readonly DateOnly Year4 = DateOnly.Parse("2029-10-09");
	private static readonly DateOnly Year5 = DateOnly.Parse("2030-09-28");
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
