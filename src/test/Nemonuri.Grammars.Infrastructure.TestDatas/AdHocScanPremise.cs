namespace Nemonuri.Grammars.Infrastructure.TestDatas;

public class AdHocScanPremise<T, TExtra> : IScanPremise<T, TExtra>
{
    private readonly Scanner<T, TExtra> _scanImpl;

    public AdHocScanPremise(Scanner<T, TExtra> scanImpl)
    {
        _scanImpl = scanImpl;
    }

    public ScanResult<int, TExtra> Scan(NodeArrowId arrow, SequenceLattice<T> ideal) => _scanImpl(arrow, ideal);
}
