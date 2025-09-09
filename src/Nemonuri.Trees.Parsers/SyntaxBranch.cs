
namespace Nemonuri.Trees.Parsers;

internal class SyntaxBranch<TChar> : ISyntaxTree<TChar>
{
    public SyntaxTreeInfo<TChar> Value { get; }

    private readonly IEnumerable<ISyntaxTree<TChar>> _originalChildren;
    private readonly ISyntaxTree<TChar>? _parent;
    private IEnumerable<ISyntaxTree<TChar>>? _children = null;

    public SyntaxBranch(SyntaxTreeInfo<TChar> value, IEnumerable<ISyntaxTree<TChar>> children, ISyntaxTree<TChar>? parent)
    {
        Value = value;
        _originalChildren = children;
        _parent = parent;
    }

    public IEnumerable<ISyntaxTree<TChar>> Children => _children ??= _originalChildren.Select(child => child.BindParent(this));

    public bool TryGetBoundParent([NotNullWhen(true)] out ISyntaxTree<TChar>? parent)
    {
        parent = _parent;
        return _parent is not null;
    }

    public ISyntaxTree<TChar> BindParent(ISyntaxTree<TChar>? settingParent)
    {
        return new SyntaxBranch<TChar>(Value, _originalChildren, settingParent);
    }
}