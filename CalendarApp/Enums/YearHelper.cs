namespace CalendarApp.Enums;

public static class YearHelper
{
	public const int Min = 5862;
	public const int Max = 5868;
	public const int Default = 5863;

	public static readonly List<int> HebrewYears =
	[
		5862,
		5863,
		5864,
		5865,
		5866,
		5867,
		5868
	];

	private const int Offset = 3836;
	public static int GetGregorianYear(int hebrewYear) { return hebrewYear - Offset; }

}