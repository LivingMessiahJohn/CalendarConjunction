namespace CalendarApp.Enums;

public static class YearHelper
{
	public const int Default = 5863;

	public static readonly List<int> HebrewYears =
	[
		5862,
		5863,
		5864,
		5865,
		5866,
		5867,
		5868,
		5869
	];

	public static int Min => HebrewYears[0];
	public static int Max => HebrewYears[^1]; // The ^ operator counts backward from the end; i.e. HebrewYears[HebrewYears.Count - 1].
	public static string Range => $"{Min}-{Max}";

	private const int Offset = 3836;
	public static int GetGregorianYear(int hebrewYear) { return hebrewYear - Offset; }

}