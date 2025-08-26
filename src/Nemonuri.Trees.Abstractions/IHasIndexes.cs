namespace Nemonuri.Trees;

public interface IHasIndexes
{ 
    IEnumerable<int> Indexes { get; }
}