using CommunityToolkit.Diagnostics;

namespace Nemonuri.Trees.Forests.Tests.Samples;

public class GrammarTree :
    IBottomUpRoseTree<StringOrSyntaxTreeListGrammar, GrammarTree>
{
    private readonly IStringGrammar? _leafValue;
    private readonly ISyntaxTreeListGrammar? _branchValue;
    private readonly GrammarTree? _parent;
    private readonly IEnumerable<GrammarTree> _unboundChildren;
    private IEnumerable<GrammarTree>? _childrenCache;

    private GrammarTree
    (
        IStringGrammar? leafValue,
        ISyntaxTreeListGrammar? branchValue,
        GrammarTree? parent,
        IEnumerable<GrammarTree> unboundChildren,
        IEnumerable<GrammarTree>? childrenCache
    )
    {
        Guard.IsTrue((leafValue is { } && branchValue is null) || (leafValue is null && branchValue is { }));
        Guard.IsTrue(leafValue is null || !unboundChildren.Any());
        Guard.IsTrue(branchValue is null || unboundChildren.Any());

        _leafValue = leafValue;
        _branchValue = branchValue;
        _parent = parent;
        _unboundChildren = unboundChildren;
        _childrenCache = childrenCache;
    }

    public GrammarTree
    (
        IStringGrammar leafValue,
        GrammarTree? parent
    )
    : this(leafValue, null, parent, [], null)
    { }

    public GrammarTree
    (
        ISyntaxTreeListGrammar branchValue,
        GrammarTree? parent,
        IEnumerable<GrammarTree> unboundChildren,
        IEnumerable<GrammarTree>? childrenCache
    )
    : this(null, branchValue, parent, unboundChildren, childrenCache)
    { }

    public GrammarTree BindParent(GrammarTree parent)
    {
        return new(_leafValue, _branchValue, _parent, _unboundChildren, _childrenCache);
    }

    public IEnumerable<GrammarTree> Children => _childrenCache ??=
        _unboundChildren.Select(child => child.BindParent(this));

    public bool HasParent => _parent is not null;

    public GrammarTree GetParent()
    {
        Guard.IsNotNull(_parent);
        return _parent;
    }

    public IEnumerable<GrammarTree> UnboundChildren => _unboundChildren;

    public bool IsBranch => Children.Any();

    public ISyntaxTreeListGrammar GetBranchValue()
    {
        Guard.IsNotNull(_branchValue);
        return _branchValue;
    }

    public bool IsLeaf => !Children.Any();

    public IStringGrammar GetLeafValue()
    {
        Guard.IsNotNull(_leafValue);
        return _leafValue;
    }

    public StringOrSyntaxTreeListGrammar Value => throw new NotImplementedException();

#if false
    public SyntaxForest Match(string source, int offset, int length)
    {
        var aggregation =
        AggregatingTheory.Aggregate<GrammarTree, ImmutableList<SyntaxForest>>
        (
            new AdHocAggregator2D<GrammarTree, ImmutableList<SyntaxForest>>
            (
                initialAggregationImplementation: static () => [],
                aggregateImplementation: (s, c, n) =>
                {
                    int matchResult = n switch
                    {
                        { IsLeaf: true } => n.GetLeafValue().Match(source, )
                    }
                }
            )
        );
    }

    private SyntaxForest Match(IReadOnlyList<ISyntaxTree> source, int offset, int length)
    {

    }
#endif
}
