using Ardalis.SmartEnum;
using CalendarApp.Constants;
using CalendarApp.Constants.Moons;
using AnchorHanukkah = CalendarApp.Constants.Feasts.Hanukkah;
using AnchorPurim = CalendarApp.Constants.Feasts.Purim;

namespace CalendarApp.Enums;

public static class FeastGroupBadgeColor  // GroupColor
{
	public sealed record BadgePair(string Icon, string Title);

	private static readonly Lazy<Dictionary<string, BadgePair>> ByTitle = new(BuildTitles);

	public static BadgePair? ForTitle(string title) =>
		ByTitle.Value.TryGetValue(title, out var pair) ? pair : null;

	private static Dictionary<string, BadgePair> BuildTitles() =>
		new(StringComparer.Ordinal)
		{
			[FeastDay.Hanukkah.Title] = Hanukkah,
			[FeastDay.Matzot.Title] = Matzot,
			[FeastDay.Shavuot.Title] = Shavuot,
			[FeastDay.YomTeruah.Title] = YomTeruah,
			[FeastDay.YomKippur.Title] = YomKippur,
			[FeastDay.Sukkot.Title] = Sukkot,
			[EventFlag.RoshKodesh.Title] = RoshKodesh,
		};

	private static string Filled(string background, string? ink = null) =>
		ink is null ? $"badge bg-{background}" : $"badge bg-{background} {ink}";

	private static string Outline(string background, string ink, string? border = null) =>
		$"border border-{border ?? background} border-2 border-sm-4 badge bg-{background} bg-transparent {ink}";

	private static string Subtle(string background, string ink = "text-black-50") =>
		$"badge bg-{background}-subtle {ink}";

	// Winter Season
	public static readonly BadgePair Hanukkah = new(Filled("info", "text-black"), Outline("info", "text-black"));
	public static readonly string MenorahCount = Subtle("info");
	public static readonly string Purim = Filled("danger");

	// Passover Season
	public static readonly BadgePair Matzot = new(Filled("warning", "text-black"), Outline("warning", "text-black"));
	public static readonly string UnleavenedBreadCount = Subtle("warning", "text-black");

	public static readonly BadgePair Shavuot = new(Filled("primary", "text-white"), Outline("primary", "text-dark"));
	public static readonly string Omer = Subtle("primary");

	// Fall Season
	public static readonly BadgePair YomTeruah = new(Filled("info", "text-black"), Outline("info", "text-black"));
	public static readonly BadgePair YomKippur = new(Filled("danger", "text-white"), Outline("danger", "text-black"));
	public static readonly BadgePair Sukkot = new(Filled("success", "text-white"), Outline("success", "text-black"));
	public static readonly string SukkotCount = Subtle("success");

	// DaysOf
	public static readonly string Repentance = Subtle("info");
	public static readonly string Awe = Subtle("primary");

	// Moon Season
	public static readonly BadgePair RoshKodesh = new(Filled("dark", "text-white"), Outline("dark", "text-black", "black"));
}


public abstract class FeastDay : SmartEnum<FeastDay>
{
	#region Id's
	private static class Id
	{
		internal const int Hanukkah = 1;
		internal const int Purim = 2;
		internal const int LambSelected = 3;
		internal const int Seder = 4; 
		internal const int Matzot = 5;
		internal const int Omer = 6;
		internal const int Shavuot = 7;
		internal const int YomTeruah = 8;
		internal const int YomKippur = 9;
		internal const int Sukkot = 10;
	}
	#endregion

	#region Declared Public Instances
	public static readonly FeastDay Purim = new PurimSE();
	public static readonly FeastDay LambSelected = new LambSelectedSE();
	public static readonly FeastDay Seder = new SederSE();
	public static readonly FeastDay Matzot = new MatzotSE();
	public static readonly FeastDay Omer = new OmerSE();
	public static readonly FeastDay Shavuot = new ShavuotSE();
	public static readonly FeastDay YomTeruah = new YomTeruahSE();
	public static readonly FeastDay YomKippur = new YomKippurSE();
	public static readonly FeastDay Sukkot = new SukkotSE();
	public static readonly FeastDay Hanukkah = new HanukkahSE();
	// Note; SE=SmartEnum
	#endregion

	private FeastDay(string name, int value) : base(name, value) { }

