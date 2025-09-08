

namespace Nemonuri.Trees;

public interface ILazyTreeValueEvaluator<TElement>
{
    TElement Evaluate(IEnumerable<ITree<TElement>> children);
}