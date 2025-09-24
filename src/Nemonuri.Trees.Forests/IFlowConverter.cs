namespace Nemonuri.Trees.Forests;

public interface IFlowConverter<TNode, TFlow>
{
    TFlow ConvertToFlow(TNode node);
    TFlow ConvertToFlow(TNode parent, TNode node, int ordinalInSequenceUnion, int ordinalInSequence);
}
