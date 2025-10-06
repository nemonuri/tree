
namespace Nemonuri.Grammars.Infrastructure;

public readonly record struct ScanResult<TBound, TExtraScanResult>
(
    ScanResultStatus Status,
    TBound? UpperBound,
    TExtraScanResult? Extra
)
{
    [MemberNotNullWhen(true, [nameof(UpperBound), nameof(Extra)])]
    public bool IsSuccess => Status == ScanResultStatus.ScanSuccess;
}
    