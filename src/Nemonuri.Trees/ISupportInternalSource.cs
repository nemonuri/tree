

namespace Nemonuri.Trees;

public interface ISupportInternalSource<out T>
{
    T? GetInternalSource();
}