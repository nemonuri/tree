
namespace Nemonuri.Grammars.Infrastructure;

public interface INafArrowPremise
<
    TNode, TOutArrow,
    TBound, TIdeal, TExtraScanResult
>
    where TIdeal : IIdeal<TBound>
    where TOutArrow : IArrow<TNode, TNode>
{
    ScanResult<TBound, TIdeal, TExtraScanResult> Scan(TOutArrow arrow, TIdeal ideal);
}
