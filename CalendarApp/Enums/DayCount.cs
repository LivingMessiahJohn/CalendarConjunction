using Ardalis.SmartEnum;

namespace CalendarApp.Enums;

public enum ParentTypeEnum 
{
	FeastDay = 1,
	Month = 2
}

public record ParentRecord(ParentTypeEnum EnumType, int Id);

public abstract class DayCount : SmartEnum<DayCount>
{
	#region Id's
	private static class Id
	{
		internal const int UnleavenedBread = 1; // 1-7
		internal const int Omer = 2;						// 1-50
		internal const int Repentance = 3;			// 1-40 Elul (6),     Repentance starts 1/40 
		internal const int Awe = 4;							// 1-10 Tishrei (7 ), Awe starts 1/10        
		internal const int Sukkot = 5;					// 1-8
		internal const int MenorahCount = 6;				// 1-8
	}
	#endregion

	#region  Declared Public Instances
	public static readonly DayCount UnleavenedBread = new UnleavenedBreadSE();
	public static readonly DayCount Omer = new OmerSE();
	public static readonly DayCount Repentance = new RepentanceSE();
	public static readonly DayCount Awe = new AweSE();
	public static readonly DayCount Sukkot = new SukkotSE();
	public static readonly DayCount MenorahCount = new MenorahCountSE();
	// SE=SmartEnum
	#endregion

	private DayCount(string name, int value) : base(name, value) { } // Constructor

	#region Extra Fields
	public abstract ParentRecord Parent { get; }
	public abstract int TotalDays { get; } // every DayCount starts at one, so the unknown is the Total
	public abstract string Details { get; }
	public abstract string Title { get; }
	public abstract string BadgeCss { get; }
	public abstract bool LastDayIsHighSabbath { get; }
	public abstract FeastGroup Group { get; }
	#endregion


	#region Private Instantiation

	private sealed class UnleavenedBreadSE : DayCount
	{
		public UnleavenedBreadSE() : base($"{nameof(Id.UnleavenedBread)}", Id.UnleavenedBread) { }
		public override ParentRecord Parent => new(ParentTypeEnum.FeastDay,FeastDay.Matzot.Value); 
		public override int TotalDays => 7;
		public override string Details => "Days of Chag Hamatzot"; //
		public override string Title => "Matzot";
		public override string BadgeCss => FeastGroupBadgeColor.UnleavenedBreadCount;
		public override bool LastDayIsHighSabbath => true;
		public override FeastGroup Group => FeastGroup.PassoverSeason;
	}

	// This is just a count
	private sealed class OmerSE : DayCount
	{
		public OmerSE() : base($"{nameof(Id.Omer)}", Id.Omer) { }
		public override ParentRecord Parent => new(ParentTypeEnum.FeastDay, FeastDay.Omer.Value);
		public override int TotalDays => 50;
		public override string Details => "Omer"; //
		public override string Title => "Omer";
		public override string BadgeCss => FeastGroupBadgeColor.Omer;
		public override bool LastDayIsHighSabbath => false;
		public override FeastGroup Group => FeastGroup.OmerAndShavuot;
	}

	private sealed class RepentanceSE : DayCount
	{
		public RepentanceSE() : base($"{nameof(Id.Repentance)}", Id.Repentance) { }
		public override ParentRecord Parent => new(ParentTypeEnum.Month, Moon.Elul.Value);
		public override int TotalDays => 40;
		public override string Details => "Days of Repentance"; //
		public override string Title => "Repentance";
		public override string BadgeCss => FeastGroupBadgeColor.Repentance;
		public override bool LastDayIsHighSabbath => false;
		public override FeastGroup Group => FeastGroup.DaysOf;
	}

	private sealed class AweSE : DayCount
	{
		public AweSE() : base($"{nameof(Id.Awe)}", Id.Awe) { }
		public override ParentRecord Parent => new(ParentTypeEnum.Month, Moon.Tishri.Value);
		public override int TotalDays => 10;
		public override string Details => "Days of Awe"; //
		public override string Title => "Awe";
		public override string BadgeCss => FeastGroupBadgeColor.Awe;
		public override bool LastDayIsHighSabbath => false;
		public override FeastGroup Group => FeastGroup.DaysOf;
	}

	private sealed class SukkotSE : DayCount
	{
		public SukkotSE() : base($"{nameof(Id.Sukkot)}", Id.Sukkot) { }
		public override ParentRecord Parent => new(ParentTypeEnum.FeastDay, FeastDay.Sukkot.Value);
		public override int TotalDays => 8;
		public override string Details => "Days of Sukkot"; // 8th day is Shemini Atzeret (8th day of Assembly)
		public override string Title => "Sukkot";
		public override string BadgeCss => FeastGroupBadgeColor.SukkotCount;
		public override bool LastDayIsHighSabbath => true;
		public override FeastGroup Group => FeastGroup.Fall;
	}

	private sealed class MenorahCountSE : DayCount
	{
		public MenorahCountSE() : base($"{nameof(Id.MenorahCount)}", Id.MenorahCount) { }
		public override ParentRecord Parent => new(ParentTypeEnum.FeastDay, FeastDay.Hanukkah.Value);
		public override int TotalDays => 8;
		public override string Details => "Nights for lighting the hanukiah"; //
		public override string Title => "Hanukkah";
		public override string BadgeCss => FeastGroupBadgeColor.MenorahCount; // "fa-solid fa-hanukiah"
		public override bool LastDayIsHighSabbath => false;
		public override FeastGroup Group => FeastGroup.Winter;
	}

	#endregion
}
