namespace Nemonuri.Grammars.Infrastructure;

public interface IIdealContext<TNode, TBound, TIdeal> : IMemoizer<TNode, TBound>
    where TIdeal : IIdeal<TBound>
{
    TIdeal CurrentIdeal { get; set; }
}
