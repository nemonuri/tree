#if false

namespace Nemonuri.Trees;

internal class ParentBoundTree<TTree, TParent> :
    IBinderTree,
    ISupportUnboundChildren<TTree>,
    ISupportUnbound<TTree>,
    ISupportUnboundParent<TParent>
    where TTree : ITree<TTree>
    where TParent : class, ITree<TParent>
{
    private readonly TParent? _parent;
    private readonly TTree _unbound;

    private IEnumerable<IBinderTree>? _children;

    public ParentBoundTree(TTree unbound, TParent? parent)
    {
        Guard.IsNotNull(unbound);

        _parent = parent;
        _unbound = unbound;
    }

    public bool TryGetBoundParent([NotNullWhen(true)] out IBinderTree? parent)
    {
        if (!TryGetUnboundParent(out var unboundParent))
        {
            parent = default; return false;
        }

        if (unboundParent is IBinderTree binderTree)
        {
            parent = binderTree; return true;
        }


    }

    public bool TryGetUnboundParent([NotNullWhen(true)] out TParent? parent)
    {
        parent = _parent;
        return parent is not null;
    }

    public TTree Unbound => _unbound;

    public IEnumerable<TTree> UnboundChildren => _unbound.Children;

    public IEnumerable<IBinderTree> Children => _children ??=
        UnboundChildren.Select
        (
            child =>
            {
                var unbound = child is ISupportUnbound<TTree> supportUnbound ?
                    supportUnbound.Unbound : child;
                return new ParentBoundTree<TTree>(child, this);
            }
        );

}

#endif