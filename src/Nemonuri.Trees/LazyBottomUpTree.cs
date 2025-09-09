#if false

namespace Nemonuri.Trees;

internal class LazyBottomUpTree<TElement> : IBinderRoseTree<TElement>
{
    private bool _hasValue;
    private TElement? _value;

    private readonly ILazyTreeValueEvaluator<TElement> _lazyTreeValueEvaluator;
    private readonly IEnumerable<IBinderRoseTree<TElement>> _originalChildren;
    private readonly IParentTreeBinder<TElement> _parentTreeBinder;

    private IEnumerable<IBinderRoseTree<TElement>>? _children;

    public LazyBottomUpTree
    (
        ILazyTreeValueEvaluator<TElement> lazyTreeValueEvaluator,
        IEnumerable<IBinderRoseTree<TElement>>? children,
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

    public IEnumerable<IBinderRoseTree<TElement>> Children =>
        _children ??= _originalChildren.Select(child => _parentTreeBinder.BindParent(child, this));

    public bool TryGetBoundParent([NotNullWhen(true)] out IBinderRoseTree<TElement>? parent)
    {
        parent = default;
        return false;
    }
}

#endif