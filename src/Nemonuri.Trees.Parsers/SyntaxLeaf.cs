
namespace Nemonuri.Trees.Parsers;

internal class SyntaxLeaf<TChar> : ISyntaxTree<TChar>
{
    public SyntaxTreeInfo<TChar> Value { get; }
    private readonly ISyntaxTree<TChar>? _parent;

    public SyntaxLeaf(SyntaxTreeInfo<TChar> value, ISyntaxTree<TChar>? parent)
    {
        Guard.IsNotNull(value);
        Value = value;
        _parent = parent;
    }

    public IEnumerable<ISyntaxTree<TChar>> Children => [];

    public bool TryGetParent([NotNullWhen(true)] out ISyntaxTree<TChar>? parent)
    {
        parent = _parent;
        return parent is not null;
    }

    public ISyntaxTree<TChar> BindParent(ISyntaxTree<TChar>? parent)
    {
        return new SyntaxLeaf<TChar>(Value, parent);
    }
}
