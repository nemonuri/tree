using SumSharp;

namespace Nemonuri.Trees.Parsers;

[UnionCase("Char", nameof(TChar), UnionCaseStorage.Inline)]
[UnionCase("String", nameof(TString), UnionCaseStorage.Inline)]
public partial struct CharOrString<TChar, TString> where TString : IString<TChar>
{ }