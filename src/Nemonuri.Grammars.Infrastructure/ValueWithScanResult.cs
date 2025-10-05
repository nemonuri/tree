namespace Nemonuri.Grammars.Infrastructure;

public struct ValueWithScanResult<T>
{
    public T WrappedValue;
    public ScanResult ScanResult;

    public ValueWithScanResult(T wrappedValue, ScanResult scanResult)
    {
        WrappedValue = wrappedValue;
        ScanResult = scanResult;
    }
}
