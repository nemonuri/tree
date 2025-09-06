using Nemonuri.Trees.Abstractions;

namespace Nemonuri.Trees;

public interface ITreeFactory<TElement>
#if NET9_0_OR_GREATER
    where TElement : allows ref struct
#endif
{
    ITree<TElement> Create
    (
        TElement root, IChildrenProvider<TElement> childrenProvider, ITree<TElement>? parent
    );
}