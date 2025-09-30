namespace Nemonuri.Graphs.Infrastructure;

public readonly struct WrappedToEffectfulAggregator<TAggregator, T, TValue> : IEffectfulAggregator<T, TValue, ValueNull, T>
    where TAggregator : IAggregator<T, TValue>
{
    public WrappedToEffectfulAggregator(TAggregator internalAggregator)
    {
        InternalAggregator = internalAggregator;
    }

    public TAggregator InternalAggregator { get; }

    public T Aggregate(ValueNull context, T source, TValue value)
    {
        return InternalAggregator.Aggregate(source, value);
    }
}
