using CalendarApp.Enums;

namespace CalendarApp.Constants.Feasts;

public static class Hanukkah
{
	private static readonly DateOnly Year1 = DateOnly.Parse("2026-12-05");
	private static readonly DateOnly Year2 = DateOnly.Parse("2027-12-25");
	private static readonly DateOnly Year3 = DateOnly.Parse("2028-12-13");
	private static readonly DateOnly Year4 = DateOnly.Parse("2029-12-02");
	private static readonly DateOnly Year5 = DateOnly.Parse("2030-12-21");
	private static readonly DateOnly Year6 = DateOnly.Parse("2031-12-10");
	private static readonly DateOnly Year7 = DateOnly.Parse("2032-11-27"); 

	public static readonly Dictionary<int, DateOnly> Dates = new()
	{
		[YearHelper.HebrewYears[0]] = Year1,
		[YearHelper.HebrewYears[1]] = Year2,
		[YearHelper.HebrewYears[2]] = Year3,
		[YearHelper.HebrewYears[3]] = Year4,
		[YearHelper.HebrewYears[4]] = Year5,
		[YearHelper.HebrewYears[5]] = Year6,
		[YearHelper.HebrewYears[6]] = Year7
	};

}