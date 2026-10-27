using Ardalis.SmartEnum;
using CalendarApp.Constants;
using CalendarApp.Constants.Moons;
using AnchorHanukkah = CalendarApp.Constants.Feasts.Hanukkah;
using AnchorOmer = CalendarApp.Constants.Moons.Omer;
using AnchorPurim = CalendarApp.Constants.Feasts.Purim;

namespace CalendarApp.Enums;

public static class FeastGroupBadgeColor  // GroupColor
{
	// Winter Season
	public const string Hanukkah = "badge bg-info text-black";
	public const string MenorahCount = "badge bg-info-subtle text-black-50";
	public const string Purim = "badge bg-danger";

	// Passover Season
	public const string PassoverSeason = "badge bg-warning text-black"; 
	public const string UnleavenedBreadCount = "badge bg-warning-subtle text-black";

	public const string Shavuot = "badge bg-primary text-black";
	public const string Omer = "badge bg-primary-subtle text-black-50";

	// Fall Season
	public const string YomTeruah = "badge bg-info text-black";
	public const string YomKippur = "badge bg-danger text-black";
	public const string Sukkot = "badge bg-success text-black";
	public const string SukkotCount = "badge bg-success-subtle text-black-50";

	// DaysOf
	public const string Repentance = "badge bg-info-subtle text-black-50";
	public const string Awe = "badge bg-primary-subtle text-black-50";

	// Moon Season
	public const string RoshKodesh = "badge bg-dark text-white"; 


}


public abstract class FeastDay : SmartEnum<FeastDay>
{
	#region Id's
	private static class Id
	{
		internal const int Hanukkah = 1;
		internal const int Purim = 2;
		internal const int LambSelected = 3;
		internal const int PassoverMeal = 4;
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
	public static readonly FeastDay PassoverMeal = new PassoverMealSE();
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
	public abstract IReadOnlyDictionary<YearEnum, DateOnly> AnchorDates { get; }
	#endregion


	#region Extra Properties

	public DateOnly DateFor(YearEnum year)
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
		public override IReadOnlyDictionary<YearEnum, DateOnly> AnchorDates => AnchorPurim.Dates;
	}

	private sealed class LambSelectedSE : FeastDay
	{
		public LambSelectedSE() : base($"{nameof(Id.LambSelected)}", Id.LambSelected) { }
		public override string Hebrew => "פֶּסַח";
		public override string Details => "Select on the 10th of Aviv (Nissan)";
		public override string Title => "Lamb selected";
		public override string Icon => "fa-regular fa-hand-point-right"; // fa-solid fa-sheep
		public override string BadgeCss => FeastGroupBadgeColor.PassoverSeason;
		public override bool HasCounting => true;
		public override FeastGroup Group => FeastGroup.PassoverSeason;
		public override int AddDays => 10;
		public override IReadOnlyDictionary<YearEnum, DateOnly> AnchorDates => Nissan.Dates;
	}

	private sealed class PassoverMealSE : FeastDay
	{
		public PassoverMealSE() : base($"{nameof(Id.PassoverMeal)}", Id.PassoverMeal) { }
		public override string Hebrew => "פֶּסַח";
		public override string Details => "The Seder Meal is prepared on the 14th of Aviv. As evening starts, the meal is eaten"; //. Also, this becomes the first day of Unleavened bread
		public override string Title => "The Seder meal";
		public override string Icon => "fa-solid fa-utensils ";
		public override string BadgeCss => FeastGroupBadgeColor.PassoverSeason;
		public override bool HasCounting => false;
		public override FeastGroup Group => FeastGroup.PassoverSeason;
		public override int AddDays => 13;
		public override IReadOnlyDictionary<YearEnum, DateOnly> AnchorDates => Nissan.Dates;
	}

	// Rename Matzot
	private sealed class MatzotSE : FeastDay
	{
		public MatzotSE() : base($"{nameof(Id.Matzot)}", Id.Matzot) { }
		public override string Hebrew => "פֶּסַח";
		public override string Details => "Feast of leavened bread; days 1 and 7 are high sabbaths";
		public override string Title => nameof(Id.Matzot);
		public override string Icon => "fas fa-door-open";  //fa-times-circle
		public override string BadgeCss => FeastGroupBadgeColor.PassoverSeason;
		public override bool HasCounting => true;
		public override FeastGroup Group => FeastGroup.PassoverSeason;
		public override int AddDays => 14;
		public override IReadOnlyDictionary<YearEnum, DateOnly> AnchorDates => Nissan.Dates;
	}

