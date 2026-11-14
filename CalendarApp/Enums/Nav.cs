using Ardalis.SmartEnum;

namespace CalendarApp.Enums;

public abstract class Nav : SmartEnum<Nav>
{
	#region Id's
	private static class Id
	{
		internal const int Home = 1;
		internal const int Constellations = 2;
		internal const int MonthDaysDiff = 3;
		internal const int About = 4;
	}
	#endregion

	#region Declared Public Instances
	public static readonly Nav Home = new HomeSE();
	public static readonly Nav Constellations = new ConstellationsSE();
	public static readonly Nav MonthDaysDiff = new MonthDaysDiffSE();
	public static readonly Nav About = new AboutSE();
	#endregion

	private Nav(string name, int value) : base(name, value) { }

	#region Extra Fields
	public abstract string Index { get; }
	public abstract string Title { get; }
	public abstract string Icon { get; }
	public abstract int Sort { get; }

	public abstract bool Disabled { get; }
	public string DisabledHtml => $"{(Disabled ? " disabled" : "")}";
	public string TextColor => $"{(Disabled ? " text-black-50" : "text-primary")}";
	#endregion

	#region Private Instantiation
	private sealed class HomeSE : Nav
	{
		public HomeSE() : base($"{nameof(Id.Home)}", Id.Home) { }
		public override string Index => "/";
		public override string Title => "Conjunction Calendar";
		public override string Icon => "fa-solid fa-house";
		public override int Sort => Id.Home;
		public override bool Disabled => false;
	}

	private sealed class ConstellationsSE : Nav
	{
		public ConstellationsSE() : base($"{nameof(Id.Constellations)}", Id.Constellations) { }
		public override string Index => "/constellations";
		public override string Title => "Constellations";
		public override string Icon => "fa-solid fa-star";
		public override int Sort => Id.Constellations;
		public override bool Disabled => false;
	}

	private sealed class MonthDaysDiffSE : Nav
	{
		public MonthDaysDiffSE() : base($"{nameof(Id.MonthDaysDiff)}", Id.MonthDaysDiff) { }
		public override string Index => "/MonthDaysDiff";
		public override string Title => "Month Days Diff";
		public override string Icon => "far fa-calendar-check";
		public override int Sort => Id.MonthDaysDiff;
		public override bool Disabled => false;
	}

	private sealed class AboutSE : Nav
	{
		public AboutSE() : base($"{nameof(Id.About)}", Id.About) { }
		public override string Index => "/about";
		public override string Title => "About";
		public override string Icon => "fa-solid fa-circle-info";
		public override int Sort => Id.About;
		public override bool Disabled => false;
	}
	#endregion
}
