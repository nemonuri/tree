namespace Nemonuri.Trees.Parsers;

public interface IParser<out TChar, in TString, out TSyntax, out TParser>
    where TString : IString<TChar, TString>
    where TParser : IParser<TChar, TString, TSyntax, TParser>
{
    TSyntax Parse(TString sourceString, Range sourceRange);
}

public interface ISyntaxForestBuilder<out TChar, out TString, out TSyntax, out TParser>
    where TString : IString<TChar, TString>
    where TParser : IParser<TChar, TString, TSyntax, TParser>
    where TSyntax : ISyntaxForestBuilder<TChar, TString, TSyntax, TParser>
{
    TString SourceString { get; }
    Range SourceRange { get; }
    TParser Parser { get; }
    IEnumerable<int> MatchLengths { get; }
}
