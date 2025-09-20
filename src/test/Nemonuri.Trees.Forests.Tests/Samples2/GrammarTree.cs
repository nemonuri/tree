using CommunityToolkit.Diagnostics;

namespace Nemonuri.Trees.Forests.Tests.Samples2;

public class GrammarTree : IBottomUpRoseTree<Grammar, GrammarTree>
{
    public Grammar Value { get; }
    private readonly GrammarTree? _parent;
    private readonly IEnumerable<GrammarTree> _unboundChildren;
    private IEnumerable<GrammarTree>? _childrenCache;

    public GrammarTree
    (
        Grammar value,
        IEnumerable<GrammarTree> unboundChildren,
        IEnumerable<GrammarTree>? childrenCache,
        GrammarTree? parent
    )
    {
        Value = value;
        _parent = parent;
        _unboundChildren = unboundChildren;
        _childrenCache = childrenCache;
    }

    public GrammarTree
    (
        Grammar value,
        IEnumerable<GrammarTree> unboundChildren
    ) : this(value, unboundChildren, default, default)
    { }

    public IEnumerable<GrammarTree> UnboundChildren => _unboundChildren;

    public GrammarTree BindParent(GrammarTree parent)
    {
        return new(Value, _unboundChildren, _childrenCache, parent);
    }

    public IEnumerable<GrammarTree> Children => _childrenCache ??=
        TreeTheory.CreateBoundChildren(_unboundChildren, this);

    public bool HasParent => _parent is not null;

    public GrammarTree GetParent() => _parent ?? ThrowHelper.ThrowArgumentNullException<GrammarTree>();
}

public abstract class GrammarFactory
{
    protected GrammarFactory(string factoryLabel)
    {
        Guard.IsNotNull(factoryLabel);
        FactoryLabel = factoryLabel;
    }

    public string FactoryLabel { get; }
}

public class SumGrammarFactory : GrammarFactory
{
    public IEnumerable<GrammarTree> GrammarTrees { get; }

    public SumGrammarFactory(string factoryLabel, IEnumerable<GrammarTree> grammarTrees) :
        base(factoryLabel)
    {
        GrammarTrees = grammarTrees;
    }


}


