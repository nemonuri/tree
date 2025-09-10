
namespace Nemonuri.Trees.Parsers;

internal class SyntaxBranch<TChar> : IBinderSyntaxTree<TChar>
{
    public SyntaxTreeInfo<TChar> Value { get; }

    private readonly IEnumerable<IBinderSyntaxTree<TChar>> _originalChildren;
    private readonly IBinderSyntaxTree<TChar>? _parent;
    private IEnumerable<IBinderSyntaxTree<TChar>>? _children = null;

    public SyntaxBranch(SyntaxTreeInfo<TChar> value, IEnumerable<IBinderSyntaxTree<TChar>> children, IBinderSyntaxTree<TChar>? parent)
    {
        Value = value;
        _originalChildren = children;
        _parent = parent;
    }

    public IEnumerable<IBinderSyntaxTree<TChar>> Children => _children ??= _originalChildren.Select(child => child.BindParent(this));

    public bool TryGetBoundParent([NotNullWhen(true)] out IBinderSyntaxTree<TChar>? parent)
    {
        parent = _parent;
        return _parent is not null;
    }

    public IBinderSyntaxTree<TChar> BindParent(IBinderSyntaxTree<TChar>? settingParent)
    {
        return new SyntaxBranch<TChar>(Value, _originalChildren, settingParent);
    }
}