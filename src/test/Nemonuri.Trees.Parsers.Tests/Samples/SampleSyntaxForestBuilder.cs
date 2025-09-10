
namespace Nemonuri.Trees.Parsers.Tests.Samples;

public class SampleSyntaxForestBuilder : ISyntaxForestBuilder<char, SourceString, SampleSyntaxForestBuilder, IGeneralStartWithParser>
{
    public SampleSyntaxForestBuilder
    (
        SourceString sourceString, Range sourceRange,
        IGeneralStartWithParser parser, IEnumerable<MatchInfo> matchInfos
    )
    {
        SourceString = sourceString;
        SourceRange = sourceRange;
        Parser = parser;
        MatchInfos = matchInfos;
    }

    public SourceString SourceString { get; }

    public Range SourceRange { get; }

    public IGeneralStartWithParser Parser { get; }

    public IEnumerable<MatchInfo> MatchInfos { get; }
}