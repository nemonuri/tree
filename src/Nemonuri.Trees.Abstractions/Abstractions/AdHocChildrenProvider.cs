
namespace Nemonuri.Trees.Abstractions;

/// <summary>
/// An ad-hoc implementation of <see cref="IChildrenProvider{_}"/>
/// </summary>
/// <inheritdoc cref="IChildrenProvider{_}" path="/typeparam" />
public class AdHocChildrenProvider<TNode> : IChildrenProvider<TNode>
#if NET9_0_OR_GREATER
    where TNode : allows ref struct
#endif
{
    private readonly Func<TNode, IEnumerable<TNode>> _getChildrenImplementation;

    /// <summary>
    /// Initializes a new instance of the <see cref="AdHocChildrenProvider{_}"/> structure with the specified implementations.
    /// </summary>
    /// <param name="getChildrenImplementation">
    /// The implementation of <see cref="IChildrenProvider{_}.GetChildren(_)"/>
    /// </param>
    public AdHocChildrenProvider(Func<TNode, IEnumerable<TNode>> getChildrenImplementation)
    {
        _getChildrenImplementation = getChildrenImplementation;
    }

    /// <inheritdoc cref="IChildrenProvider{_}.GetChildren(_)" />
    public IEnumerable<TNode> GetChildren(TNode source) =>
        _getChildrenImplementation(source);
}