namespace CalendarApp.Constants.Feasts;

// Relevant FlattenByDates rows only. DateOnly.MinValue means that year has no row.
public static class Purim
{
	private static readonly DateOnly Year1 = DateOnly.MinValue; //DateOnly.Parse("2026-03-03");
  private static readonly DateOnly Year2 = DateOnly.Parse("2027-03-23");
  private static readonly DateOnly Year3 = DateOnly.Parse("2028-03-12");
  private static readonly DateOnly Year4 = DateOnly.Parse("2029-03-01");
  private static readonly DateOnly Year5 = DateOnly.Parse("2030-03-19");
  private static readonly DateOnly Year6 = DateOnly.Parse("2031-03-09");

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
