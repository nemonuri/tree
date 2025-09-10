#if false

namespace Nemonuri.Trees.Parsers;

public interface IGeneralParser<TChar> :
    IParser<TChar, IGeneralString<TChar>, IGeneralSyntaxNode<TChar>, IGeneralParser<TChar>>
{

}

public interface IGeneralSyntaxNode<TChar> :
    ISyntaxNodeBuilder<TChar, IGeneralString<TChar>, IGeneralSyntaxNode<TChar>, IGeneralParser<TChar>>
{

}

#endif