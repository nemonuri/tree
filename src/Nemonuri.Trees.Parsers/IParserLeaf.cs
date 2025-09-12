using Nemonuri.Trees.Forests;

namespace Nemonuri.Trees.Parsers;

public interface IParserLeaf<out TChar, in TString, out TSyntaxLeaf, out TParserLeaf>
    where TString : IString<TChar, TString>
    where TParserLeaf : IParserLeaf<TChar, TString, TSyntaxLeaf, TParserLeaf>
    where TSyntaxLeaf : ISyntaxLeaf<TChar, TString, TSyntaxLeaf, TParserLeaf>
{
    TSyntaxLeaf Parse(TString sourceString, Range sourceRange);
}

public interface ISyntaxLeaf<out TChar, out TString, out TSyntaxLeaf, out TParserLeaf>
    where TString : IString<TChar, TString>
    where TParserLeaf : IParserLeaf<TChar, TString, TSyntaxLeaf, TParserLeaf>
    where TSyntaxLeaf : ISyntaxLeaf<TChar, TString, TSyntaxLeaf, TParserLeaf>
{
    TString SourceString { get; }
    Range SourceRange { get; }
    TParserLeaf ParserLeaf { get; }
    int MatchLength { get; }
}

