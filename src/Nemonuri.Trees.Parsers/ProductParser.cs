

namespace Nemonuri.Trees.Parsers;

internal class ProductParser<TChar> : IParser<TChar>
{
    private readonly IEnumerable<IParser<TChar>> _parserSequence;
    private readonly ILazyTreeValueEvaluator<SyntaxTreeInfo<TChar>, ISyntaxTree<TChar>> _lazyTreeValueEvaluator;
    private readonly IParser<TChar>? _parent;
    private IEnumerable<IParser<TChar>>? _children;

    public ProductParser
    (
        IEnumerable<IParser<TChar>> parserSequence,
        ILazyTreeValueEvaluator<SyntaxTreeInfo<TChar>, ISyntaxTree<TChar>> lazyTreeValueEvaluator,
        IParser<TChar>? parent = null
    )
    {
        Guard.IsNotNull(parserSequence);
        Guard.IsNotNull(lazyTreeValueEvaluator);

        _parserSequence = parserSequence;
        _lazyTreeValueEvaluator = lazyTreeValueEvaluator;
        _parent = parent;
    }

    public ISyntaxForest<TChar> Parse(IString<TChar> @string, int offset)
    {
        throw new NotImplementedException();
    }

    public IParser<TChar> Value => this;

    public IEnumerable<IParser<TChar>> Children => _children ??= _parserSequence.Select(child => child.BindParent(this));

    public bool TryGetBoundParent([NotNullWhen(true)] out IParser<TChar>? parent)
    {
        parent = _parent;
        return parent is not null;
    }

    public IParser<TChar> BindParent(IParser<TChar>? settingParent)
    {
        return new ProductParser<TChar>(_parserSequence, _lazyTreeValueEvaluator, settingParent);
    }
}
