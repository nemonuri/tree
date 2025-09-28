namespace Nemonuri.Graphs.Infrastructure;

public enum AggregatingPhase
{
    Unknown = 0,
    AggregatePrevious = 1,
    AssignPrevious = 2,
    AggregateAndAssignChildren = 3,
    AggregatePost = 4,
    AssignPost = 5
}