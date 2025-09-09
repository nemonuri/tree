
namespace Nemonuri.Trees.Parsers;

public interface ISyntaxTree<TChar> : 
    IBinderRoseTree<SyntaxTreeInfo<TChar>, ISyntaxTree<TChar>>,
    IParentTreeBindable<SyntaxTreeInfo<TChar>, ISyntaxTree<TChar>>
{
}
