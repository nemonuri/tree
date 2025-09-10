
namespace Nemonuri.Trees.Parsers;

internal class SyntaxLeaf<TChar> : IBinderSyntaxTree<TChar>
{
    public SyntaxTreeInfo<TChar> Value { get; }
    private readonly IBinderSyntaxTree<TChar>? _parent;

    public SyntaxLeaf(SyntaxTreeInfo<TChar> value, IBinderSyntaxTree<TChar>? parent)
    {
        Guard.IsNotNull(value);
        Value = value;
        _parent = parent;
    }

    public IEnumerable<IBinderSyntaxTree<TChar>> Children => [];

    public bool TryGetBoundParent([NotNullWhen(true)] out IBinderSyntaxTree<TChar>? parent)
    {
        parent = _parent;
        return parent is not null;
    }

    public IBinderSyntaxTree<TChar> BindParent(IBinderSyntaxTree<TChar>? parent)
    {
        return new SyntaxLeaf<TChar>(Value, parent);
    }
}
