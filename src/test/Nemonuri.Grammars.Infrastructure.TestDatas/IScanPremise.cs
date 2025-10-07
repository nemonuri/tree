namespace Nemonuri.Grammars.Infrastructure.TestDatas;

public interface IScanPremise<T, TExtra>
{
    ScanResult<int, TExtra> Scan(NodeArrowId arrow, SequenceIdeal<T> ideal);
}

