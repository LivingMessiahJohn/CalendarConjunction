using Ardalis.SmartEnum;

namespace CalendarApp.Enums;

//public record struct CoordinateRecord(TimeOnly MoonriseTime, string MoonriseAzimuth, TimeOnly MoonsetTime, string MoonsetAzimuth);


public abstract class Moon : SmartEnum<Moon>
{
	internal const string Adar2Suffix = " II";

	#region Id's
	private static class Id
	{
		internal const int Nissan = 1; // Aviv
		internal const int Iyar = 2;   // biblical: Ziv
		internal const int Sivan = 3;
		internal const int Tammuz = 4;
		internal const int Av = 5;
		internal const int Elul = 6;
		internal const int Tishri = 7;  //Tishrei biblical: Ethanim
		internal const int Heshvan = 8; //Cheshvan or Marcheshvan; biblical: Bul
		internal const int Kislev = 9;
		internal const int Tevet = 10;
		internal const int Shevat = 11;
		internal const int Adar = 12;
		internal const int Adar2 = 13;
	}
	#endregion

	#region Declared Public Instances
	public static readonly Moon Nissan = new NissanMonth();
	public static readonly Moon Iyar = new IyarMonth();
	public static readonly Moon Sivan = new SivanMonth();
	public static readonly Moon Tammuz = new TammuzMonth();
	public static readonly Moon Av = new AvMonth();
	public static readonly Moon Elul = new ElulMonth();
	public static readonly Moon Tishri = new TishriMonth();
	public static readonly Moon Heshvan = new HeshvanMonth();
	public static readonly Moon Kislev = new KislevMonth();
	public static readonly Moon Tevet = new TevetMonth();
	public static readonly Moon Shevat = new ShevatMonth();
	public static readonly Moon Adar = new AdarMonth();
	public static readonly Moon Adar2 = new Adar2Month();

	private Moon(string name, int value) : base(name, value) { }

	#endregion

	#region Extra Fields
	public abstract string FullName { get; }
	public abstract string Hebrew { get; }
	public abstract string BiblicalName { get; }
	public abstract string BiblicalHebrew { get; }  // http://www.yashanet.com/library/hebrew-days-and-months.html
	public abstract string Description { get; }
	//public abstract CoordinateRecord CoordinateRecord { get; }
	#endregion

	/*
	#region Extra Properties
	public DateTime DateTime2 => Date.ToDateTime(new TimeOnly(12, 0));

	public DateOnly ErevDate => Date == DateOnly.MinValue // Safe DateOnly properties — avoid DateTime underflow when Date == DateTime.MinValue
	? DateOnly.MinValue
	: Date.AddDays(-1);
	public DateOnly DayTimeDate => Date == DateOnly.MinValue
		? DateOnly.MinValue
		: Date;
	#endregion
	*/

	#region Private Instantiation

	private sealed class NissanMonth : Moon
	{
		public NissanMonth() : base($"{nameof(Id.Nissan)}", Id.Nissan) { }
		public override string Hebrew => "ניסן";
		public override string BiblicalName => "Abib";
		public override string BiblicalHebrew => "הָאָבִיב";
		public override string FullName => nameof(Id.Nissan);
		public override string Description => String.IsNullOrEmpty(BiblicalHebrew) ? $"{BiblicalName}" : $"{BiblicalName} {BiblicalHebrew}";
		//public override CoordinateRecord CoordinateRecord => MoonCoordinates.Nissan;
	}

	private sealed class IyarMonth : Moon
	{
		public IyarMonth() : base($"{nameof(Id.Iyar)}", Id.Iyar) { }
		public override string Hebrew => "אייר";
		public override string BiblicalName => "Ziv";
		public override string BiblicalHebrew => "זִו";
		public override string FullName => nameof(Id.Iyar);
		public override string Description => String.IsNullOrEmpty(BiblicalHebrew) ? $"{BiblicalName}" : $"{BiblicalName} {BiblicalHebrew}";
		//public override CoordinateRecord CoordinateRecord => MoonCoordinates.Iyar;
	}

	private sealed class SivanMonth : Moon
	{
		public SivanMonth() : base($"{nameof(Id.Sivan)}", Id.Sivan) { }
		public override string Hebrew => "סיון";
		public override string BiblicalName => "3rd";
		public override string BiblicalHebrew => "";
		public override string FullName => nameof(Id.Sivan);
		public override string Description => String.IsNullOrEmpty(BiblicalHebrew) ? $"{BiblicalName}" : $"{BiblicalName} {BiblicalHebrew}";
		//public override CoordinateRecord CoordinateRecord => MoonCoordinates.Sivan;
	}

	private sealed class TammuzMonth : Moon
	{
		public TammuzMonth() : base($"{nameof(Id.Tammuz)}", Id.Tammuz) { }
		public override string Hebrew => "תמוז";
		public override string BiblicalName => "4th";
		public override string BiblicalHebrew => "";
		public override string FullName => nameof(Id.Tammuz);
		public override string Description => String.IsNullOrEmpty(BiblicalHebrew) ? $"{BiblicalName}" : $"{BiblicalName} {BiblicalHebrew}";
		//public override CoordinateRecord CoordinateRecord => MoonCoordinates.Tammuz;
	}

