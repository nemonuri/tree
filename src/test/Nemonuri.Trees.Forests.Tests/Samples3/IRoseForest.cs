namespace Nemonuri.Trees.Forests.Tests.Samples3;

public interface IForest<TForest, TForestSequence, TForestSequenceUnion>
    where TForest : IForest<TForest, TForestSequence, TForestSequenceUnion>
    where TForestSequence : IEnumerable<TForest>
    where TForestSequenceUnion : IEnumerable<TForestSequence>
{
    TForestSequenceUnion ChildMatrix { get; }
}

public interface IRoseForest<TValue, TForest, TForestSequence, TForestSequenceUnion> :
    IForest<TForest, TForestSequence, TForestSequenceUnion>,
    ISupportValue<TValue>
    where TForest : IRoseForest<TValue, TForest, TForestSequence, TForestSequenceUnion>
    where TForestSequence : IEnumerable<TForest>
    where TForestSequenceUnion : IEnumerable<TForestSequence>
{
}
