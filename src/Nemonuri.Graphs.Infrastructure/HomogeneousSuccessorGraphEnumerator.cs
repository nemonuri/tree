using System.Collections;
using System.Diagnostics;

namespace Nemonuri.Graphs.Infrastructure;

public class HomogeneousSuccessorGraphEnumerator<TGraph, TNode, TOutArrow, TOutArrowSet> :
    IEnumerator<PositionedNode<TNode>>
    where TGraph : ISuccessorGraph<TNode, TNode, TOutArrow, TOutArrowSet>
    where TOutArrow : IArrow<TNode, TNode>
    where TOutArrowSet : IOutArrowSet<TOutArrow, TNode, TNode>
{
    private readonly TGraph _graph;
    private readonly HashSet<TNode> _nodeSet;
    private readonly Stack<StackItem> _enumeratorStack;
    private bool _started;
    private PositionedNode<TNode> _current;
    private readonly TNode _initialNode;

    public HomogeneousSuccessorGraphEnumerator(TGraph graph, TNode initialNode)
    {
        _graph = graph;
        _nodeSet = new();
        _enumeratorStack = new();
        _started = false;
        _initialNode = initialNode;
    }

    public PositionedNode<TNode> Current => _current;

    public bool MoveNext()
    {
        if (!_started)
        {
            _started = true;

            UpdateCurrent(_initialNode, 0, 0);
            return true;
        }
        else
        {
        Label2:
            if (_enumeratorStack.TryPeek(out var stackItem))
            {
            Label1:
                if
                (
                    stackItem.Enumerator.MoveNext() &&
                    stackItem.Enumerator.Current.Head is { } headNode
                )
                {
                    stackItem.Index += 1;
                    if (_nodeSet.Contains(headNode))
                    {
                        goto Label1;
                    }
                    UpdateCurrent(headNode, stackItem.Depth, stackItem.Index);
                    return true;
                }
                else
                {
                    _enumeratorStack.Pop();
                    goto Label2;
                }
            }
            else
            {
                _current = default;
                return false;
            }
        }
    }

    private void UpdateCurrent(TNode node, int depth, int index)
    {
        Debug.Assert(!_nodeSet.Contains(node));

        _current = new(node, depth, index);
        _nodeSet.Add(node);
        _enumeratorStack.Push(new(_graph.GetDirectSuccessorArrows(node).GetEnumerator(), depth+1, -1));
    }

    public void Reset()
    {
        _nodeSet.Clear();
        _enumeratorStack.Clear();
        _started = false;
        _current = default;
    }

    object? IEnumerator.Current => Current;

    public void Dispose() { }

    private class StackItem
    {
        public StackItem(IEnumerator<TOutArrow> enumerator, int depth, int index)
        {
            Enumerator = enumerator;
            Depth = depth;
            Index = index;
        }

        public IEnumerator<TOutArrow> Enumerator { get; }

        public int Depth { get; set; }
        public int Index { get; set; }
    }
}

public readonly record struct PositionedNode<TNode>(TNode? Node, int Depth, int Index);
