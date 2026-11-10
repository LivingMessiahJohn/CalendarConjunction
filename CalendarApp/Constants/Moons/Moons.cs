using CalendarApp.Enums;

namespace CalendarApp.Constants.Moons;

public static class Nissan // Month 1
{
	private static readonly DateOnly Year1 = DateOnly.MinValue;            // Day of...
	private static readonly DateOnly Year2 = DateOnly.Parse("2027-04-08"); // updated to Phx TimeZone
	private static readonly DateOnly Year3 = DateOnly.Parse("2028-03-27");
	private static readonly DateOnly Year4 = DateOnly.Parse("2029-04-15");
	private static readonly DateOnly Year5 = DateOnly.Parse("2030-04-04");
	private static readonly DateOnly Year6 = DateOnly.Parse("2031-03-24");
	private static readonly DateOnly Year7 = DateOnly.Parse("2032-04-11");
	private static readonly DateOnly Year8 = DateOnly.Parse("2033-04-01");

	public static readonly Dictionary<int, DateOnly> Dates = new()
	{
		[YearHelper.HebrewYears[0]] = Year1,
		[YearHelper.HebrewYears[1]] = Year2,
		[YearHelper.HebrewYears[2]] = Year3,
		[YearHelper.HebrewYears[3]] = Year4,
		[YearHelper.HebrewYears[4]] = Year5,
		[YearHelper.HebrewYears[5]] = Year6,
		[YearHelper.HebrewYears[6]] = Year7,
		[YearHelper.HebrewYears[7]] = Year8
	};
}

public static class Iyar // Month 2
{
	private static readonly DateOnly Year1 = DateOnly.MinValue;
	private static readonly DateOnly Year2 = DateOnly.Parse("2027-05-07");
	private static readonly DateOnly Year3 = DateOnly.Parse("2028-04-26");
	private static readonly DateOnly Year4 = DateOnly.Parse("2029-05-14");
	private static readonly DateOnly Year5 = DateOnly.Parse("2030-05-04");
	private static readonly DateOnly Year6 = DateOnly.Parse("2031-04-23");
	private static readonly DateOnly Year7 = DateOnly.Parse("2032-05-10");
	private static readonly DateOnly Year8 = DateOnly.Parse("2033-04-30");

	public static readonly Dictionary<int, DateOnly> Dates = new()
	{
		[YearHelper.HebrewYears[0]] = Year1,
		[YearHelper.HebrewYears[1]] = Year2,
		[YearHelper.HebrewYears[2]] = Year3,
		[YearHelper.HebrewYears[3]] = Year4,
		[YearHelper.HebrewYears[4]] = Year5,
		[YearHelper.HebrewYears[5]] = Year6,
		[YearHelper.HebrewYears[6]] = Year7,
		[YearHelper.HebrewYears[7]] = Year8
	};
}

