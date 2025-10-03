namespace Nemonuri.Graphs.Infrastructure;

public readonly ref struct MutableContextRecord<TMutableGraphContext, TMutableSiblingContext, TMutableDepthContext>
{
    private readonly ref TMutableGraphContext _mutableGraphContext;
    private readonly ref TMutableSiblingContext _mutableSiblingContext;
    private readonly ref TMutableDepthContext _mutableDepthContext;

    public MutableContextRecord(ref TMutableGraphContext mutableGraphContext, ref TMutableSiblingContext mutableSiblingContext, ref TMutableDepthContext mutableDepthContext)
    {
        _mutableGraphContext = ref mutableGraphContext;
        _mutableSiblingContext = ref mutableSiblingContext;
        _mutableDepthContext = ref mutableDepthContext;
    }

    public ref TMutableGraphContext MutableGraphContext => ref _mutableGraphContext;
    public ref TMutableSiblingContext MutableSiblingContext => ref _mutableSiblingContext;
    public ref TMutableDepthContext MutableDepthContext => ref _mutableDepthContext;

    public void DeconstructToRef(ref TMutableGraphContext mutableGraphContext, ref TMutableSiblingContext mutableSiblingContext, ref TMutableDepthContext mutableDepthContext)
    {
        mutableGraphContext = ref _mutableGraphContext;
        mutableSiblingContext = ref _mutableSiblingContext;
        mutableDepthContext = ref _mutableDepthContext;
    }
}
