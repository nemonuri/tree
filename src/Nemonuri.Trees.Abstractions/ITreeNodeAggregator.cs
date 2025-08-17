// TODO: Delete
#if false
namespace Nemonuri.Trees;

public interface ITreeNodeAggregator<TTreeNode, TTarget, TContext>
{
    IAggregator<TreeNodeWithIndex<TTreeNode>, TContext> ContextAggregator { get; }

    IAggregator2DWithContext<TTreeNode, TTarget, TContext> TargetAggregator { get; }
}
#endif