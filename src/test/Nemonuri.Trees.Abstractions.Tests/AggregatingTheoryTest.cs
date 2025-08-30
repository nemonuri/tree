namespace Nemonuri.Trees.Abstractions.Tests;

public class AggregatingTheoryTest
{
    private readonly ITestOutputHelper _outputHelper;

    public AggregatingTheoryTest(ITestOutputHelper outputHelper)
    {
        _outputHelper = outputHelper;
    }

    [Fact]
    public void Aggregate2D()
    {
        // Arrange
        LeafOrBranch<int> node = LeafOrBranch<int>.Branch
        (
            1, [
                1,
                2, (
                3, [
                    4,
                    5]), (
                6, [
                    (7, [
                        9]),
                    8
                ])]
        );
        AdHocAggregator2D<LeafOrBranch<int>, bool> aggregator2D = new
        (
            static () => true,
            (s, c, e) =>
            {
                if ((s || c) is false)
                {
                    _outputHelper.WriteLine($"Previous aggregation is false. s = {s}, c = {c}");
                    return false;
                }
                _outputHelper.WriteLine($"e.GetValue() = {e.GetValue()}");
                return e.GetValue() < 10;
            }
        );

        // Act
        bool actual = AggregatingTheory.Aggregate(aggregator2D, LeafOrBranch<int>.ChildrenProvider, node);

        // Assert
        bool expected = true;
        Assert.Equal(expected, actual);
    }
}
