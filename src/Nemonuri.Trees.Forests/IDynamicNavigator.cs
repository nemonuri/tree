namespace Nemonuri.Trees.Forests;

public interface IDynamicNavigator<TNode, TFlowAggregation, TAggregation>
{
    bool TryGetFirstChildOfNextChildren(TNode node, TFlowAggregation flowAggregation, TAggregation childrenUnion, [NotNullWhen(true)] out TNode? firstChildNode);
    
    bool TryGetNextSibling(TNode node, TFlowAggregation flowAggregation, TAggregation siblingSequence, [NotNullWhen(true)] out TNode? nextSiblingNode);
}
