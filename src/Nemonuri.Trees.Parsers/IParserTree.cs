namespace Nemonuri.Trees.Parsers;

public interface IParserTree<out TChar, in TString, out TSyntax, TParser> :
    IParser<TChar, TString, TSyntax, TParser>,
    IBinderTree<TParser>
    where TString : IString<TChar, TString>
    where TParser : IParserTree<TChar, TString, TSyntax, TParser>
{ }

public interface ISyntaxForestBuilderTree<out TChar, out TString, out TSyntax, out TParser> :
    ISyntaxForestBuilder<TChar, TString, TSyntax, TParser>,
    IBinderTree<TSyntax>
    where TString : IString<TChar, TString>
    where TParser : IParser<TChar, TString, TSyntax, TParser>
    where TSyntax : ISyntaxForestBuilderTree<TChar, TString, TSyntax, TParser>
{
}

#if false
public interface IBottomUpParser<TChar> :
    IParserTree<TChar>,
    IBoundableTree<IBottomUpParser<TChar>, IParserTree<TChar>>
{
}
#endif
