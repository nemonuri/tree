
using CommunityToolkit.Diagnostics;

namespace Nemonuri.Trees.Forests.Tests.Samples;

public class GrammarForest :
    IMultiAxisTree<GrammarTree, GrammarForest>
{
    private readonly GrammarTree _firstAxisTree;
    private IEnumerable<GrammarForest>? _wrappedFirstAxisChildCache;
    private readonly IEnumerable<GrammarForest> _secondAxisChildren;

    public GrammarForest
    (
        GrammarTree firstAxisTree,
        IEnumerable<GrammarForest>? secondAxisChildren
    )
    {
        _firstAxisTree = firstAxisTree;
        _secondAxisChildren = secondAxisChildren ?? [];
    }

    public int AxisCount => GrammarForestTheory.AxisCount();

    public IEnumerable<GrammarForest> GetChildrenFromAxis(int axisIndex)
    {
        return axisIndex switch
        {
            0 => _wrappedFirstAxisChildCache ??=
                    Children.Select(static a => new GrammarForest(a, null)),
            1 => _secondAxisChildren,
            _ => ThrowHelper.ThrowArgumentOutOfRangeException<IEnumerable<GrammarForest>>()
        };
    }

    public IEnumerable<GrammarForest> GetChildrenFromAxis(GrammarForestAxisKind axis) =>
        GetChildrenFromAxis((int)axis);

    public IEnumerable<GrammarTree> Children => _firstAxisTree.Children;

    public SyntaxForest Match(string source, int offset, int length)
    {
        throw new NotImplementedException();
    }
}
