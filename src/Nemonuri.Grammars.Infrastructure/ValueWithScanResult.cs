namespace Nemonuri.Grammars.Infrastructure;

public struct ValueWithScanResult<T, TBound, TIdeal, TExtraScanResult>
    where TIdeal : IIdeal<TBound>
{
    public T WrappedValue;
    public ScanResult<TBound, TExtraScanResult> ScanResult;

    public ValueWithScanResult(T wrappedValue, ScanResult<TBound, TExtraScanResult> scanResult)
    {
        WrappedValue = wrappedValue;
        ScanResult = scanResult;
    }
}
