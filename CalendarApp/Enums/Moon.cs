using Ardalis.SmartEnum;

namespace CalendarApp.Enums;

public abstract class Moon : SmartEnum<Moon>
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
    public static readonly Moon None = new NoneSE();
    public static readonly Moon New = new NewSE();
    public static readonly Moon Waxing = new WaxingSE();
    public static readonly Moon Full = new FullSE();
    public static readonly Moon Waning = new WaningSE();
    #endregion

    private Moon(string name, int value) : base(name, value) { }

    #region Extra Fields
    public abstract string Title { get; }
    public abstract string Image { get; }
    public abstract string Description { get; }
    #endregion

    #region Private Instantiation
    private sealed class NoneSE : Moon
    {
        public NoneSE() : base($"{nameof(Id.None)}", Id.None) { }
        public override string Title => "None";
        public override string Image => "";
        public override string Description => "N/A this date has no note-able moon location";
    }

    private sealed class NewSE : Moon
    {
        public NewSE() : base($"{nameof(Id.New)}", Id.New) { }
        public override string Title => "New Moon";
        public override string Image => "new.jpg";
        public override string Description => "Occurs at Erev Rosh Kodesh";
    }

    private sealed class WaxingSE : Moon
    {
        public WaxingSE() : base($"{nameof(Id.Waxing)}", Id.Waxing) { }
        public override string Title => "Waxing Half Moon";
        public override string Image => "waxing.jpg";
        public override string Description => "near moon age day 8";
    }

    private sealed class FullSE : Moon
    {
        public FullSE() : base($"{nameof(Id.Full)}", Id.Full) { }
        public override string Title => "Full Moon";
        public override string Image => "full.jpg";
        public override string Description => "near moon age day 15";
    }

    private sealed class WaningSE : Moon
    {
        public WaningSE() : base($"{nameof(Id.Waning)}", Id.Waning) { }
        public override string Title => "Waning Half Moon";
        public override string Image => "waning.jpg";
        public override string Description => "near moon age day 22";
    }
    #endregion
}
