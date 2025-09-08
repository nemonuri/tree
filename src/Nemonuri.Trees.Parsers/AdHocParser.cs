using Nemonuri.Trees.Abstractions;

namespace Nemonuri.Trees.Parsers;

internal class AdHocParser<TChar> : IParser<TChar>
{
    private readonly Func<IString<TChar>, int, IParser<TChar>, IEnumerable<ISyntaxTree<TChar>>> _parseImplementation;

    public AdHocParser(Func<IString<TChar>, int, IParser<TChar>, IEnumerable<ISyntaxTree<TChar>>> parseImplementation)
    {
        Guard.IsNotNull(parseImplementation);
        _parseImplementation = parseImplementation;
    }

    public ISyntaxForest<TChar> Parse(IString<TChar> @string, int offset)
    {
        return _parseImplementation(@string, offset, this).ToSyntaxTree();
    }

    public NullAggregation Value => default;

    public IEnumerable<IParser<TChar>> Children => [];

    public bool TryGetParent([NotNullWhen(true)] out IParser<TChar>? parent)
    {
        parent = default;
        return false;
    }
}
