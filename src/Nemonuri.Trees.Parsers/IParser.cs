namespace Nemonuri.Trees.Parsers;

public interface IParser<TChar> : ITree<IParser<TChar>>
{
    ISyntaxNode<TChar> Parse(IString<TChar> @string, int offset);
}

public interface IParserTree<TChar> : 
    IParser<TChar>,
    IBinderTree<IParserTree<TChar>>,
    ISupportUnboundChildren<IParser<TChar>>
{ }

public interface IBottomUpParser<TChar> :
    IParserTree<TChar>,
    IBoundableTree<IBottomUpParser<TChar>, IParserTree<TChar>>
{
}

#if false
public class SumParser<TChar, TInfo> : IParser<TChar, TInfo>
{

}

#endif