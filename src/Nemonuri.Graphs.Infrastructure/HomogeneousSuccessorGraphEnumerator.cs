using System.Collections;
using System.Diagnostics;

namespace Nemonuri.Graphs.Infrastructure;

public class HomogeneousSuccessorGraphEnumerator<TGraph, TNode, TOutArrow, TOutArrowSet> :
    IEnumerator<TNode>
    where TGraph : ISuccessorGraph<TNode, TNode, TOutArrow, TOutArrowSet>
    where TOutArrow : IArrow<TNode, TNode>
    where TOutArrowSet : IOutArrowSet<TOutArrow, TNode, TNode>
{
    private readonly TGraph _graph;
    private readonly HashSet<TNode> _nodeSet;
    private readonly Stack<IEnumerator<TOutArrow>> _enumeratorStack;
    private bool _started;
    private TNode? _current;
    private readonly TNode _initialNode;

    public HomogeneousSuccessorGraphEnumerator(TGraph graph, TNode initialNode)
    {
        _graph = graph;
        _nodeSet = new();
        _enumeratorStack = new();
        _started = false;
        _initialNode = initialNode;
    }

    public TNode Current => _current!;

    public bool MoveNext()
    {
        if (!_started)
        {
            _started = true;

            UpdateCurrent(_initialNode);
            return true;
        }
        else
        {
        Label2:
            if (_enumeratorStack.TryPeek(out var et))
            {
            Label1:
                if
                (
                    et.MoveNext() &&
                    et.Current.Head is { } headNode
                )
                {
                    if (_nodeSet.Contains(headNode))
                    {
                        goto Label1;
                    }
                    UpdateCurrent(headNode);
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

    private void UpdateCurrent(TNode node)
    {
        Debug.Assert(!_nodeSet.Contains(node));

        _current = node;
        _nodeSet.Add(node);
        _enumeratorStack.Push(_graph.GetDirectSuccessorArrows(node).GetEnumerator());
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
}
