using CommunityToolkit.Diagnostics;

namespace Nemonuri.Trees.Forests.Tests.Samples;

public class GrammarTree :
    IBottomUpRoseTree<StringOrSyntaxTreeListGrammar, GrammarTree>
{
    private readonly StringOrSyntaxTreeListGrammar _grammarValue;
    private readonly GrammarTree? _parent;
    private readonly IEnumerable<GrammarTree> _unboundChildren;
    private IEnumerable<GrammarTree>? _childrenCache;

    public GrammarTree
    (
        StringOrSyntaxTreeListGrammar grammarValue,
        GrammarTree? parent,
        IEnumerable<GrammarTree> unboundChildren,
        IEnumerable<GrammarTree>? childrenCache
    )
    {
        _grammarValue = grammarValue;
        _parent = parent;
        _unboundChildren = unboundChildren;
        _childrenCache = childrenCache;
    }

    public GrammarTree BindParent(GrammarTree parent)
    {
        return new(_grammarValue, _parent, _unboundChildren, _childrenCache);
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

    public StringOrSyntaxTreeListGrammar Value => _grammarValue;

    public SyntaxTree Match(string source, int offset, int length)
    {
        throw new NotImplementedException();
    }

    public SyntaxTree Match(IReadOnlyList<ISyntaxTree> source, int offset, int length)
    { 
        throw new NotImplementedException();
    }

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
