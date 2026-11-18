using CalendarApp.Models;
using CalendarApp.Services;
namespace CalendarApp.Features.Home;

//private readonly List<MonthSpan> spans = [];



public class CalendarHelper
{
	//private
	readonly List<MonthSpan> spans = [];
	
	public (string Name, int Day)? HebrewFor(DateOnly date)
	{

		spans.Clear();
		spans.AddRange(ConstantCalendar.MonthSpans());

		foreach (var span in spans)
		{
			if (date >= span.Date && date < span.NextDate)
			{
				return (span.Month.Name, date.DayNumber - span.Date.DayNumber + 1);
			}
		}

		if (spans.Count == 0)
		{
			return null;
		}

		var last = spans[^1];
		var offset = date.DayNumber - last.NextDate.DayNumber;
		if (offset >= 0 && offset < 30)
		{
			return (last.NextMonth.Name, offset + 1);
		}

		return null;
	}

}
