namespace Nemonuri.Trees;

public interface IParentProvider<TNode>
#if NET9_0_OR_GREATER
    where TNode : allows ref struct
#endif
{ 
    bool TryGetParent(TNode source, [NotNullWhen(true)] out TNode? parent);
}