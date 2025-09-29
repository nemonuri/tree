namespace Nemonuri.Graphs.Infrastructure;

public static class AggregatingPhaseTheory
{
    public const int Inner = 1 << 0;
    public const int Previous = 1 << 1;
    public const int Post = 1 << 2;
    public const int Recursive = 1 << 3;

    public const int Moment = Previous | Post;



    public static bool IsInner(int value) => (value & Inner) != 0;
    public static bool IsOuter(int value) => !IsInner(value);

    public static bool IsPreviousOrMoment(int value) => (value & Previous) != 0;
    public static bool IsPostOrMoment(int value) => (value & Post) != 0;
    public static bool IsMoment(int value) => (value & Moment) == Moment;
    public static bool IsNever(int value) => (value & Moment) == 0;
    public static bool IsPrevious(int value) => (value & Moment) == Previous;
    public static bool IsPost(int value) => (value & Moment) == Post;

    public static bool IsRecursive(int value) => (value & Recursive) != 0;
    public static bool IsInitial(int value) => !IsRecursive(value);

    //--- InnerPhaseLabel extensions ---
    public static bool IsPreviousOrMoment(this InnerPhaseLabel value) => IsPreviousOrMoment((int)value);
    public static bool IsPostOrMoment(this InnerPhaseLabel value) => IsPostOrMoment((int)value);
    public static bool IsMoment(this InnerPhaseLabel value) => IsMoment((int)value);
    public static bool IsNever(this InnerPhaseLabel value) => IsNever((int)value);
    public static bool IsPrevious(this InnerPhaseLabel value) => IsPrevious((int)value);
    public static bool IsPost(this InnerPhaseLabel value) => IsPost((int)value);
    //---|

    //--- OuterPhaseLabel extensions ---
    public static bool IsPrevious(this OuterPhaseLabel value) => IsPreviousOrMoment((int)value);
    public static bool IsPost(this OuterPhaseLabel value) => IsPostOrMoment((int)value);
    public static bool IsNever(this OuterPhaseLabel value) => IsNever((int)value);

    public static bool IsRecursive(this OuterPhaseLabel value) => IsRecursive((int)value);
    public static bool IsInitial(this OuterPhaseLabel value) => IsInitial((int)value);
    //---|
}
