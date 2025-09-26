namespace Nemonuri.Graphs.Infrastructure;

public readonly struct WrappedToEffectfulAggregator<TAggregator, T, TValue> : IEffectfulAggregator<T, TValue, NullValue, T>
    where TAggregator : IAggregator<T, TValue>
{
    public WrappedToEffectfulAggregator(TAggregator internalAggregator)
    {
        InternalAggregator = internalAggregator;
    }

    public TAggregator InternalAggregator { get; }

    public T Aggregate(NullValue context, T source, TValue value)
    {
        return InternalAggregator.Aggregate(source, value);
    }
}
