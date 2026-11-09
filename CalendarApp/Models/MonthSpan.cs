using System.Globalization;
using CalendarApp.Enums;

namespace CalendarApp.Models;

public sealed record MonthSpan(int Year, Moon Month, DateOnly Date, Moon NextMonth, DateOnly NextDate)
{
	private static readonly CultureInfo Invariant = CultureInfo.InvariantCulture;
	public int DaysDifferent => NextDate.DayNumber - Date.DayNumber;
	public string MonthDate => Format(Month, Date);
	public string NextMonthDate => Format(NextMonth, NextDate);
	private static string Format(Moon moon, DateOnly date) =>
			$"{moon.Name} - {moon.Value} {date.ToString("MM/dd/yyyy", Invariant)}";
}
