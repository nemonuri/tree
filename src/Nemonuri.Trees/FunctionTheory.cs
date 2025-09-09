
namespace Nemonuri.Trees;

public static class FunctionTheory
{
    public static T Identity<T>(T t) => t;

    public static Func<TNode, T2> WarpParameterInTree<TNode, T1, T2>(Func<T1, T2> source)
        where TNode : ISupportValue<T1>
        =>
        tree => source(tree.Value);

    public static bool Tautology<T>(T t) => true;
}