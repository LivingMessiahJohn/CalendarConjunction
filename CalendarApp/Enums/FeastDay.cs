
using Ardalis.SmartEnum;
//using CalendarApp;

namespace CalendarApp.Enums;

public record DateRange(DateOnly Min, DateOnly Max);

public abstract class FeastDay : SmartEnum<FeastDay>
{
	#region Id's
	private static class Id
	{
		internal const int Hanukkah = 1;
		internal const int Purim = 2;
		internal const int LambSelected = 3;
		internal const int Passover = 4;
		internal const int Omer = 5;
		internal const int Weeks = 6;
		internal const int Trumpets = 7;
		internal const int YomKippur = 8;
		internal const int Sukkot = 9;
	}
	#endregion

	#region Declared Public Instances
	public static readonly FeastDay Purim = new PurimSE();
	public static readonly FeastDay LambSelected = new LambSelectedSE();
	public static readonly FeastDay Passover = new PassoverSE();
	public static readonly FeastDay Omer = new OmerSE();
	public static readonly FeastDay Weeks = new WeeksSE();
	public static readonly FeastDay Trumpets = new TrumpetsSE();
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
	public abstract bool HasCounting { get; }
	public abstract DateOnly Date { get; }
	public abstract DateRange DateRange { get; }
	#endregion


	#region Extra Properties
	//public DateOnly ErevDate => Date.AddDays(-1);
	//public DateOnly DayTimeDate => Date;
	#endregion


	#region Private Instantiation

	private sealed class PurimSE : FeastDay
	{
		public PurimSE() : base($"{nameof(Id.Purim)}", Id.Purim) { }
		public override string Hebrew => "פוּרִים";
		public override string Details => "Tradition is to read the book of Esther; date determined by Rabbinic sources";
		public override string Title => nameof(Id.Purim);
		public override string Icon => "fas fa-dice";  // far fa-square
		public override bool HasCounting => false;
		public override DateOnly Date => FeastDayDates.Purim;
		public override DateRange DateRange => new(Date, Date);
	}

	private sealed class LambSelectedSE : FeastDay
	{
		public LambSelectedSE() : base($"{nameof(Id.LambSelected)}", Id.LambSelected) { }
		public override string Hebrew => "פֶּסַח";
		public override string Details => "The Seder Meal is prepared on the 14th of Aviv. As evening starts, the meal is eaten. Also, this becomes the first day of Unleavened bread";
		public override string Title => "Lamb Selected";
		public override string Icon => "fas fa-door-open";
		public override bool HasCounting => true;
		public override DateOnly Date => FeastDayDates.LambSelected;
		public override DateRange DateRange => new(Date, Date.AddDays(DayCount.Hanukkah.TotalDays));
	}

	private sealed class PassoverSE : FeastDay
	{
		public PassoverSE() : base($"{nameof(Id.Passover)}", Id.Passover) { }
		public override string Hebrew => "פֶּסַח";
		public override string Details => "The Seder Meal is prepared on the 14th of Aviv. As evening starts, the meal is eaten. Also, this becomes the first day of Unleavened bread";
		public override string Title => nameof(Id.Passover);
		public override string Icon => "fas fa-door-open";
		public override bool HasCounting => true;
		public override DateOnly Date => FeastDayDates.Passover;
		public override DateRange DateRange => new(Date, Date.AddDays(DayCount.Hanukkah.TotalDays));
	}

	private sealed class OmerSE : FeastDay
	{
		public OmerSE() : base($"{nameof(Id.Omer)}", Id.Omer) { }
		public override string Hebrew => "סְפִירַת הָעוֹמֶר"; // Sefirat HaOmer
		public override string Details => "the counting of the Omer";
		public override string Title => nameof(Id.Omer);
		public override string Icon => "fa-solid fa-list-ol";
		public override bool HasCounting => true;
		public override DateOnly Date => FeastDayDates.Omer;
		public override DateRange DateRange => new(Date, Date.AddDays(DayCount.Omer.TotalDays));
	}

	private sealed class WeeksSE : FeastDay
	{
		public WeeksSE() : base($"{nameof(Id.Weeks)}", Id.Weeks) { }
		public override string Hebrew => "שָׁבוּעוֹת";
		public override string Details => "The hight sabbath begins the evening before; This is also called Pentecost";
		public override string Title => nameof(Id.Weeks);
		public override string Icon => "fab fa-creative-commons-zero";
		public override bool HasCounting => false;  
		public override DateOnly Date => FeastDayDates.Weeks;
		public override DateRange DateRange => new(Date, Date);
	}

	private sealed class TrumpetsSE : FeastDay
	{
		public TrumpetsSE() : base($"{nameof(Id.Trumpets)}", Id.Trumpets) { }
		public override string Hebrew => "יוֹם תְּרוּעָה";
		public override string Details => "A high holy day sabbath";
		public override string Title => nameof(Id.Trumpets);
		public override string Icon => "fas fa-bullhorn";
		public override bool HasCounting => false;
		public override DateOnly Date => FeastDayDates.Sukkot;
		public override DateRange DateRange => new(Date, Date);
	}

	private sealed class YomKippurSE : FeastDay
	{
		public YomKippurSE() : base($"{nameof(Id.YomKippur)}", Id.YomKippur) { }
		public override string Hebrew => "יוֹם כִּיפּוּר";
		public override string Details => "In the afternoon we have our Yom Kippur service and break the fast after the sun sets";
		public override string Title => "Yom Kippur";
		public override string Icon => "fas fa-hands-helping";
		public override bool HasCounting => false;
		public override DateOnly Date => FeastDayDates.YomKippur;
		public override DateRange DateRange => new(Date, Date);
	}

	private sealed class SukkotSE : FeastDay
	{
		public SukkotSE() : base($"{nameof(Id.Sukkot)}", Id.Sukkot) { }
		public override string Hebrew => "סֻּכּוֹת";
		public override string Details => "Preparation Day, High Sabbath begins at sunset";
		public override string Title => "Sukkot | Day 1";
		public override string Icon => "fas fa-campground";
		public override bool HasCounting => true;
		public override DateOnly Date => FeastDayDates.Sukkot;
		public override DateRange DateRange => new(Date, Date.AddDays(DayCount.Sukkot.TotalDays));
	}

	private sealed class HanukkahSE : FeastDay
	{
		public HanukkahSE() : base($"{nameof(Id.Hanukkah)}", Id.Hanukkah) { }
		public override string Hebrew => "חֲנֻכָּה";
		public override string Details => "First day of Hanukkah; Date determined by Rabbinic sources";
		public override string Title => nameof(Id.Hanukkah);
		public override string Icon => "fas fa-hanukiah";
		public override bool HasCounting => true;
		public override DateOnly Date => FeastDayDates.Hanukkah;
		public override DateRange DateRange => new(Date, Date.AddDays(DayCount.Hanukkah.TotalDays));
	}

	#endregion

}

// Ignore Spelling: Yom Kippur