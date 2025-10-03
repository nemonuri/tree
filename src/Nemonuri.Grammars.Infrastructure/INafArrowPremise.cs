
namespace Nemonuri.Grammars.Infrastructure;

public interface INafArrowPremise
<
    TNode, TOutArrow,
    TBound, TIdeal
>
    where TIdeal : IIdeal<TBound>
    where TOutArrow : IArrow<TNode, TNode>
{
    bool CanEnter(TOutArrow arrow, TIdeal ideal);
}