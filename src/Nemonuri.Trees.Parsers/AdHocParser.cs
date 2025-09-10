namespace Nemonuri.Trees.Parsers;

internal class AdHocParser<TChar> : IParserNode<TChar>
{
    private readonly Func<IString<TChar>, int, IParserNode<TChar>, IEnumerable<IBinderSyntaxTree<TChar>>> _parseImplementation;
    private readonly IParserNode<TChar>? _parent;

    public AdHocParser
    (
        Func<IString<TChar>, int, IParserNode<TChar>, IEnumerable<IBinderSyntaxTree<TChar>>> parseImplementation,
        IParserNode<TChar>? parent = null
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

    public IParserNode<TChar> Value => this;

    public IEnumerable<IParserNode<TChar>> Children => [];

    public bool TryGetBoundParent([NotNullWhen(true)] out IParserNode<TChar>? parent)
    {
        parent = _parent;
        return parent is not null;
    }

    public IParserNode<TChar> BindParent(IParserNode<TChar>? settingParent)
    {
        return new AdHocParser<TChar>(_parseImplementation, this);
    }
}
