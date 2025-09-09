#if false

namespace Nemonuri.Trees;

internal class ParentTreeBinder<TElement> : IParentTreeBinder<TElement>
{
    public readonly static ParentTreeBinder<TElement> Instance = new();

    public ParentTreeBinder() { }

    public IBinderRoseTree<TElement> BindParent(IBinderRoseTree<TElement> source, IBinderRoseTree<TElement>? settingParent)
    {
        return TreeTheory.BindParent(source, settingParent);
    }
}

#endif