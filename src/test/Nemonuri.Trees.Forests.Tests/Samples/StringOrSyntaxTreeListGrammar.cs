using SumSharp;

namespace Nemonuri.Trees.Forests.Tests.Samples;

[UnionCase("StringGrammar", typeof(IStringGrammar), UnionCaseStorage.AsObject)]
[UnionCase("SyntaxTreeListGrammar", typeof(ISyntaxTreeListGrammar), UnionCaseStorage.AsObject)]
public readonly partial struct StringOrSyntaxTreeListGrammar
{ }