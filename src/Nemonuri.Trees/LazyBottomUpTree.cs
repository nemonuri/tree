

namespace Nemonuri.Trees;

internal class LazyBottomUpTree<TElement> : ITree<TElement>
{
    private bool _hasValue;
    private TElement? _value;

    private readonly ILazyTreeValueEvaluator<TElement> _lazyTreeValueEvaluator;
    private readonly IEnumerable<ITree<TElement>> _originalChildren;
    private readonly IParentTreeBinder<TElement> _parentTreeBinder;

    private IEnumerable<ITree<TElement>>? _children;

    public LazyBottomUpTree
    (
        ILazyTreeValueEvaluator<TElement> lazyTreeValueEvaluator,
        IEnumerable<ITree<TElement>>? children,
        IParentTreeBinder<TElement> parentTreeBinder
    )
    {
        _hasValue = false;
        _lazyTreeValueEvaluator = lazyTreeValueEvaluator;
        _originalChildren = children ?? [];
        _parentTreeBinder = parentTreeBinder;
    }

    public TElement Value
    {
        get
        {
            if (!_hasValue)
            {
                _value = _lazyTreeValueEvaluator.Evaluate(_originalChildren);
                _hasValue = true;
            }
            Debug.Assert(_value is not null);
            return _value;
        }
    }

    public IEnumerable<ITree<TElement>> Children =>
        _children ??= _originalChildren.Select(child => _parentTreeBinder.BindParent(child, this));

    public bool TryGetParent([NotNullWhen(true)] out ITree<TElement>? parent)
    {
        parent = default;
        return false;
    }
}
