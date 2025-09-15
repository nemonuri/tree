using CommunityToolkit.Diagnostics;

namespace Nemonuri.Trees.Forests.Tests.Samples;

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

    private TextSpan? _textSpanCache;
    public TextSpan TextSpan => _textSpanCache ??= Value.MatchInfo switch
    {
        { IsString: true } and { AsString: var v } => v.Entry.TextSpan,
        { IsSyntaxTreeList: true } => TextSpan.Merge(GetChildrenFromAxis(0).Select(static a => a.TextSpan)),
        _ => TextSpan.None
    };

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