public static class Sivan // Month 3
{
	private static readonly DateOnly Year1 = DateOnly.MinValue;
	private static readonly DateOnly Year2 = DateOnly.Parse("2027-06-06");
	private static readonly DateOnly Year3 = DateOnly.Parse("2028-05-25");
	private static readonly DateOnly Year4 = DateOnly.Parse("2029-06-13");
	private static readonly DateOnly Year5 = DateOnly.Parse("2030-06-02");
	private static readonly DateOnly Year6 = DateOnly.Parse("2031-05-22");
	private static readonly DateOnly Year7 = DateOnly.Parse("2032-06-09");

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

public static class Tammuz // Month 4
{
	private static readonly DateOnly Year1 = DateOnly.MinValue;
	private static readonly DateOnly Year2 = DateOnly.Parse("2027-07-05");
	private static readonly DateOnly Year3 = DateOnly.Parse("2028-06-24");
	private static readonly DateOnly Year4 = DateOnly.Parse("2029-07-13");
	private static readonly DateOnly Year5 = DateOnly.Parse("2030-07-02");
	private static readonly DateOnly Year6 = DateOnly.Parse("2031-06-21");
	private static readonly DateOnly Year7 = DateOnly.Parse("2032-07-09");

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

public static class Av // Month 5
{
	private static readonly DateOnly Year1 = DateOnly.MinValue;
	private static readonly DateOnly Year2 = DateOnly.Parse("2027-08-03");
	private static readonly DateOnly Year3 = DateOnly.Parse("2028-07-23");
	private static readonly DateOnly Year4 = DateOnly.Parse("2029-08-11");
	private static readonly DateOnly Year5 = DateOnly.Parse("2030-07-31");
	private static readonly DateOnly Year6 = DateOnly.Parse("2031-07-20");
	private static readonly DateOnly Year7 = DateOnly.Parse("2032-08-07");

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

public static class Elul // Month 6
{
	private static readonly DateOnly Year1 = DateOnly.MinValue;
	private static readonly DateOnly Year2 = DateOnly.Parse("2027-09-02"); // updated to Phx TimeZone
	private static readonly DateOnly Year3 = DateOnly.Parse("2028-08-21");
	private static readonly DateOnly Year4 = DateOnly.Parse("2029-09-09"); // 08 OR 09 Sept 2029 anomaly
	private static readonly DateOnly Year5 = DateOnly.Parse("2030-08-30");
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

public static class Heshvan // Month 8
{
	private static readonly DateOnly Year1 = DateOnly.MinValue;
	private static readonly DateOnly Year2 = DateOnly.Parse("2027-10-31"); // updated to Phx TimeZone
	private static readonly DateOnly Year3 = DateOnly.Parse("2028-10-19");
	private static readonly DateOnly Year4 = DateOnly.Parse("2029-11-07");
	private static readonly DateOnly Year5 = DateOnly.Parse("2030-10-28");
	private static readonly DateOnly Year6 = DateOnly.Parse("2031-10-17");
	private static readonly DateOnly Year7 = DateOnly.Parse("2032-11-04");

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

public static class Kislev // Month 9
{
	private static readonly DateOnly Year1 = DateOnly.MinValue;
	private static readonly DateOnly Year2 = DateOnly.Parse("2027-11-29"); // updated to Phx TimeZone
	private static readonly DateOnly Year3 = DateOnly.Parse("2028-11-18");
	private static readonly DateOnly Year4 = DateOnly.Parse("2029-12-07");
	private static readonly DateOnly Year5 = DateOnly.Parse("2030-11-26");
	private static readonly DateOnly Year6 = DateOnly.Parse("2031-11-16");
	private static readonly DateOnly Year7 = DateOnly.Parse("2032-12-04");

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

public static class Tevet // Month 10
{
	private static readonly DateOnly Year1 = DateOnly.Parse("2026-12-10");
	private static readonly DateOnly Year2 = DateOnly.Parse("2027-12-29"); // updated to Phx TimeZone
	private static readonly DateOnly Year3 = DateOnly.Parse("2028-12-17");
	private static readonly DateOnly Year4 = DateOnly.Parse("2030-01-05");
	private static readonly DateOnly Year5 = DateOnly.Parse("2030-12-26");
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

public static class Shevat // Month 11
{
	private static readonly DateOnly Year1 = DateOnly.Parse("2027-01-09");
	private static readonly DateOnly Year2 = DateOnly.Parse("2028-01-28"); // updated to Phx TimeZone
	private static readonly DateOnly Year3 = DateOnly.Parse("2029-01-16");
	private static readonly DateOnly Year4 = DateOnly.Parse("2030-02-04");
	private static readonly DateOnly Year5 = DateOnly.Parse("2031-01-24");
	private static readonly DateOnly Year6 = DateOnly.Parse("2032-01-14");
	private static readonly DateOnly Year7 = DateOnly.Parse("2033-02-01");

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

public static class Adar2 // Month 13
{
	private static readonly DateOnly Year1 = DateOnly.Parse("2027-03-09");
	private static readonly DateOnly Year2 = DateOnly.MinValue;
	private static readonly DateOnly Year3 = DateOnly.Parse("2029-03-16");
	private static readonly DateOnly Year4 = DateOnly.MinValue;
	private static readonly DateOnly Year5 = DateOnly.MinValue;
	private static readonly DateOnly Year6 = DateOnly.MinValue;
	private static readonly DateOnly Year7 = DateOnly.Parse("2032-03-12");

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
