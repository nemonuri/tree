

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

internal class SyntaxBranch<TChar> : ISyntaxTree<TChar>
{
    public SyntaxTreeInfo<TChar> Value { get; }
    public IEnumerable<ISyntaxTree<TChar>> Children { get; }
    private readonly ISyntaxTree<TChar>? _parent;

    public SyntaxBranch(SyntaxTreeInfo<TChar> value, IEnumerable<ISyntaxTree<TChar>> children, ISyntaxTree<TChar>? parent)
    {
        Value = value;
        Children = children;
        _parent = parent;
    }

    public bool TryGetParent([NotNullWhen(true)] out ISyntaxTree<TChar>? parent)
    {
        throw new NotImplementedException();
    }

    public ISyntaxTree<TChar> BindParent(ISyntaxTree<TChar>? settingParent)
    {
        throw new NotImplementedException();
    }
}