namespace Nemonuri.Grammars.Infrastructure;

public interface IIdealContext<TBound, TIdeal, TNode> : IMemoizer<TNode, TIdeal>
    where TIdeal : IIdeal<TBound>
{
    TIdeal CurrentIdeal { get; set; }
}
