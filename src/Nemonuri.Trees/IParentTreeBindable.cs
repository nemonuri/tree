
namespace Nemonuri.Trees;

public interface IParentTreeBindable<TElement, TTree>
    where TTree : ITree<TElement, TTree>
{ 
    TTree BindParent(TTree? settingParent);
}
