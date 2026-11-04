using CalendarApp.Enums;

namespace CalendarApp.Constants.Moons;

public static class Elul // Month 6
{
	private static readonly DateOnly Year1 = DateOnly.MinValue;
	private static readonly DateOnly Year2 = DateOnly.Parse("2027-09-01");
	private static readonly DateOnly Year3 = DateOnly.Parse("2028-08-20");
	private static readonly DateOnly Year4 = DateOnly.Parse("2029-09-08");
	private static readonly DateOnly Year5 = DateOnly.Parse("2030-08-29");
	private static readonly DateOnly Year6 = DateOnly.Parse("2031-08-19"); 
	private static readonly DateOnly Year7 = DateOnly.Parse("2032-09-06");

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
