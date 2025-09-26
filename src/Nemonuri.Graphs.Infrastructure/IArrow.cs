namespace Nemonuri.Graphs.Infrastructure;

public interface IArrow<TTail, THead>
{
    TTail Tail { get; }
    THead Head { get; }
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