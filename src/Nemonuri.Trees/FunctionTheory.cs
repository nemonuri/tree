
namespace Nemonuri.Trees;

public static class FunctionTheory
{
    public static T Identity<T>(T t) => t;

    public static Func<ITree<T1>, T2> WarpParameterInTree<T1, T2>(Func<T1, T2> source) =>
        tree => source(tree.Value);

    public static bool Tautology<T>(T t) => true;
}