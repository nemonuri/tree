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

public static class GrammarForestTheory
{
    public static int AxisCount() => 2;
}
