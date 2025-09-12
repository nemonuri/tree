namespace Nemonuri.Trees;

public readonly struct MinMax
{
    public const int Infinity = int.MaxValue;

    public int Min { get; }
    public int Max { get; }

    public MinMax(int min, int max)
    {
        Guard.IsGreaterThanOrEqualTo(min, 0);
        Guard.IsGreaterThanOrEqualTo(max, min);

        Min = min;
        Max = max;
    }
}