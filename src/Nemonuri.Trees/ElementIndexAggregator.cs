
namespace Nemonuri.Trees;

public class ElementIndexAggregator : IAggregator<int?, IIndexPath>
{
    private readonly IIndexPathFactory _indexPathFactory;
    private readonly IIndexPath _initialAggregation;

    public ElementIndexAggregator(IIndexPathFactory indexPathFactory)
    {
        Guard.IsNotNull(indexPathFactory);
        _indexPathFactory = indexPathFactory;
        _initialAggregation = _indexPathFactory.Create([]);
    }

    public IIndexPath InitialAggregation => _initialAggregation;

    public IIndexPath Aggregate(IIndexPath aggregation, int? element)
    {
        return element is int i ? aggregation.Concat([i]) : aggregation;
    }
}
