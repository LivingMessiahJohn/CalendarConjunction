using Ardalis.SmartEnum;

namespace CalendarApp.Enums;

public abstract class MoonPhase : SmartEnum<MoonPhase>
{
    #region Id's
    private static class Id
    {
        internal const int None = 0;
        internal const int New = 1;
        internal const int Waxing = 2;
        internal const int Full = 3;
        internal const int Waning = 4;
    }
    #endregion

    #region Declared Public Instances
    public static readonly MoonPhase None = new NoneSE();
    public static readonly MoonPhase New = new NewSE();
    public static readonly MoonPhase Waxing = new WaxingSE();
    public static readonly MoonPhase Full = new FullSE();
    public static readonly MoonPhase Waning = new WaningSE();
    #endregion

    private MoonPhase(string name, int value) : base(name, value) { }

    #region Extra Fields
    public abstract string Title { get; }
    public abstract string Image { get; }
    public abstract string Description { get; }
    #endregion

    #region Private Instantiation
    private sealed class NoneSE : MoonPhase
    {
        public NoneSE() : base($"{nameof(Id.None)}", Id.None) { }
        public override string Title => "None";
        public override string Image => "";
        public override string Description => "N/A this date has no note-able moon location";
    }

    private sealed class NewSE : MoonPhase
    {
        public NewSE() : base($"{nameof(Id.New)}", Id.New) { }
        public override string Title => "New Moon";
        public override string Image => "new.jpg";
        public override string Description => "Occurs at Erev Rosh Kodesh";
    }

    private sealed class WaxingSE : MoonPhase
    {
        public WaxingSE() : base($"{nameof(Id.Waxing)}", Id.Waxing) { }
        public override string Title => "Waxing Half Moon";
        public override string Image => "waxing.jpg";
        public override string Description => "near moon age day 8";
    }

    private sealed class FullSE : MoonPhase
    {
        public FullSE() : base($"{nameof(Id.Full)}", Id.Full) { }
        public override string Title => "Full Moon";
        public override string Image => "full.jpg";
        public override string Description => "near moon age day 15";
    }

    private sealed class WaningSE : MoonPhase
    {
        public WaningSE() : base($"{nameof(Id.Waning)}", Id.Waning) { }
        public override string Title => "Waning Half Moon";
        public override string Image => "waning.jpg";
        public override string Description => "near moon age day 22";
    }
    #endregion
}
