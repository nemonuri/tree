namespace Nemonuri.Trees.Parsers;

public class SyntaxTreeInfo<TChar>
{
    public SyntaxTreeInfo(IString<TChar> @string, IParserNode<TChar> parser)
    {
        String = @string;
        Parser = parser;
    }

    public IString<TChar> String { get; }
    public IParserNode<TChar> Parser { get; }
}