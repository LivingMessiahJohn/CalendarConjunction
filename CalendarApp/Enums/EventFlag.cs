using Ardalis.SmartEnum;

namespace CalendarApp.Enums;

public abstract class EventFlag : SmartEnum<EventFlag>
{
	// Powers of two so a selection can hold several events at once.
	#region Id's
	private static class BitwiseId
	{
		internal const int Hanukkah = 1;
		internal const int Purim = 2;
		internal const int LambSelected = 4;
		internal const int PassoverMeal = 8;
		internal const int Matzot = 16;
		internal const int Omer = 32;
		internal const int Shavuot = 64;
		internal const int Repentance = 128;
		internal const int Awe = 256;
		internal const int YomTeruah = 512;
		internal const int YomKippur = 1024;
		internal const int Sukkot = 2048;
		internal const int RoshKodesh = 4096;
	}
	#endregion

	#region Declared Public Instances
	public static readonly EventFlag Hanukkah = new HanukkahSE();
	public static readonly EventFlag Purim = new PurimSE();
	public static readonly EventFlag LambSelected = new LambSelectedSE();
	public static readonly EventFlag PassoverMeal = new PassoverMealSE();
	public static readonly EventFlag Matzot = new MatzotSE();
	public static readonly EventFlag Omer = new OmerSE();
	public static readonly EventFlag Shavuot = new ShavuotSE();
	public static readonly EventFlag Repentance = new RepentanceSE();
	public static readonly EventFlag Awe = new AweSE();
	public static readonly EventFlag YomTeruah = new YomTeruahSE();
	public static readonly EventFlag YomKippur = new YomKippurSE();
	public static readonly EventFlag Sukkot = new SukkotSE();
	public static readonly EventFlag RoshKodesh = new RoshKodeshSE();
	#endregion

	private EventFlag(string name, int value) : base(name, value) { }

	public abstract string Title { get; }
	public abstract FeastGroup? Group { get; }

	public static int AllMask => List.Aggregate(0, (mask, flag) => mask | flag.Value);

	public static int DefaultMask => Matzot.Value | MaskFor(FeastGroup.Fall) | RoshKodesh.Value;

	public static int MaskFor(FeastGroup group)
	{
		var mask = 0;
		foreach (var flag in List)
		{
			if (flag.Group == group)
			{
				mask |= flag.Value;
			}
		}

		return mask;
	}

	public static int MaskForTitles(IEnumerable<string> titles)
	{
		var mask = 0;
		foreach (var title in titles)
		{
			if (TryFromTitle(title, out var flag))
			{
				mask |= flag.Value;
			}
		}

		return mask;
	}

	public static bool Matches(int mask, IEnumerable<string> titles)
	{
		if (mask == 0)
		{
			return true;
		}

		foreach (var title in titles)
		{
			if (TryFromTitle(title, out var flag) && (mask & flag.Value) == flag.Value)
			{
				return true;
			}
		}

		return false;
	}

	public static IReadOnlyList<string> SelectedTitles(int mask)
	{
		if (mask == 0)
		{
			return [];
		}

		if (mask == AllMask)
		{
			return ["All events"];
		}

		return List
				.OrderBy(flag => flag.Value)
				.Where(flag => (mask & flag.Value) == flag.Value)
				.Select(flag => flag.Title)
				.ToList();
	}

	public static bool TryFromTitle(string title, out EventFlag flag)
	{
		var match = List.FirstOrDefault(item => string.Equals(item.Title, title, StringComparison.Ordinal));
		if (match is null)
		{
			flag = null!;
			return false;
		}

		flag = match;
		return true;
	}

	#region Private Instantiation

	private sealed class HanukkahSE : EventFlag
	{
		public HanukkahSE() : base(nameof(BitwiseId.Hanukkah), BitwiseId.Hanukkah) { }
		public override string Title => FeastDay.Hanukkah.Title;
		public override FeastGroup? Group => FeastGroup.Winter;
	}

	private sealed class PurimSE : EventFlag
	{
		public PurimSE() : base(nameof(BitwiseId.Purim), BitwiseId.Purim) { }
		public override string Title => FeastDay.Purim.Title;
		public override FeastGroup? Group => FeastGroup.Winter;
	}

	private sealed class LambSelectedSE : EventFlag
	{
		public LambSelectedSE() : base(nameof(BitwiseId.LambSelected), BitwiseId.LambSelected) { }
		public override string Title => FeastDay.LambSelected.Title;
		public override FeastGroup? Group => FeastGroup.PassoverSeason;
	}

	private sealed class PassoverMealSE : EventFlag
	{
		public PassoverMealSE() : base(nameof(BitwiseId.PassoverMeal), BitwiseId.PassoverMeal) { }
		public override string Title => FeastDay.PassoverMeal.Title;
		public override FeastGroup? Group => FeastGroup.PassoverSeason;
	}

	private sealed class MatzotSE : EventFlag
	{
		public MatzotSE() : base(nameof(BitwiseId.Matzot), BitwiseId.Matzot) { }
		public override string Title => FeastDay.Matzot.Title;
		public override FeastGroup? Group => FeastGroup.PassoverSeason;
	}

	private sealed class OmerSE : EventFlag
	{
		public OmerSE() : base(nameof(BitwiseId.Omer), BitwiseId.Omer) { }
		public override string Title => FeastDay.Omer.Title;
		public override FeastGroup? Group => FeastGroup.OmerAndShavuot;
	}

	private sealed class ShavuotSE : EventFlag
	{
		public ShavuotSE() : base(nameof(BitwiseId.Shavuot), BitwiseId.Shavuot) { }
		public override string Title => FeastDay.Shavuot.Title;
		public override FeastGroup? Group => FeastGroup.OmerAndShavuot;
	}

	private sealed class RepentanceSE : EventFlag
	{
		public RepentanceSE() : base(nameof(BitwiseId.Repentance), BitwiseId.Repentance) { }
		public override string Title => DayCount.Repentance.Title;
		public override FeastGroup? Group => FeastGroup.DaysOf;
	}

	private sealed class AweSE : EventFlag
	{
		public AweSE() : base(nameof(BitwiseId.Awe), BitwiseId.Awe) { }
		public override string Title => DayCount.Awe.Title;
		public override FeastGroup? Group => FeastGroup.DaysOf;
	}

	private sealed class YomTeruahSE : EventFlag
	{
		public YomTeruahSE() : base(nameof(BitwiseId.YomTeruah), BitwiseId.YomTeruah) { }
		public override string Title => FeastDay.YomTeruah.Title;
		public override FeastGroup? Group => FeastGroup.Fall;
	}

	private sealed class YomKippurSE : EventFlag
	{
		public YomKippurSE() : base(nameof(BitwiseId.YomKippur), BitwiseId.YomKippur) { }
		public override string Title => FeastDay.YomKippur.Title;
		public override FeastGroup? Group => FeastGroup.Fall;
	}

	private sealed class SukkotSE : EventFlag
	{
		public SukkotSE() : base(nameof(BitwiseId.Sukkot), BitwiseId.Sukkot) { }
		public override string Title => FeastDay.Sukkot.Title;
		public override FeastGroup? Group => FeastGroup.Fall;
	}

	private sealed class RoshKodeshSE : EventFlag
	{
		public RoshKodeshSE() : base(nameof(BitwiseId.RoshKodesh), BitwiseId.RoshKodesh) { }
		public override string Title => "Rosh Kodesh";
		public override FeastGroup? Group => null;
	}

	#endregion
}
