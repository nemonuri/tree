using Nemonuri.Graphs.Infrastructure;

namespace Nemonuri.Grammars.Infrastructure.TestDatas;

public class AdHocInitialSourceGivenAggregator<T, TValue> : IInitialSourceGivenAggregator<T, TValue>
{
    private readonly Func<T> _initialSourceImpl;
    private readonly Func<T, TValue, T> _aggregateImpl;

    public AdHocInitialSourceGivenAggregator(Func<T> initialSourceImpl, Func<T, TValue, T> aggregateImpl)
    {
        _initialSourceImpl = initialSourceImpl;
        _aggregateImpl = aggregateImpl;
    }

    public T InitialSource => _initialSourceImpl();

    public T Aggregate(T source, TValue value) => _aggregateImpl(source, value);
}