namespace Nemonuri.Graphs.Infrastructure;

public readonly ref struct MutableContextRecord<TMutableGraphContext, TMutableSiblingContext>
{
    private readonly ref TMutableGraphContext _mutableGraphContext;
    private readonly ref TMutableSiblingContext _mutableSiblingContext;

    public MutableContextRecord(ref TMutableGraphContext mutableGraphContext, ref TMutableSiblingContext mutableSiblingContext)
    {
        _mutableGraphContext = ref mutableGraphContext;
        _mutableSiblingContext = ref mutableSiblingContext;
    }

    public ref TMutableGraphContext MutableGraphContext => ref _mutableGraphContext;
    public ref TMutableSiblingContext MutableSiblingContext => ref _mutableSiblingContext;

    public void DeconstructToRef(ref TMutableGraphContext mutableGraphContext, ref TMutableSiblingContext mutableSiblingContext)
    {
        mutableGraphContext = ref _mutableGraphContext;
        mutableSiblingContext = ref _mutableSiblingContext;
    }
}
