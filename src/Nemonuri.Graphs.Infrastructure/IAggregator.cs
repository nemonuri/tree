using System.Diagnostics.CodeAnalysis;

namespace Nemonuri.Graphs.Infrastructure;

public readonly record struct ValueNull { }

public interface IEmbedder<TSource, TTarget, TRemainder>
{ 
    bool TryEmbed(TSource source, [NotNullWhen(true)] out TTarget target, out TRemainder? remainder);
}

public interface IEmbedder<TSource, TTarget>
{
    TTarget Embed(TSource source);
}

public interface IEffectfulAggregator<T, TValue, TContext, TResult>
{
    TResult Aggregate(TContext context, T source, TValue value);
}

public interface IMutableContextedAggregator<TMutableContext, T, TValue>
{
    T Aggregate(scoped ref TMutableContext mutableContext, T source, TValue value);
}

public interface IAggregator<T, TValue>
{
    T Aggregate(T source, TValue value);
}

public interface IInitialSourceGivenAggregator<T, TValue> : IAggregator<T, TValue>
{ 
    T InitialSource { get; }
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
