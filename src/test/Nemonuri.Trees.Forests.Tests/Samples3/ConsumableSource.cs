namespace Nemonuri.Trees.Forests.Tests.Samples3;

public readonly record struct ConsumableSourceInvarient(string SourceText)
{
    public static implicit operator ConsumableSourceInvarient(string v) => new(v);
}
public readonly record struct ConsumableSourceVarient(int Offset) : IComparable<ConsumableSourceVarient>
{
    public static implicit operator ConsumableSourceVarient(int v) => new(v);

    public int CompareTo(ConsumableSourceVarient other)
    {
        return Offset.CompareTo(other.Offset);
    }
}

public readonly record struct ConsumableSource(ConsumableSourceInvarient Invarient, ConsumableSourceVarient Varient);
