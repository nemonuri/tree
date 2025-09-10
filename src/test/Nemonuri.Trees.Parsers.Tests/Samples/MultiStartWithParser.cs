using System.Diagnostics.CodeAnalysis;
using Nemonuri.Trees.Paths;

namespace Nemonuri.Trees.Parsers.Tests.Samples;

public class MultiStartWithParser :
    IParser<char, SourceString, SampleSyntaxForestBuilder, MultiStartWithParser>,
    IGeneralStartWithParser
{
    private readonly IReadOnlyList<string> _startWithSources;

    public MultiStartWithParser(IEnumerable<string> startWithSources)
    {
        _startWithSources = [..startWithSources];
    }

    public SampleSyntaxForestBuilder Parse(SourceString sourceString, Range sourceRange)
    {
        string source = sourceString[sourceRange].InternalString;

        return new SampleSyntaxForestBuilder
        (
            sourceString, sourceRange, this,
            _startWithSources.Select((sw, idx) => GetMatchInfo(source, idx, sw)).Where(static mi => mi.IsValid)
        );
    }

    private static MatchInfo GetMatchInfo(string source, int index, string startWith)
    {
        return source.StartsWith(startWith) ? ([index], startWith.Length) : MatchInfo.InValid;
    }

    public bool TryGetItemFromIndexPath(IIndexPath indexPath, [NotNullWhen(true)] out MultiStartWithParser? result) =>
    (
        result = indexPath switch
        {
            [] => this,
            [var i] when (i >= 0) && (i < _startWithSources.Count) => new([_startWithSources[i]]),
            _ => default
        }
    )
    is not null;

    bool ISupportTryGetItemFromIndexPath<IGeneralStartWithParser>.TryGetItemFromIndexPath(IIndexPath indexPath, [NotNullWhen(true)] out IGeneralStartWithParser? result) =>
        (result = TryGetItemFromIndexPath(indexPath, out var v) ? v : default) is not null;
}
