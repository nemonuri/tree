namespace Nemonuri.Trees.Parsers;

public interface ISyntaxForest<TChar> : 
    IBinderRoseTree<ISyntaxTree<TChar>, ISyntaxForest<TChar>>,
    ISupportUnboundChildren<ISyntaxForest<TChar>>
{
}

public interface IBottomUpSyntaxForest<TChar> :
    ISyntaxForest<TChar>,
    IBoundableTree<IBottomUpSyntaxForest<TChar>, ISyntaxForest<TChar>>
{ }