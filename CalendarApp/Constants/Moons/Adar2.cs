using CalendarApp.Enums;

namespace CalendarApp.Constants.Moons;

public static class Adar2 // Month 13
{
	private static readonly DateOnly Year1 = DateOnly.Parse("2027-03-09");
	private static readonly DateOnly Year2 = DateOnly.MinValue;
	private static readonly DateOnly Year3 = DateOnly.Parse("2029-03-16");
	private static readonly DateOnly Year4 = DateOnly.MinValue;
	private static readonly DateOnly Year5 = DateOnly.MinValue;
	private static readonly DateOnly Year6 = DateOnly.MinValue;
	private static readonly DateOnly Year7 = DateOnly.Parse("2032-03-16");

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
