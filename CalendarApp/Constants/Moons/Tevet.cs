using CalendarApp.Enums;

namespace CalendarApp.Constants.Moons;

public static class Tevet // Month 10
{
	private static readonly DateOnly Year1 = DateOnly.Parse("2026-12-09");
	private static readonly DateOnly Year2 = DateOnly.Parse("2027-12-28");
	private static readonly DateOnly Year3 = DateOnly.Parse("2028-12-16");
	private static readonly DateOnly Year4 = DateOnly.Parse("2030-01-04");
	private static readonly DateOnly Year5 = DateOnly.Parse("2030-12-25");
	private static readonly DateOnly Year6 = DateOnly.Parse("2031-12-15");
	private static readonly DateOnly Year7 = DateOnly.Parse("2033-01-02");

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
