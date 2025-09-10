namespace Nemonuri.Trees;

public interface ISupportSlice<out T> where T : ISupportSlice<T>
{ 
    T Slice(int start, int length);
}