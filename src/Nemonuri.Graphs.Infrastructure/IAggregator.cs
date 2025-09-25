using System.Diagnostics.CodeAnalysis;

namespace Nemonuri.Graphs.Infrastructure;

public readonly record struct NullValue { }

public interface IEffectfulAggregator<T, TValue, TContext, TResult>
{
    TResult Aggregate(TContext context, T source, TValue value);

    bool TryEmbed(TResult aggreation, [NotNullWhen(true)] out T embedded);
}

public interface IAggregator<T, TValue>
{
    T Aggregate(T source, TValue value);
}

public interface IResultRecord<TEntry, TResult>
{
    TEntry Entry { get; }
    TResult Result { get; }
}

public interface IObservableAggregator<T, TValue, TContext, TResult, TResultRecord> : IEffectfulAggregator<T, TValue, TContext, TResult>
    where TResultRecord : IResultRecord<(TValue Value, TContext Context), TResult>
{
    void OnAggregating(TContext context, T source, TValue value);
    void OnAggregated(TResultRecord resultRecord);
}
