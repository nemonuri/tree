namespace Nemonuri.Trees;

/// <summary>
/// Defines type-specific methods to aggregate tree structured objects.
/// </summary>
/// <inheritdoc cref="AggregatingTheory.Aggregate{_,_,_,_}(IAggregator3D{_,_,_,_},IChildrenProvider{_},IAncestorConverter{_,_},_)" path="/typeparam" />
public interface ITreeAggregator<TElement, TAggregation, TAncestor, TAncestorsAggregation> :
    IAggregator3D<ITree<TElement>, TAggregation, TAncestor, TAncestorsAggregation>,
    IAncestorConverter<ITree<TElement>, TAncestor>
#if NET9_0_OR_GREATER
    where TElement : allows ref struct
    where TAggregation : allows ref struct
    where TAncestor : allows ref struct
    where TAncestorsAggregation : allows ref struct
#endif
{
}
