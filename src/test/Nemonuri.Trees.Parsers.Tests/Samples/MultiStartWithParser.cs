
namespace Nemonuri.Trees.Parsers.Tests.Samples;

public class MultiStartWithParser : 
    IParser<char, SourceString, SampleSyntaxForestBuilder, MultiStartWithParser>,
    IGeneralStartWithParser
{
    private readonly IEnumerable<string> _startWithSources;

    public MultiStartWithParser(IEnumerable<string> startWithSources)
    {
        _startWithSources = startWithSources;
    }

    public SampleSyntaxForestBuilder Parse(SourceString sourceString, Range sourceRange)
    {
        string source = sourceString[sourceRange].InternalString;

        return new SampleSyntaxForestBuilder
        (
            sourceString, sourceRange, this,
            _startWithSources.Select(sw => GetMatchLength(source, sw)).Where(i => i.HasValue).Select(i => i!.Value)
        );
    }

    private static int? GetMatchLength(string source, string startWith)
    {
        return source.StartsWith(startWith) ? startWith.Length : null;
    }
}
