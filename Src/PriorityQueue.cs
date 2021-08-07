using System;

namespace RT.Dijkstra
{
    public sealed class PriorityQueue<TItem, TWeight> where TWeight : IComparable<TWeight>
    {
        private TItem[] _elements;
        private TWeight[] _weights;
        private int _count;

        public bool IsEmpty { get { return _count == 0; } }

        public PriorityQueue()
        {
            _elements = new TItem[64];
            _weights = new TWeight[64];
            _count = 0;
        }

        private int compare(int a, int b)
        {
            return _weights[a].CompareTo(_weights[b]);
        }

        private void swap(int a, int b)
        {
            TItem element = _elements[a];
            _elements[a] = _elements[b];
            _elements[b] = element;

            TWeight weight = _weights[a];
            _weights[a] = _weights[b];
            _weights[b] = weight;
        }

        private void reheapifyUp(int index)
        {
            int parent = (index - 1) / 2;
            while (index > 0 && compare(index, parent) < 0)
            {
                swap(index, parent);
                index = parent;
                parent = (index - 1) / 2;
            }
        }

        private void reheapifyDown(int index)
        {
            while (index < _count / 2)
            {
                // special case: only one child
                if (_count % 2 == 0 && index == _count / 2 - 1)
                {
                    if (compare(index, _count - 1) > 0)
                        swap(index, _count - 1);
                    return;
                }
                else
                {
                    int child1 = 2 * index + 1;
                    int child2 = child1 + 1;
                    int childCompare = compare(child1, child2);
                    if (childCompare <= 0 && compare(child1, index) < 0)
                    {
                        swap(child1, index);
                        index = child1;
                    }
                    else if (childCompare >= 0 && compare(child2, index) < 0)
                    {
                        swap(child2, index);
                        index = child2;
                    }
                    else
                        return;
                }
            }
        }

        public void Add(TItem element, TWeight weight)
        {
            if (_count == _elements.Length)
            {
                var newSize = 2 * _elements.Length;
                Array.Resize(ref _elements, newSize);
                Array.Resize(ref _weights, newSize);
            }
            _elements[_count] = element;
            _weights[_count] = weight;
            _count++;
            reheapifyUp(_count - 1);
        }

        public void Extract(out TItem element, out TWeight weight)
        {
            if (_count == 0)
                throw new InvalidOperationException("Cannot extract element from empty priority queue.");

            element = _elements[0];
            weight = _weights[0];
            _count--;
            _elements[0] = _elements[_count];
            _weights[0] = _weights[_count];
            reheapifyDown(0);
        }

        public int Count { get { return _count; } }
    }
}
