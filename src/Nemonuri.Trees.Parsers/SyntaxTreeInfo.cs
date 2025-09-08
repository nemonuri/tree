namespace Nemonuri.Trees.Parsers;

public class SyntaxTreeInfo<TChar>
{
    public SyntaxTreeInfo(IString<TChar> @string, IParser<TChar> parser)
    {
        String = @string;
        Parser = parser;
    }

    public IString<TChar> String { get; }
    public IParser<TChar> Parser { get; }
}