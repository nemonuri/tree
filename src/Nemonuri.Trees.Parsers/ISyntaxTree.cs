
namespace Nemonuri.Trees.Parsers;

public interface ISyntaxTree<TChar> : 
    ITree<SyntaxTreeInfo<TChar>, ISyntaxTree<TChar>>,
    IParentTreeBindable<SyntaxTreeInfo<TChar>, ISyntaxTree<TChar>>
{
}
