using System.Globalization;
using CalendarApp.Constants;
using CalendarApp.Enums;
using CalendarApp.Models;

namespace CalendarApp.Services;

public static class ConstantCalendar
{
    private static readonly CultureInfo Invariant = CultureInfo.InvariantCulture;

    public static IReadOnlyList<CalendarGridRow> Build()
    {
        var buckets = new Dictionary<DateOnly, DateBucket>();
        var monthAnchors = new List<MonthAnchor>();

        foreach (var year in Enum.GetValues<YearEnum>())
        {
            foreach (var feast in FeastDay.List)
            {
                var date = FeastDate(feast, year);
                if (date == DateOnly.MinValue)
                {
                    continue;
                }

                Bucket(buckets, date, year).Feasts.Add(feast);
            }

            foreach (var moon in Moon.List)
            {
                var date = MoonDate(moon, year);
                if (date == DateOnly.MinValue)
                {
                    continue;
                }

                Bucket(buckets, date, year).IsNewMoon = true;
                monthAnchors.Add(new MonthAnchor(date, moon));
            }

            foreach (var count in DayCount.List)
            {
                var start = ParentDate(count, year);
                if (start == DateOnly.MinValue)
                {
                    continue;
                }

                for (var number = 1; number <= count.TotalDays; number++)
                {
                    var date = start.AddDays(number - 1);
                    Bucket(buckets, date, year).Counts.Add((count, number));
                }
            }
        }

        monthAnchors.Sort((left, right) => left.Date.CompareTo(right.Date));

        return buckets
            .OrderBy(pair => pair.Key)
            .Select(pair => ToRow(pair.Key, pair.Value, monthAnchors))
            .ToList();
    }

    private static CalendarGridRow ToRow(DateOnly date, DateBucket bucket, List<MonthAnchor> monthAnchors)
    {
        var anchor = LatestAnchor(monthAnchors, date);
        return new CalendarGridRow
        {
            Year = bucket.Year,
            Month = anchor is null ? "" : $"{anchor.Moon.Name} - {anchor.Moon.Value}",
            GregorianDate = date,
            DateText = date.ToString("yyyy-MMM-dd ddd", Invariant),
            Day = anchor is null ? null : date.DayNumber - anchor.Date.DayNumber + 1,
            MoonPhaseId = bucket.IsNewMoon ? MoonPhase.New.Value : MoonPhase.None.Value,
            Counts = bucket.Counts
                .OrderBy(count => count.Count.Value)
                .Select(count => new CountBadge(
                    count.Count.Title,
                    $"{count.Number}/{count.Count.TotalDays}",
                    count.Count.BadgeCss))
                .ToList(),
            Events = BuildEvents(bucket)
        };
    }

    private static List<EventBadge> BuildEvents(DateBucket bucket)
    {
        var counts = bucket.Counts
            .OrderBy(count => count.Count.Value)
            .Select(count => count.Count)
            .ToList();
        var events = new List<EventBadge>();
        var seen = new HashSet<string>(StringComparer.Ordinal);

        foreach (var feast in bucket.Feasts.OrderBy(feast => feast.Value))
        {
            if (seen.Add(feast.Title))
            {
                events.Add(new EventBadge(feast.Title, BadgeCssFor(feast, counts)));
            }
        }

        foreach (var count in counts)
        {
            if (seen.Add(count.Title))
            {
                events.Add(new EventBadge(count.Title, count.BadgeCss));
            }
        }

        if (bucket.IsNewMoon && seen.Add("Rosh Kodesh"))
        {
            events.Add(new EventBadge("Rosh Kodesh", "badge bg-dark text-white"));
        }

        return events;
    }

    private static string BadgeCssFor(FeastDay feast, List<DayCount> counts)
    {
        var parented = DayCount.List.FirstOrDefault(count =>
            count.Parent.EnumType == ParentTypeEnum.FeastDay && count.Parent.Id == feast.Value);
        if (parented is not null)
        {
            return parented.BadgeCss;
        }

        return counts.Count == 1 ? counts[0].BadgeCss : "badge bg-secondary text-black";
    }

    private static MonthAnchor? LatestAnchor(List<MonthAnchor> monthAnchors, DateOnly date)
    {
        MonthAnchor? latest = null;
        foreach (var anchor in monthAnchors)
        {
            if (anchor.Date > date)
            {
                break;
            }

            latest = anchor;
        }

        return latest;
    }

    private static DateBucket Bucket(Dictionary<DateOnly, DateBucket> buckets, DateOnly date, YearEnum year)
    {
        if (!buckets.TryGetValue(date, out var bucket))
        {
            bucket = new DateBucket { Year = (int)year };
            buckets.Add(date, bucket);
        }

        return bucket;
    }

    private static DateOnly ParentDate(DayCount count, YearEnum year)
    {
        var parent = count.Parent;
        return parent.EnumType switch
        {
            ParentTypeEnum.FeastDay => FeastDate(FeastDay.FromValue(parent.Id), year),
            ParentTypeEnum.Month => MoonDate(Moon.FromValue(parent.Id), year),
            _ => DateOnly.MinValue
        };
    }

    private static DateOnly FeastDate(FeastDay feast, YearEnum year) => feast.Name switch
    {
        "Purim" => DateFor(Purim.Dates, year),
        "LambSelected" => DateFor(LambSelected.Dates, year),
        "Passover" => DateFor(Passover.Dates, year),
        "Omer" => DateFor(Omer.Dates, year),
        "Weeks" => DateFor(Weeks.Dates, year),
        "Trumpets" => DateFor(Trumpets.Dates, year),
        "YomKippur" => DateFor(YomKippur.Dates, year),
        "Sukkot" => DateFor(Sukkot.Dates, year),
        "Hanukkah" => DateFor(Hanukkah.Dates, year),
        _ => throw new InvalidOperationException($"No constant dates for feast {feast.Name}.")
    };

    private static DateOnly MoonDate(Moon moon, YearEnum year) => moon.Name switch
    {
        "Nissan" => DateFor(Nissan.Dates, year),
        "Iyar" => DateFor(Iyar.Dates, year),
        "Sivan" => DateFor(Sivan.Dates, year),
        "Tammuz" => DateFor(Tammuz.Dates, year),
        "Av" => DateFor(Av.Dates, year),
        "Elul" => DateFor(Elul.Dates, year),
        "Tishri" => DateFor(Tishri.Dates, year),
        "Heshvan" => DateFor(Heshvan.Dates, year),
        "Kislev" => DateFor(Kislev.Dates, year),
        "Tevet" => DateFor(Tevet.Dates, year),
        "Shevat" => DateFor(Shevat.Dates, year),
        "Adar" => DateFor(Adar.Dates, year),
        "Adar2" => DateFor(Adar2.Dates, year),
        _ => throw new InvalidOperationException($"No constant dates for moon {moon.Name}.")
    };

    private static DateOnly DateFor(IReadOnlyDictionary<YearEnum, DateOnly> dates, YearEnum year) =>
        dates.TryGetValue(year, out var date) ? date : DateOnly.MinValue;

    private sealed class DateBucket
    {
        public int Year { get; init; }

        public List<FeastDay> Feasts { get; } = [];

        public List<(DayCount Count, int Number)> Counts { get; } = [];

        public bool IsNewMoon { get; set; }
    }

    private sealed record MonthAnchor(DateOnly Date, Moon Moon);
}
