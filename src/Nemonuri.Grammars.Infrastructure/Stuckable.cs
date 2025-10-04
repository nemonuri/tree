using SumSharp;

namespace Nemonuri.Grammars.Infrastructure;

[UnionCase("Stuck")]
[UnionCase("Pass", "T", UnionCaseStorage.Inline)]
public partial struct Stuckable<T>
{ }