namespace Nemonuri.Graphs.Infrastructure;

public readonly ref struct MutableInnerContextRecord<TMutableGraphContext, TMutableSiblingContext, TMutableDepthContext, TMutableInnerSiblingContext>
{
    private readonly MutableOuterContextRecord<TMutableGraphContext, TMutableSiblingContext, TMutableDepthContext> _mutableOuterContext;
    private readonly ref TMutableInnerSiblingContext _mutableInnerSiblingContext;

    public MutableInnerContextRecord
    (
        MutableOuterContextRecord<TMutableGraphContext, TMutableSiblingContext, TMutableDepthContext> mutableOuterContext,
        ref TMutableInnerSiblingContext mutableInnerSiblingContext
    )
    {
        _mutableOuterContext = mutableOuterContext;
        _mutableInnerSiblingContext = ref mutableInnerSiblingContext;
    }

    public MutableOuterContextRecord<TMutableGraphContext, TMutableSiblingContext, TMutableDepthContext> MutableOuterContext => _mutableOuterContext;

    public ref TMutableGraphContext MutableGraphContext => ref _mutableOuterContext.MutableGraphContext;
    public ref TMutableSiblingContext MutableSiblingContext => ref _mutableOuterContext.MutableSiblingContext;
    public ref TMutableDepthContext MutableDepthContext => ref _mutableOuterContext.MutableDepthContext;
    public ref TMutableInnerSiblingContext MutableInnerSiblingContext => ref _mutableInnerSiblingContext;


    public void Deconstruct
    (
        out TMutableGraphContext mutableGraphContext, out TMutableSiblingContext mutableSiblingContext, out TMutableDepthContext mutableDepthContext,
        out TMutableInnerSiblingContext mutableInnerSiblingContext
    )
    {
        _mutableOuterContext.Deconstruct(out mutableGraphContext, out mutableSiblingContext, out mutableDepthContext);
        mutableInnerSiblingContext = _mutableInnerSiblingContext;
    }
}
