namespace Nemonuri.Trees.Paths;

public interface ISupportTryGetItemFromIndexPath<TItem>
{
    bool TryGetItemFromIndexPath
    (
        IIndexPath indexPath,
        [NotNullWhen(true)] out TItem? result
    );
}