	private sealed class AvMonth : Moon
	{
		public AvMonth() : base($"{nameof(Id.Av)}", Id.Av) { }
		public override string Hebrew => "אב";
		public override string BiblicalName => "5th";
		public override string BiblicalHebrew => "";
		public override string FullName => nameof(Id.Av);
		public override string Description => String.IsNullOrEmpty(BiblicalHebrew) ? $"{BiblicalName}" : $"{BiblicalName} {BiblicalHebrew}";
		//public override CoordinateRecord CoordinateRecord => MoonCoordinates.Av;
	}

	private sealed class ElulMonth : Moon
	{
		public ElulMonth() : base($"{nameof(Id.Elul)}", Id.Elul) { }
		public override string Hebrew => "אלול";
		public override string BiblicalName => "6th";
		public override string BiblicalHebrew => "";
		public override string FullName => nameof(Id.Elul);
		public override string Description => String.IsNullOrEmpty(BiblicalHebrew) ? $"{BiblicalName}" : $"{BiblicalName} {BiblicalHebrew}";
		//public override CoordinateRecord CoordinateRecord => MoonCoordinates.Elul;
	}

	private sealed class TishriMonth : Moon
	{
		public TishriMonth() : base($"{nameof(Id.Tishri)}", Id.Tishri) { }
		public override string Hebrew => "תשרי";
		public override string BiblicalName => "Ethanim";
		public override string BiblicalHebrew => "הָאֵתָנִים";
		public override string FullName => nameof(Id.Tishri);
		public override string Description => String.IsNullOrEmpty(BiblicalHebrew) ? $"{BiblicalName}" : $"{BiblicalName} {BiblicalHebrew}";
		//public override CoordinateRecord CoordinateRecord => MoonCoordinates.Tishri;
	}


	// also transliterated as Cheshvan
	private sealed class HeshvanMonth : Moon
	{
		public HeshvanMonth() : base($"{nameof(Id.Heshvan)}", Id.Heshvan) { }
		public override string Hebrew => "חשון";
		public override string BiblicalName => "Bul";
		public override string BiblicalHebrew => "בּוּל";
		public override string FullName => nameof(Id.Heshvan);
		public override string Description => String.IsNullOrEmpty(BiblicalHebrew) ? $"{BiblicalName}" : $"{BiblicalName} {BiblicalHebrew}";
		//public override CoordinateRecord CoordinateRecord => MoonCoordinates.Heshvan;
	}

	private sealed class KislevMonth : Moon
	{
		public KislevMonth() : base($"{nameof(Id.Kislev)}", Id.Kislev) { }
		public override string Hebrew => "כסלו";
		public override string BiblicalName => "9th";
		public override string BiblicalHebrew => "";
		public override string FullName => nameof(Id.Kislev);
		public override string Description => String.IsNullOrEmpty(BiblicalHebrew) ? $"{BiblicalName}" : $"{BiblicalName} {BiblicalHebrew}";
		//public override CoordinateRecord CoordinateRecord => MoonCoordinates.Kislev;
	}

	private sealed class TevetMonth : Moon
	{
		public TevetMonth() : base($"{nameof(Id.Tevet)}", Id.Tevet) { }
		public override string Hebrew => "טבת";
		public override string BiblicalName => "10th";
		public override string BiblicalHebrew => "";
		public override string FullName => nameof(Id.Tevet);
		public override string Description => String.IsNullOrEmpty(BiblicalHebrew) ? $"{BiblicalName}" : $"{BiblicalName} {BiblicalHebrew}";
		//public override CoordinateRecord CoordinateRecord => MoonCoordinates.Tevet;
	}
	private sealed class ShevatMonth : Moon
	{
		public ShevatMonth() : base($"{nameof(Id.Shevat)}", Id.Shevat) { }
		public override string Hebrew => "שבט";
		public override string BiblicalName => "11th";
		public override string BiblicalHebrew => "";
		public override string FullName => nameof(Id.Shevat);
		public override string Description => String.IsNullOrEmpty(BiblicalHebrew) ? $"{BiblicalName}" : $"{BiblicalName} {BiblicalHebrew}";
		//public override CoordinateRecord CoordinateRecord => MoonCoordinates.Shevat;
	}

	private sealed class AdarMonth : Moon
	{
		public AdarMonth() : base($"{nameof(Id.Adar)}", Id.Adar) { }
		public override string Hebrew => "אדר א";
		public override string BiblicalName => "12th";
		public override string BiblicalHebrew => "";
		public override string FullName => nameof(Id.Adar);
		public override string Description => String.IsNullOrEmpty(BiblicalHebrew) ? $"{BiblicalName}" : $"{BiblicalName} {BiblicalHebrew}";
		//public override CoordinateRecord CoordinateRecord => MoonCoordinates.Adar;
	}

	private sealed class Adar2Month : Moon
	{
		public Adar2Month() : base($"{nameof(Id.Adar2)}", Id.Adar2) { }
		public override string Hebrew => "אדר ב";
		public override string BiblicalName => "13th";
		public override string BiblicalHebrew => "";
		public override string FullName => nameof(Id.Adar) + Adar2Suffix;
		public override string Description => String.IsNullOrEmpty(BiblicalHebrew) ? $"{BiblicalName}" : $"{BiblicalName} {BiblicalHebrew}";
		//public override CoordinateRecord CoordinateRecord => LunarMonthCoordinates.Adar2;
	}

	#endregion
}

// Ignore Spelling: Nissan, Iyar, Sivan, Tammuz, Av, Elul, Tishri, Shevat, Adar, Heshvan, Kislev, Tevet, Adar2
