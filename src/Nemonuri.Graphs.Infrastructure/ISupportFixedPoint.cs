namespace Nemonuri.Graphs.Infrastructure;

public interface ISupportFixedPoint<out TFix> 
    where TFix : ISupportFixedPoint<TFix>
{
    TFix ToFixedPoint();
}

public interface IImmutableAggregation<TSelf, T> : ISupportFixedPoint<TSelf>
    where TSelf : IImmutableAggregation<TSelf, T>
{
    TSelf Aggregate(T item);

    TSelf AggregateRange(IEnumerable<T> item);
}


public interface IEmptyOperation<TSelf> : ISupportFixedPoint<TSelf>
    where TSelf : IEmptyOperation<TSelf>
{
    static abstract TSelf Empty { get; }

    static abstract bool IsEmpty(TSelf value);
}

public interface IEmptyPremise<T>
{
    T Empty { get; }
    
    bool IsEmpty(T value);
}

public interface IImmutableEnumerable<TSelf, T> : IImmutableAggregation<TSelf, T>, IEnumerable<T>
    where TSelf : IImmutableEnumerable<TSelf, T>
{ }

public interface INode<TSelf, TAdjacentNodes> : ISupportFixedPoint<TSelf>
    where TSelf : INode<TSelf, TAdjacentNodes>
    where TAdjacentNodes : IEnumerable<TSelf>
{
    TAdjacentNodes AdjacentNodes { get; }
}

public interface ISupportPath<T, TPath>
    where TPath : IEquatable<TPath>
{
    TPath GetPath(T item);
    bool TryGetFromPath(TPath path, out T? result);
}


