namespace CalendarApp.Constants;

public enum YearEnum
{
	All = 0,
	Year1 = 5862, // 2026-12-09 - 2027-04-07
	Year2 = 5863, // 2027-04-14 - 2028-03-26
	Year3 = 5864, // 2028-04-03 - 2029-04-14
	Year4 = 5865, // 2029-04-22 - 2030-04-03
	Year5 = 5866, // 2030-04-11 - 2031-03-23
	Year6 = 5867  // 2031-03-31 - 2031-05-22
}

public static class YearEnumExtensions
{
	public static int GregorianYear(this YearEnum year) => year switch
	{
		YearEnum.Year1 => 2026,
		YearEnum.Year2 => 2027,
		YearEnum.Year3 => 2028,
		YearEnum.Year4 => 2029,
		YearEnum.Year5 => 2030,
		YearEnum.Year6 => 2031,
		_ => throw new ArgumentOutOfRangeException(nameof(year), year, "Only a numbered year has a Gregorian year.")
	};

	public static string Label(this YearEnum year) => $"{(int)year} - {year.GregorianYear()}";
}

//public static class Constant
//{
//	public static readonly IReadOnlyList<int> Years = [5862, 5863, 5864, 5865, 5866, 5867];
//}
