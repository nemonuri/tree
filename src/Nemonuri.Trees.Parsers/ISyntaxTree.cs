
namespace Nemonuri.Trees.Parsers;

public interface ISyntaxNode<TChar> :
    IRoseTree<IParser<TChar>, ISyntaxNode<TChar>>,
    IString<TChar>,
    ISupportChildren<CharOrString<TChar, ISyntaxNode<TChar>>>
{
}

public interface ISyntaxTree<TChar> : 
    ISyntaxNode<TChar>,
    IBinderRoseTree<IParserTree<TChar>, ISyntaxTree<TChar>>,
    ISupportUnboundChildren<ISyntaxNode<TChar>>
{
}

public interface IBottomUpSyntaxTree<TChar> :
    ISyntaxTree<TChar>,
    IBoundableTree<IBottomUpSyntaxTree<TChar>, ISyntaxTree<TChar>>
{ }
