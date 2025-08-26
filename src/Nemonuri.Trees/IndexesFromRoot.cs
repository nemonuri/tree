
namespace Nemonuri.Trees;

public interface IIndexesFromRoot<TTreeNode> : IHasIndexes
{
    TTreeNode? Root { get; }
}

public readonly struct IndexesFromRoot<TTreeNode> : IIndexesFromRoot<TTreeNode>
{
    private readonly TTreeNode? _root;
    private readonly ImmutableList<int>? _indexSequence;

    public IndexesFromRoot(TTreeNode? root, ImmutableList<int>? indexSequence)
    {
        _root = root;
        _indexSequence = indexSequence;
    }

    public TTreeNode? Root => _root;

    public IEnumerable<int> Indexes => _indexSequence ?? [];
}