	#region Extra Fields
	public abstract string Hebrew { get; }
	public abstract string Details { get; }
	public abstract string Title { get; }
	public abstract string Icon { get; }
	public abstract string BadgeCss { get; }
	public abstract bool HasCounting { get; }
	public abstract FeastGroup Group { get; }
	public abstract int AddDays { get; }
	//public abstract IReadOnlyDictionary<int, DateOnly> AnchorDatesDELETE { get; }
	public abstract IReadOnlyDictionary<int, DateOnly> AnchorDates { get; }
	#endregion


	#region Extra Properties

	public DateOnly DateFor(int year)
	{
		if (!AnchorDates.TryGetValue(year, out var anchor) || anchor == DateOnly.MinValue)
		{
			return DateOnly.MinValue;
		}

		return anchor.AddDays(AddDays);
	}

	#endregion

	#region Private Instantiation

	private sealed class PurimSE : FeastDay
	{
		public PurimSE() : base($"{nameof(Id.Purim)}", Id.Purim) { }
		public override string Hebrew => "פוּרִים";
		public override string Details => "Tradition is to read the book of Esther; date determined by Rabbinic sources";
		public override string Title => nameof(Id.Purim);
		public override string Icon => "fas fa-dice";  // far fa-square
		public override string BadgeCss => FeastGroupBadgeColor.Purim;
		public override bool HasCounting => false;
		public override FeastGroup Group => FeastGroup.Winter;
		public override int AddDays => 0;
		public override IReadOnlyDictionary<int, DateOnly> AnchorDates => AnchorPurim.Dates;
	}

	private sealed class LambSelectedSE : FeastDay
	{
		public LambSelectedSE() : base($"{nameof(Id.LambSelected)}", Id.LambSelected) { }
		public override string Hebrew => "פֶּסַח";
		public override string Details => "Select on the 10th of Aviv (Nisan)";
		public override string Title => "Lamb selected";
		public override string Icon => "fa-regular fa-hand-point-right"; // fa-solid fa-sheep
		public override string BadgeCss => FeastGroupBadgeColor.Matzot.Title;
		public override bool HasCounting => true;
		public override FeastGroup Group => FeastGroup.PassoverSeason;
		public override int AddDays => PassoverSeasonAddDays.LambSelected; // 10;
		public override IReadOnlyDictionary<int, DateOnly> AnchorDates => Nisan.Dates;
	}

	private class PassoverSeasonAddDays
	{
		public const int LambSelected = 10;
		public const int Seder = 13;
		public const int MatzotFirstDay = 14;
		public const int OmerCountStart = 15;
		public const int Shavuot = OmerCountStart + 49;
	}

	private sealed class SederSE : FeastDay
	{
		public SederSE() : base($"{nameof(Id.Seder)}", Id.Seder) { }
		public override string Hebrew => "פֶּסַח";
		public override string Details => "The Seder Meal is prepared on the 14th of Aviv. As evening starts, the meal is eaten"; //. Also, this becomes the first day of Unleavened bread
		public override string Title => "Seder meal";
		public override string Icon => "fa-solid fa-utensils ";
		public override string BadgeCss => FeastGroupBadgeColor.Matzot.Title;
		public override bool HasCounting => false;
		public override FeastGroup Group => FeastGroup.PassoverSeason;
		public override int AddDays => PassoverSeasonAddDays.Seder; 
		public override IReadOnlyDictionary<int, DateOnly> AnchorDates => Nisan.Dates;
	}

	private sealed class MatzotSE : FeastDay
	{
		public MatzotSE() : base($"{nameof(Id.Matzot)}", Id.Matzot) { }
		public override string Hebrew => "פֶּסַח";
		public override string Details => "Feast of leavened bread; days 1 and 7 are high sabbaths";
		public override string Title => nameof(Id.Matzot);
		public override string Icon => "fa-solid fa-bread-slice";  // fas fa-door-openfa-times-circle 
		public override string BadgeCss => FeastGroupBadgeColor.Matzot.Title;
		public override bool HasCounting => true;
		public override FeastGroup Group => FeastGroup.PassoverSeason;
		public override int AddDays => PassoverSeasonAddDays.MatzotFirstDay; 
		public override IReadOnlyDictionary<int, DateOnly> AnchorDates => Nisan.Dates;
	}

