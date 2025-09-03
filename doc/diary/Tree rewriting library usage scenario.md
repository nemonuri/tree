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

private static SyntaxNode CreateSyntaxNode(ITree<SyntaxNode> tree, ImmutableList<ITree<SyntaxNode>> children)
{
    if (!tree.IsRewrited()) { return tree.Root; }

    return (tree, children.Parse(GetParser())) switch 
    {
        (BlockSyntax, [SyntaxList<AttributeListSyntax> v1, SyntaxToken v2, SyntaxList<StatementSyntax> v3, SyntaxToken v4]) => SyntaxFactory.Block(v1, v2, v3, v4),
        // (...)
    }
}
```