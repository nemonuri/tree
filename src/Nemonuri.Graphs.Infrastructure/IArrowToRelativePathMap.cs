namespace Nemonuri.Graphs.Infrastructure;

public interface IArrowToRelativePathMap<TTail, THead, TArrow, TRelativePath>
    where TArrow : IArrow<TTail, THead>
{
    TRelativePath GetRelativePath(TArrow arrow);
}
