
namespace Nemonuri.Trees.Parsers.Tests.Samples;

public class SampleSyntaxForestBuilder : ISyntaxForestBuilder<char, SourceString, SampleSyntaxForestBuilder, IGeneralStartWithParser>
{
    public SampleSyntaxForestBuilder(SourceString sourceString, Range sourceRange, IGeneralStartWithParser parser, IEnumerable<int> parsedLengths)
    {
        SourceString = sourceString;
        SourceRange = sourceRange;
        Parser = parser;
        MatchLengths = parsedLengths;
    }

    public SourceString SourceString { get; }

    public Range SourceRange { get; }

    public IGeneralStartWithParser Parser { get; }

    public IEnumerable<int> MatchLengths { get; }
}