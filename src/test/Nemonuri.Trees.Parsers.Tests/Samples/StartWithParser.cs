using System.Diagnostics.CodeAnalysis;
using Nemonuri.Trees.Paths;

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
        return new SampleSyntaxForestBuilder(sourceString, sourceRange, this, matched ? [(null, _startWithSource.Length)] : []);
    }

    public bool TryGetItemFromIndexPath(IIndexPath indexPath, [NotNullWhen(true)] out StartWithParser? result) =>
        (result = indexPath.Any() ? default : this) is not null;

    bool ISupportTryGetItemFromIndexPath<IGeneralStartWithParser>.TryGetItemFromIndexPath(IIndexPath indexPath, [NotNullWhen(true)] out IGeneralStartWithParser? result) =>
        (result = TryGetItemFromIndexPath(indexPath, out var v) ? v : default) is not null;
}
