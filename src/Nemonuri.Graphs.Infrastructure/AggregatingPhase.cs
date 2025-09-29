using A = Nemonuri.Graphs.Infrastructure.AggregatingPhaseTheory;

namespace Nemonuri.Graphs.Infrastructure;


public enum InnerPhaseLabel : int
{
    Unknown = 0,
    InnerPrevious = A.Inner | A.Previous,
    InnerMoment = A.Inner | A.Moment,
    InnerPost = A.Inner | A.Post
}

public enum OuterPhaseLabel : int
{
    Unknown = 0,
    InitialOuterPrevious = A.Previous,
    RecursiveOuterPrevious = A.Recursive | A.Previous,
    RecursiveOuterPost = A.Recursive | A.Post,
    InitialOuterPost = A.Post
}
