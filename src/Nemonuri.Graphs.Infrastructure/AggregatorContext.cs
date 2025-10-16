using System.Diagnostics.CodeAnalysis;

namespace Nemonuri.Graphs.Infrastructure;

public ref struct AggregatorContext<TChildren, TDescendants, TChild, TGraph>
{
    private ref TChildren _childrenScopeContext;

    private ref TDescendants _descendantsScopeContext;

    private ref TChild _childScopeContext;

    private ref TGraph _graphScopeContext;

    public AggregatorContext(ref TChildren childrenScopeContext, ref TDescendants descendantsScopeContext, ref TChild childScopeContext, ref TGraph graphScopeContext)
    {
        _descendantsScopeContext = ref descendantsScopeContext;
        _childrenScopeContext = ref childrenScopeContext;
        _childScopeContext = ref childScopeContext;
        _graphScopeContext = ref graphScopeContext;
    }

    public ref TChildren ChildrenScopeContext => ref _childrenScopeContext;
    public void SetChildrenScopeContextRef([UnscopedRef] ref TChildren contextRef) => _childrenScopeContext = ref contextRef;

    public ref TDescendants DescendantsScopeContext => ref _descendantsScopeContext;
    public void SetDescendantsScopeContextRef([UnscopedRef] ref TDescendants contextRef) => _descendantsScopeContext = ref contextRef;

    public ref TChild ChildScopeContext => ref _childScopeContext;
    public void SetChildScopeContextRef([UnscopedRef] ref TChild contextRef) => _childScopeContext = ref contextRef;

    public ref TGraph GraphScopeContext => ref _graphScopeContext;
    public void SetGraphScopeContextRef([UnscopedRef] ref TGraph contextRef) => _graphScopeContext = ref contextRef;
}
