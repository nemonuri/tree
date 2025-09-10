
namespace Nemonuri.Trees.Parsers.Tests.Samples;

public class StartWithParser : 
    IParser<char, SourceString, SampleSyntaxForestBuilder, StartWithParser>,
    IGeneralStartWithParser
{
    private readonly string _startWithSource;

    public StartWithParser(string startWithSource)
    {
        _startWithSource = startWithSource;
    }

    public string StartWithSource => _startWithSource;

    public SampleSyntaxForestBuilder Parse(SourceString sourceString, Range sourceRange)
    {
        bool matched = sourceString[sourceRange].InternalString.StartsWith(_startWithSource);
        return new SampleSyntaxForestBuilder(sourceString, sourceRange, this, matched ? [_startWithSource.Length] : []);
    }
}
