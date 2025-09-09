namespace Nemonuri.Trees;

public interface ISupportUnboundChildren<out TUnbound>
    where TUnbound : ITree<TUnbound>
#if NET9_0_OR_GREATER
    , allows ref struct
#endif
{
    IEnumerable<TUnbound> UnboundChildren { get; }
}
