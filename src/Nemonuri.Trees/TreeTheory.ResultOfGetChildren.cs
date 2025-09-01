#if REF_TREE

using System.Collections;

namespace Nemonuri.Trees;

public static partial class TreeTheory
{
    internal class ResultOfGetChildren
    <
        TTree,
        TElement, TAggregation, TAncestor, TAncestorsAggregation
    >
        : IEnumerable<TTree>
        where TTree : ITree<TElement, TAggregation, TAncestor, TAncestorsAggregation>
#if NET9_0_OR_GREATER
                        , allows ref struct
        where TElement : allows ref struct
        where TAggregation : allows ref struct
        where TAncestor : allows ref struct
        where TAncestorsAggregation : allows ref struct
#endif
    {
        private readonly IEnumerable<TElement> _children;
        private readonly ITreeWalker<TElement, TAggregation, TAncestor, TAncestorsAggregation> _treeWalker;
        private readonly Func<TElement, ITreeWalker<TElement, TAggregation, TAncestor, TAncestorsAggregation>, TTree> _treeFactory;

        public ResultOfGetChildren
        (
            IEnumerable<TElement> children,
            ITreeWalker<TElement, TAggregation, TAncestor, TAncestorsAggregation> treeWalker,
            Func<TElement, ITreeWalker<TElement, TAggregation, TAncestor, TAncestorsAggregation>, TTree> treeFactory
        )
        {
            _children = children;
            _treeWalker = treeWalker;
            _treeFactory = treeFactory;
        }

        public IEnumerator<TTree> GetEnumerator()
        {
            return new Enumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator() => throw new NotSupportedException();

        internal class Enumerator : IEnumerator<TTree>
        {
            private readonly ResultOfGetChildren<TTree, TElement, TAggregation, TAncestor, TAncestorsAggregation> _innerSource;
            private readonly IEnumerator<TElement> _enumerator;

            public Enumerator(ResultOfGetChildren<TTree, TElement, TAggregation, TAncestor, TAncestorsAggregation> innerSource)
            {
                _innerSource = innerSource;
                _enumerator = innerSource._children.GetEnumerator();
            }

            public TTree Current => _innerSource._treeFactory(_enumerator.Current, _innerSource._treeWalker);

            public bool MoveNext() => _enumerator.MoveNext();

            public void Reset() => _enumerator.Reset();

            object IEnumerator.Current => throw new NotSupportedException();

            public void Dispose() => _enumerator.Dispose();
        }
    }
}

#endif