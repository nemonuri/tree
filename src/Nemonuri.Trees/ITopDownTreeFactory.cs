using Nemonuri.Trees.Abstractions;

namespace Nemonuri.Trees;

public interface ITopDownTreeFactory<TElement>
#if NET9_0_OR_GREATER
    where TElement : allows ref struct
#endif
{
    ITree<TElement> Create
    (
        TElement value,
        IChildrenProvider<TElement> childrenProvider,
        ITopDownTreeFactory<TElement> childToTreeFactory,
        ITree<TElement>? parent
    );
}