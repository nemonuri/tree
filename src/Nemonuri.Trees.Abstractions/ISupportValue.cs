namespace Nemonuri.Trees;

public interface ISupportValue<out TValue>
#if NET9_0_OR_GREATER
    where TValue : allows ref struct
#endif
{ 
    TValue Value { get; }
}