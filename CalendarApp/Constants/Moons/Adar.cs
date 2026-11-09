using CalendarApp.Enums;

namespace CalendarApp.Constants.Moons;

public static class Adar // Month 12
{
	private static readonly DateOnly Year1 = DateOnly.Parse("2027-02-08");
	private static readonly DateOnly Year2 = DateOnly.Parse("2028-02-26"); // updated to Phx TimeZone
	private static readonly DateOnly Year3 = DateOnly.Parse("2029-02-14");
	private static readonly DateOnly Year4 = DateOnly.Parse("2030-03-05");
	private static readonly DateOnly Year5 = DateOnly.Parse("2031-02-23");
	private static readonly DateOnly Year6 = DateOnly.Parse("2032-02-12");
	private static readonly DateOnly Year7 = DateOnly.Parse("2033-03-02");
	//private static readonly DateOnly Year8 = DateOnly.Parse("2033-03-31");

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
