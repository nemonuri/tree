namespace Nemonuri.Trees.Abstractions.Tests;

[UnionCase("Leaf", "T")]
[UnionCase("Branch", "(T, LeafOrBranch<T>[])")]
public partial class LeafOrBranch<T>
{
    public readonly static AdHocChildrenProvider<LeafOrBranch<T>> ChildrenProvider = new
    (
        static n => n.IsLeaf ? [] : n.AsBranch.Item2
    );

    public T GetValue() => IsLeaf ? AsLeaf : AsBranch.Item1;
}