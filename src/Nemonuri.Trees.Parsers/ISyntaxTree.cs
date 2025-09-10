
namespace Nemonuri.Trees.Parsers;


public interface IBottomUpSyntaxTree<TChar> :
    IBinderSyntaxTree<TChar>,
    IBoundableTree<IBottomUpSyntaxTree<TChar>, IBinderSyntaxTree<TChar>>
{ }
