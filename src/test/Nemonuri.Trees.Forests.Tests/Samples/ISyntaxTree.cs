using System.Collections.Immutable;
using CommunityToolkit.Diagnostics;
using Nemonuri.Trees.Abstractions;

namespace Nemonuri.Trees.Forests.Tests.Samples;





public interface ISyntaxTree :
    IBottomUpRoseTree<GrammarForest, ISyntaxTree>
{
}

public class SyntaxTree : ISyntaxTree
{
    private readonly GrammarForest _grammarTree;
    private readonly ISyntaxTree? _parent;
    private readonly IEnumerable<ISyntaxTree> _unboundChildren;
    private IEnumerable<ISyntaxTree>? _childrenCache;

    public SyntaxTree
    (
        GrammarForest grammarTree,
        ISyntaxTree? parent,
        IEnumerable<ISyntaxTree> unboundChildren,
        IEnumerable<ISyntaxTree>? childrenCache
    )
    {
        _grammarTree = grammarTree;
        _parent = parent;
        _unboundChildren = unboundChildren;
        _childrenCache = childrenCache;
    }

    public IEnumerable<ISyntaxTree> UnboundChildren => _unboundChildren;

    public ISyntaxTree BindParent(ISyntaxTree parent)
    {
        return new SyntaxTree(_grammarTree, _parent, _unboundChildren, _childrenCache);
    }

    public GrammarForest Value => _grammarTree;

    public IEnumerable<ISyntaxTree> Children => _childrenCache ??=
        _unboundChildren.Select(child => child.BindParent(this));

    public bool HasParent => _parent is not null;

    public ISyntaxTree GetParent()
    {
        Guard.IsNotNull(_parent);
        return _parent;
    }
}

public record SyntaxForestValue(GrammarForest GrammarForest, UnionedMatchInfo MatchInfo);

public class SyntaxForest :
    IMultiAxisTree<SyntaxForest>,
    ISupportMultiAxisUnboundChildren<SyntaxForest>,
    IBoundableTree<SyntaxForest, SyntaxForest>,
    IBinderTree<SyntaxForest>,
    IRoseTree<SyntaxForestValue, SyntaxForest>
{
    public SyntaxForestValue Value { get; }
    private readonly IReadOnlyList<IEnumerable<SyntaxForest>> _unboundChildrenList;
    private IReadOnlyList<IEnumerable<SyntaxForest>>? _childrenListCache;
    private readonly SyntaxForest? _parent;

    public SyntaxForest
    (
        SyntaxForestValue value,
        IReadOnlyList<IEnumerable<SyntaxForest>> unboundChildrenList,
        IReadOnlyList<IEnumerable<SyntaxForest>>? childrenListCache,
        SyntaxForest? parent
    )
    {
        Guard.IsNotNull(value);
        Guard.IsNotNull(unboundChildrenList);

        Value = value;
        _unboundChildrenList = unboundChildrenList;
        _childrenListCache = childrenListCache;
        _parent = parent;
    }

    public int AxisCount => GrammarForestTheory.AxisCount();

    public IEnumerable<SyntaxForest> GetChildrenFromAxis(int axisIndex) =>
    (
        _childrenListCache ??=
        [
            .._unboundChildrenList.Select(cl => cl.Select(child => child.BindParent(this)))
        ]
    )[axisIndex];

    public IEnumerable<SyntaxForest> Children => GetChildrenFromAxis(0);

    public IReadOnlyList<IEnumerable<SyntaxForest>> UnboundChildrenList => _unboundChildrenList;

    public SyntaxForest BindParent(SyntaxForest parent)
    {
        return new SyntaxForest(Value, UnboundChildrenList, _childrenListCache, parent);
    }

    public bool HasParent => _parent is not null;

    public SyntaxForest GetParent()
    {
        Guard.IsNotNull(_parent);
        return _parent;
    }
}

public static class GrammarForestTheory
{
    public static int AxisCount() => 2;
}
