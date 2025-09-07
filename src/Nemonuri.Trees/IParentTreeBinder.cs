
namespace Nemonuri.Trees;

public interface IParentTreeBinder<TElement>
#if NET9_0_OR_GREATER
    where TElement : allows ref struct
#endif
{
    ITree<TElement> BindParent(ITree<TElement> source, ITree<TElement>? settingParent);
}