	private sealed class OmerSE : FeastDay
	{
		public OmerSE() : base($"{nameof(Id.Omer)}", Id.Omer) { }
		public override string Hebrew => "סְפִירַת הָעוֹמֶר"; // Sefirat HaOmer
		public override string Details => "the counting of the Omer";
		public override string Title => nameof(Id.Omer);
		public override string Icon => "fa-solid fa-wheat-awn"; //fa-solid fa-list-ol
		public override string BadgeCss => FeastGroupBadgeColor.Omer;
		public override bool HasCounting => true;
		public override FeastGroup Group => FeastGroup.OmerAndShavuot;
		public override int AddDays => PassoverSeasonAddDays.OmerCountStart;
		public override IReadOnlyDictionary<int, DateOnly> AnchorDates => Nisan.Dates;
	}

	private sealed class ShavuotSE : FeastDay
	{
		public ShavuotSE() : base($"{nameof(Id.Shavuot)}", Id.Shavuot) { }
		public override string Hebrew => "שָׁבוּעוֹת";
		public override string Details => "The high sabbath begins the evening before; This is also called Pentecost";
		public override string Title => nameof(Id.Shavuot);
		public override string Icon => "fab fa-creative-commons-zero";
		public override string BadgeCss => FeastGroupBadgeColor.Shavuot.Title;
		public override bool HasCounting => false;
		public override FeastGroup Group => FeastGroup.OmerAndShavuot;

		public override int AddDays => PassoverSeasonAddDays.Shavuot;  // 14 (Matzot) + 2 + 49
		public override IReadOnlyDictionary<int, DateOnly> AnchorDates => Nisan.Dates;
	}

	private sealed class YomTeruahSE : FeastDay
	{
		public YomTeruahSE() : base($"{nameof(Id.YomTeruah)}", Id.YomTeruah) { }
		public override string Hebrew => "יוֹם תְּרוּעָה";
		public override string Details => "Day of Trumpets, a high sabbath";
		public override string Title => "Yom Teruah"; 
		public override string Icon => "fas fa-bullhorn";
		public override string BadgeCss => FeastGroupBadgeColor.YomTeruah.Title;
		public override bool HasCounting => false;
		public override FeastGroup Group => FeastGroup.Fall;
		public override int AddDays => 0;
		public override IReadOnlyDictionary<int, DateOnly> AnchorDates => Tishri.Dates;
	}

	private sealed class YomKippurSE : FeastDay
	{
		public YomKippurSE() : base($"{nameof(Id.YomKippur)}", Id.YomKippur) { }
		public override string Hebrew => "יוֹם כִּיפּוּר";
		public override string Details => "Day of Atonements; a high sabbath";  // In the afternoon we have our Yom Kippur service and break the fast after the sun sets
		public override string Title => "Yom Kippur";
		public override string Icon => "fas fa-hands-helping";
		public override string BadgeCss => FeastGroupBadgeColor.YomKippur.Title;
		public override bool HasCounting => false;
		public override FeastGroup Group => FeastGroup.Fall;
		public override int AddDays => 10;
		public override IReadOnlyDictionary<int, DateOnly> AnchorDates => Tishri.Dates;
	}

	private sealed class SukkotSE : FeastDay
	{
		public SukkotSE() : base($"{nameof(Id.Sukkot)}", Id.Sukkot) { }
		public override string Hebrew => "סֻּכּוֹת";
		public override string Details => "Feast of tabernacles; days 1 and 8 are high sabbaths";  // Preparation Day, High Sabbath begins at sunset
		public override string Title => nameof(Id.Sukkot);
		public override string Icon => "fas fa-campground";
		public override string BadgeCss => FeastGroupBadgeColor.Sukkot.Title;
		public override bool HasCounting => true;
		public override FeastGroup Group => FeastGroup.Fall;
		public override int AddDays => 15;
		public override IReadOnlyDictionary<int, DateOnly> AnchorDates => Tishri.Dates;
	}

	private sealed class HanukkahSE : FeastDay
	{
		public HanukkahSE() : base($"{nameof(Id.Hanukkah)}", Id.Hanukkah) { }
		public override string Hebrew => "חֲנֻכָּה";
		public override string Details => "First day of Hanukkah; Date determined by Rabbinic sources";
		public override string Title => nameof(Id.Hanukkah);
		public override string Icon => "fas fa-hanukiah";
		public override string BadgeCss => FeastGroupBadgeColor.Hanukkah.Title;
		public override bool HasCounting => true;
		public override FeastGroup Group => FeastGroup.Winter;
		public override int AddDays => 0;
		public override IReadOnlyDictionary<int, DateOnly> AnchorDates => AnchorHanukkah.Dates;
	}

	#endregion

}

// Ignore Spelling: Yom Kippur, Kodesh