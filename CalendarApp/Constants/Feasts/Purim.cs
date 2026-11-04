using CalendarApp.Enums;

namespace CalendarApp.Constants.Feasts;

public static class Purim
{
	private static readonly DateOnly Year1 = DateOnly.MinValue; //DateOnly.Parse("2026-03-03");
  private static readonly DateOnly Year2 = DateOnly.Parse("2027-03-23");
  private static readonly DateOnly Year3 = DateOnly.Parse("2028-03-12");
  private static readonly DateOnly Year4 = DateOnly.Parse("2029-03-01");
  private static readonly DateOnly Year5 = DateOnly.Parse("2030-03-19");
  private static readonly DateOnly Year6 = DateOnly.Parse("2031-03-09");

	public static readonly Dictionary<int, DateOnly> Dates = new()
	{
		[YearHelper.HebrewYears[0]] = Year1,
		[YearHelper.HebrewYears[1]] = Year2,
		[YearHelper.HebrewYears[2]] = Year3,
		[YearHelper.HebrewYears[3]] = Year4,
		[YearHelper.HebrewYears[4]] = Year5,
		[YearHelper.HebrewYears[5]] = Year6
	};
}