	private sealed class OmerSE : FeastDay
	{
		public OmerSE() : base($"{nameof(Id.Omer)}", Id.Omer) { }
		public override string Hebrew => "סְפִירַת הָעוֹמֶר"; // Sefirat HaOmer
		public override string Details => "the counting of the Omer";
		public override string Title => nameof(Id.Omer);
		public override string Icon => "fa-solid fa-list-ol";
		public override string BadgeCss => FeastGroupBadgeColor.Omer;
		public override bool HasCounting => true;
		public override FeastGroup Group => FeastGroup.OmerAndShavuot;
		public override int AddDays => 0;
		public override IReadOnlyDictionary<YearEnum, DateOnly> AnchorDates => AnchorOmer.Dates;
	}

	private sealed class ShavuotSE : FeastDay
	{
		public ShavuotSE() : base($"{nameof(Id.Shavuot)}", Id.Shavuot) { }
		public override string Hebrew => "שָׁבוּעוֹת";
		public override string Details => "The high sabbath begins the evening before; This is also called Pentecost";
		public override string Title => nameof(Id.Shavuot);
		public override string Icon => "fab fa-creative-commons-zero";
		public override string BadgeCss => FeastGroupBadgeColor.Shavuot;
		public override bool HasCounting => false;
		public override FeastGroup Group => FeastGroup.OmerAndShavuot;
		public override int AddDays => 49;
		public override IReadOnlyDictionary<YearEnum, DateOnly> AnchorDates => AnchorOmer.Dates;
	}

	private sealed class YomTeruahSE : FeastDay
	{
		public YomTeruahSE() : base($"{nameof(Id.YomTeruah)}", Id.YomTeruah) { }
		public override string Hebrew => "יוֹם תְּרוּעָה";
		public override string Details => "Day of Trumpets, a high sabbath";
		public override string Title => "Yom Teruah"; 
		public override string Icon => "fas fa-bullhorn";
		public override string BadgeCss => FeastGroupBadgeColor.YomTeruah;
		public override bool HasCounting => false;
		public override FeastGroup Group => FeastGroup.Fall;
		public override int AddDays => 1;
		public override IReadOnlyDictionary<YearEnum, DateOnly> AnchorDates => Tishri.Dates;
	}

	private sealed class YomKippurSE : FeastDay
	{
		public YomKippurSE() : base($"{nameof(Id.YomKippur)}", Id.YomKippur) { }
		public override string Hebrew => "יוֹם כִּיפּוּר";
		public override string Details => "Day of Atonements; a high sabbath";  // In the afternoon we have our Yom Kippur service and break the fast after the sun sets
		public override string Title => "Yom Kippur";
		public override string Icon => "fas fa-hands-helping";
		public override string BadgeCss => FeastGroupBadgeColor.YomKippur;
		public override bool HasCounting => false;
		public override FeastGroup Group => FeastGroup.Fall;
		public override int AddDays => 10;
		public override IReadOnlyDictionary<YearEnum, DateOnly> AnchorDates => Tishri.Dates;
	}

	private sealed class SukkotSE : FeastDay
	{
		public SukkotSE() : base($"{nameof(Id.Sukkot)}", Id.Sukkot) { }
		public override string Hebrew => "סֻּכּוֹת";
		public override string Details => "Feast of tabernacles; days 1 and 8 are high sabbaths";  // Preparation Day, High Sabbath begins at sunset
		public override string Title => "Sukkot";
		public override string Icon => "fas fa-campground";
		public override string BadgeCss => FeastGroupBadgeColor.Sukkot;
		public override bool HasCounting => true;
		public override FeastGroup Group => FeastGroup.Fall;
		public override int AddDays => 15;
		public override IReadOnlyDictionary<YearEnum, DateOnly> AnchorDates => Tishri.Dates;
	}

	private sealed class HanukkahSE : FeastDay
	{
		public HanukkahSE() : base($"{nameof(Id.Hanukkah)}", Id.Hanukkah) { }
		public override string Hebrew => "חֲנֻכָּה";
		public override string Details => "First day of Hanukkah; Date determined by Rabbinic sources";
		public override string Title => nameof(Id.Hanukkah);
		public override string Icon => "fas fa-hanukiah";
		public override string BadgeCss => FeastGroupBadgeColor.Hanukkah;
		public override bool HasCounting => true;
		public override FeastGroup Group => FeastGroup.Winter;
		public override int AddDays => 0;
		public override IReadOnlyDictionary<YearEnum, DateOnly> AnchorDates => AnchorHanukkah.Dates;
	}

	#endregion

}

// Ignore Spelling: Yom Kippur, Kodesh