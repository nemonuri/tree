namespace Nemonuri.Trees.Forests;

public interface IDynamicNavigator<TNode, TFlowAggregation, TAggregation>
{
    bool TryGetFirstOfFirstChildrenInUnion(TNode node, TFlowAggregation flowAggregation, [NotNullWhen(true)] out TNode? firstChildNode);

    bool TryGetFirstOfNextChildrenInUnion(TNode node, TFlowAggregation flowAggregation, TAggregation childSiblingSequenceUnion, [NotNullWhen(true)] out TNode? firstChildNode);


    bool TryGetAlternativeNextSibling(TNode node, TFlowAggregation flowAggregation, TAggregation siblingSequence, [NotNullWhen(true)] out TNode? nextSiblingNode);
    
    bool TryGetDefaultNextSibling(TNode node, TFlowAggregation flowAggregation, [NotNullWhen(true)] out TNode? nextSiblingNode);
}


public interface IFlowConverter<TNode, TContext, TFlow>
{
    TFlow ConvertToFlow(TNode node, TContext context);
    TFlow ConvertToFlow(TNode parent, TNode node, TContext context, int ordinalInSequenceUnion, int ordinalInSequence);
}
