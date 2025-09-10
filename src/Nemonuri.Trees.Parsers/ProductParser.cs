

namespace Nemonuri.Trees.Parsers;

internal class ProductParser<TChar> : IParserNode<TChar>
{
    private readonly IEnumerable<IParserNode<TChar>> _parserSequence;
    private readonly ILazyTreeValueEvaluator<SyntaxTreeInfo<TChar>, IBinderSyntaxTree<TChar>> _lazyTreeValueEvaluator;
    private readonly IParserNode<TChar>? _parent;
    private IEnumerable<IParserNode<TChar>>? _children;

    public ProductParser
    (
        IEnumerable<IParserNode<TChar>> parserSequence,
        ILazyTreeValueEvaluator<SyntaxTreeInfo<TChar>, IBinderSyntaxTree<TChar>> lazyTreeValueEvaluator,
        IParserNode<TChar>? parent = null
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

    public IParserNode<TChar> Value => this;

    public IEnumerable<IParserNode<TChar>> Children => _children ??= _parserSequence.Select(child => child.BindParent(this));

    public bool TryGetBoundParent([NotNullWhen(true)] out IParserNode<TChar>? parent)
    {
        parent = _parent;
        return parent is not null;
    }

    public IParserNode<TChar> BindParent(IParserNode<TChar>? settingParent)
    {
        return new ProductParser<TChar>(_parserSequence, _lazyTreeValueEvaluator, settingParent);
    }
}


#if false
public class SumParser<TChar, TInfo> : IParser<TChar, TInfo>
{

}

#endif