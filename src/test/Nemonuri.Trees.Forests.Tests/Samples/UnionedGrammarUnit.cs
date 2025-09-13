using SumSharp;

namespace Nemonuri.Trees.Forests.Tests.Samples;

[UnionCase("StringGrammar", typeof(IStringGrammarUnit), UnionCaseStorage.AsObject)]
[UnionCase("SyntaxTreeListGrammar", typeof(ISyntaxTreeListGrammarUnit), UnionCaseStorage.AsObject)]
public partial struct UnionedGrammarUnit : IGrammarUnit
{
    public GrammarLabel GetGrammarLabel()
    {
        return Match<GrammarLabel>
        (
            static (IStringGrammarUnit v) => v.GetGrammarLabel(),
            static (ISyntaxTreeListGrammarUnit v) => v.GetGrammarLabel(),
            static () => GrammarLabel.None
        );
    }
}