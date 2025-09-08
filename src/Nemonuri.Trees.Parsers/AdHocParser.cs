namespace Nemonuri.Trees.Parsers;

internal class AdHocParser<TChar> : IParser<TChar>
{
    private readonly Func<IString<TChar>, int, IParser<TChar>, IEnumerable<ISyntaxTree<TChar>>> _parseImplementation;
    private readonly IParser<TChar>? _parent;

    public AdHocParser
    (
        Func<IString<TChar>, int, IParser<TChar>, IEnumerable<ISyntaxTree<TChar>>> parseImplementation,
        IParser<TChar>? parent = null
    )
    {
        Guard.IsNotNull(parseImplementation);
        _parseImplementation = parseImplementation;
        _parent = parent;
    }

    public ISyntaxForest<TChar> Parse(IString<TChar> @string, int offset)
    {
        return _parseImplementation(@string, offset, this).ToSyntaxTree();
    }

    public IParser<TChar> Value => this;

    public IEnumerable<IParser<TChar>> Children => [];

    public bool TryGetParent([NotNullWhen(true)] out IParser<TChar>? parent)
    {
        parent = _parent;
        return parent is not null;
    }

    public IParser<TChar> BindParent(IParser<TChar>? settingParent)
    {
        return new AdHocParser<TChar>(_parseImplementation, this);
    }
}
