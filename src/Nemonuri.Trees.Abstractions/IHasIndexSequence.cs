namespace Nemonuri.Trees;

public interface IHasIndexSequence
{ 
    IEnumerable<int> IndexSequence { get; }
}