using CalendarApp.Enums;

namespace CalendarApp.Constants.Moons;

public static class Tishri // Month 7
{
	private static readonly DateOnly Year1 = DateOnly.MinValue;
	private static readonly DateOnly Year2 = DateOnly.Parse("2027-10-01"); // updated to Phx TimeZone
	private static readonly DateOnly Year3 = DateOnly.Parse("2028-09-20");
	private static readonly DateOnly Year4 = DateOnly.Parse("2029-10-09");
	private static readonly DateOnly Year5 = DateOnly.Parse("2030-09-28");
	private static readonly DateOnly Year6 = DateOnly.Parse("2031-09-18"); 
	private static readonly DateOnly Year7 = DateOnly.Parse("2032-10-05");


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
