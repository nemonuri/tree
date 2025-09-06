namespace Nemonuri.Trees;

public interface IIndexPathFactory
{
    IIndexPath Create(IEnumerable<int> indexes);
}