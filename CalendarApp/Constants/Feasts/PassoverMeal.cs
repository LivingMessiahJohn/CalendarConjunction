namespace CalendarApp.Constants.Feasts;

public static class PassoverMeal
{
	private static readonly DateOnly Year1 = DateOnly.MinValue;
	private static readonly DateOnly Year2 = DateOnly.Parse("2027-04-20");
	private static readonly DateOnly Year3 = DateOnly.Parse("2028-04-08");
	private static readonly DateOnly Year4 = DateOnly.Parse("2029-04-27");
	private static readonly DateOnly Year5 = DateOnly.Parse("2030-04-16");
	private static readonly DateOnly Year6 = DateOnly.Parse("2031-04-05"); // 08

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
