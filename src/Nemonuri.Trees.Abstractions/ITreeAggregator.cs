namespace Nemonuri.Trees;

public interface ITreeAggregator<TTree, TElement, TAggregation, TAncestor, TAncestorsAggregation> :
    IAggregator3D<TTree, TAggregation, TAncestor, TAncestorsAggregation>,
    IAncestorConverter<TTree, TAncestor>
    where TTree : ITree<TElement, TTree>
#if NET9_0_OR_GREATER
    , allows ref struct
    where TElement : allows ref struct
    where TAggregation : allows ref struct
    where TAncestor : allows ref struct
    where TAncestorsAggregation : allows ref struct
#endif
{
}

/// <summary>
/// Defines type-specific methods to aggregate tree structured objects.
/// </summary>
/// <inheritdoc cref="AggregatingTheory.Aggregate{_,_,_,_}(IAggregator3D{_,_,_,_},IChildrenProvider{_},IAncestorConverter{_,_},_)" path="/typeparam" />
public interface ITreeAggregator<TElement, TAggregation, TAncestor, TAncestorsAggregation> :
    ITreeAggregator<ITree<TElement>, TElement, TAggregation, TAncestor, TAncestorsAggregation>
#if NET9_0_OR_GREATER
    where TElement : allows ref struct
    where TAggregation : allows ref struct
    where TAncestor : allows ref struct
    where TAncestorsAggregation : allows ref struct
#endif
{
}
