
namespace Nemonuri.Trees;

public interface ILazyTreeValueEvaluator<TElement, TTree>
    where TTree : ITree<TElement, TTree>
{
    TElement Evaluate(IEnumerable<TTree> children);
}

public interface ILazyTreeValueEvaluator<TElement> : ILazyTreeValueEvaluator<TElement, ITree<TElement>>
{
}

