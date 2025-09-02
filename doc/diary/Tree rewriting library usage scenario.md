# Tree rewriting library usage scenario

## Rewrite syntax tree

```csharp
public static SyntaxNode RewriteSyntaxTree(SyntaxTree syntaxTree)
{
    var tree = new Tree<SyntaxNode>(syntaxTree.Root, new SyntaxNodeChildProvider());

    return tree
        .ToArray(IsNeedToRewrite, SelectIndexPath)
        .OrderBy(IndexPathComparer.DepthFirstPostOrder)
        .ToArray()
        .Rewrite(tree, DoRewrite)
        .Select<SyntaxNode>(CreateSyntaxNode)
        ;
}

private static IEnumerable<TreeStructureChangedInfo> DoRewrite(ITree<SyntaxNode> tree, IndexPath indexPath)
{
    // (...)
}

private static SyntaxNode CreateSyntaxNode(SyntaxNode value, ImmutableList<SyntaxNode> children)
{
    // (...)
}
```