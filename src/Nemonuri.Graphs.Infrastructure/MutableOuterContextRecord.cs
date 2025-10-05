namespace Nemonuri.Graphs.Infrastructure;

public readonly ref struct MutableOuterContextRecord<TMutableGraphContext, TMutableSiblingContext, TMutableDepthContext>
{
    private readonly ref TMutableGraphContext _mutableGraphContext;
    private readonly ref TMutableSiblingContext _mutableSiblingContext;
    private readonly ref TMutableDepthContext _mutableDepthContext;

    public MutableOuterContextRecord(ref TMutableGraphContext mutableGraphContext, ref TMutableSiblingContext mutableSiblingContext, ref TMutableDepthContext mutableDepthContext)
    {
        _mutableGraphContext = ref mutableGraphContext;
        _mutableSiblingContext = ref mutableSiblingContext;
        _mutableDepthContext = ref mutableDepthContext;
    }

    public ref TMutableGraphContext MutableGraphContext => ref _mutableGraphContext;
    public ref TMutableSiblingContext MutableSiblingContext => ref _mutableSiblingContext;
    public ref TMutableDepthContext MutableDepthContext => ref _mutableDepthContext;

    public void Deconstruct(out TMutableGraphContext mutableGraphContext, out TMutableSiblingContext mutableSiblingContext, out TMutableDepthContext mutableDepthContext)
    {
        mutableGraphContext = _mutableGraphContext;
        mutableSiblingContext = _mutableSiblingContext;
        mutableDepthContext = _mutableDepthContext;
    }
}
