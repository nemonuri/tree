namespace Nemonuri.Trees.Forests;

public interface IMultiAxisTree<TFirstAxis, TMultiAxis> :
    ITree<TFirstAxis>
    where TFirstAxis : ITree<TFirstAxis>
    where TMultiAxis : IMultiAxisTree<TFirstAxis, TMultiAxis>
{
    int AxisCount { get; }
    IEnumerable<TMultiAxis> GetChildrenFromAxis(int axisIndex);
}
