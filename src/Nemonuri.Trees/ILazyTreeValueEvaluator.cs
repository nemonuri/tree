
namespace Nemonuri.Trees;

public interface ILazyTreeValueEvaluator<out TValue, TTree>
    where TTree : IRoseTree<TValue, TTree>
{
    TValue Evaluate(IEnumerable<TTree> children);
}