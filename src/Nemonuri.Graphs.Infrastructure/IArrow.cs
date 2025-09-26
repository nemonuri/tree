using System.Diagnostics.CodeAnalysis;

namespace Nemonuri.Graphs.Infrastructure;

public interface IArrow<TTail, THead>
{
    TTail Tail { get; }
    THead Head { get; }
}

public interface IInArrowSet<TArrow, TTail, THead> : IEnumerable<TArrow>
    where TArrow : IArrow<TTail, THead>
{
    bool TryGetCommonHead([NotNullWhen(true)] out THead? commonHead);
}

public interface IOutArrowSet<TArrow, TTail, THead> : IEnumerable<TArrow>
    where TArrow : IArrow<TTail, THead>
{
    bool TryGetCommonTail([NotNullWhen(true)] out TTail? commonTail);
}

public interface IFixedArrow<TArrow, TTail, THead> :
    ISupportFixedPoint<TArrow>,
    IArrow<TTail, THead>
    where TArrow : IFixedArrow<TArrow, TTail, THead>
{
}

public class MyFixedArrow : IFixedArrow<MyFixedArrow, int, IArrow<string, MyFixedArrow>>
{
    public MyFixedArrow ToFixedPoint()
    {
        return this;
    }

    public int Tail => 0;

    public IArrow<string, MyFixedArrow> Head => throw new NotImplementedException();
}