using Ardalis.SmartEnum;

namespace CalendarApp.Enums;

public abstract class FeastGroup : SmartEnum<FeastGroup>
{
	#region Id's
	private static class Id
	{
		internal const int Winter = 1;
		internal const int PassoverSeason = 2;
		internal const int OmerAndShavuot = 3;
		internal const int DaysOf = 4;
		internal const int Fall = 5;
	}
	#endregion

	#region Declared Public Instances
	public static readonly FeastGroup Winter = new WinterSE();
	public static readonly FeastGroup PassoverSeason = new PassoverSeasonSE();
	public static readonly FeastGroup OmerAndShavuot = new OmerAndShavuotSE();
	public static readonly FeastGroup DaysOf = new DaysOfSE();
	public static readonly FeastGroup Fall = new FallSE();
	#endregion

	private FeastGroup(string name, int value) : base(name, value) { }

	public abstract string Title { get; }

	public IEnumerable<string> Titles()
	{
		var seen = new HashSet<string>(StringComparer.Ordinal);
		foreach (var feast in FeastDay.List.OrderBy(feast => feast.Value))
		{
			if (feast.Group == this && seen.Add(feast.Title))
			{
				yield return feast.Title;
			}
		}

		foreach (var count in DayCount.List.OrderBy(count => count.Value))
		{
			if (count.Group == this && seen.Add(count.Title))
			{
				yield return count.Title;
			}
		}
	}

	#region Private Instantiation

	private sealed class WinterSE : FeastGroup
	{
		public WinterSE() : base(nameof(Id.Winter), Id.Winter) { }
		public override string Title => "Winter";
	}

	private sealed class PassoverSeasonSE : FeastGroup
	{
		public PassoverSeasonSE() : base(nameof(Id.PassoverSeason), Id.PassoverSeason) { }
		public override string Title => "Passover Season";
	}

	private sealed class OmerAndShavuotSE : FeastGroup
	{
		public OmerAndShavuotSE() : base(nameof(Id.OmerAndShavuot), Id.OmerAndShavuot) { }
		public override string Title => "Omer and Shavuot";
	}

	private sealed class DaysOfSE : FeastGroup
	{
		public DaysOfSE() : base(nameof(Id.DaysOf), Id.DaysOf) { }
		public override string Title => "Days of";
	}

	private sealed class FallSE : FeastGroup
	{
		public FallSE() : base(nameof(Id.Fall), Id.Fall) { }
		public override string Title => "Fall";
	}

	#endregion
}
