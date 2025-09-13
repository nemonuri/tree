namespace Nemonuri.Trees.Forests;

public interface IMultiAxisTree<TMultiAxis> :
    ITree<TMultiAxis>
    where TMultiAxis : IMultiAxisTree<TMultiAxis>
#if NET9_0_OR_GREATER
    , allows ref struct
#endif
{
    int AxisCount { get; }
    IEnumerable<TMultiAxis> GetChildrenFromAxis(int axisIndex);
}

public interface ISupportMultiAxisUnboundChildren<out TUnbound>
    where TUnbound : ITree<TUnbound>
#if NET9_0_OR_GREATER
    , allows ref struct
#endif
{
    IReadOnlyList<IEnumerable<TUnbound>> UnboundChildrenList { get; }
}

