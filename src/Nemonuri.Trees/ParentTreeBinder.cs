
namespace Nemonuri.Trees;

internal class ParentTreeBinder<TElement> : IParentTreeBinder<TElement>
{
    public readonly static ParentTreeBinder<TElement> Instance = new();

    public ParentTreeBinder() { }

    public ITree<TElement> BindParent(ITree<TElement> source, ITree<TElement>? settingParent)
    {
        return TreeTheory.BindParent(source, settingParent);
    }
}