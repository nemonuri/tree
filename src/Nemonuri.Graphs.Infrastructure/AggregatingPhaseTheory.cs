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

    public static bool IsPrevious(int value) => (value & Previous) != 0;
    public static bool IsPost(int value) => (value & Post) != 0;
    public static bool IsMoment(int value) => (value & Moment) == Moment;
    public static bool IsNever(int value) => (value & Moment) == 0;

    public static bool IsRecursive(int value) => (value & Recursive) != 0;
    public static bool IsInitial(int value) => !IsRecursive(value);
}
