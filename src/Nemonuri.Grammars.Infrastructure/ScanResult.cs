
namespace Nemonuri.Grammars.Infrastructure;

public readonly record struct ScanResult<TBound, TIdeal, TExtraScanResult>
(
    ScanResultStatus Status,
    TBound? UpperBound,
    TExtraScanResult? Extra
)
    where TIdeal : IIdeal<TBound>
{
    [MemberNotNullWhen(true, [nameof(UpperBound), nameof(Extra)])]
    public bool IsSuccess => Status == ScanResultStatus.ScanSuccess;
}
    