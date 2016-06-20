using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace ElementsOfProgrammerInterviewsCSharp
{
    public class Chapter09
    {
        #region 01 Design a stack with max api

        public class StackWithMax
        {
            private readonly LinkedList<ElementWithCachedMax> _stack = new LinkedList<ElementWithCachedMax>();

            public void Push(int val)
            {
                _stack.AddFirst(new ElementWithCachedMax()
                {
                    Value = val,
                    CachedMax = _stack.First != null && _stack.First.Value.CachedMax > val
                        ? _stack.First.Value.CachedMax
                        : val
                });
            }

            public int Pop()
            {
                var first = _stack.First;
                _stack.RemoveFirst(); // throws an exception if there isn't a last
                return first.Value.Value;
            }

            public int Max()
            {
                if (_stack.First == null)
                {
                    throw new InvalidOperationException();
                }
                return _stack.First.Value.CachedMax;
            }

            private class ElementWithCachedMax
            {
                public int Value;
                public int CachedMax;
            }
        }

        #endregion

        #region 03 Test a string for wellformedness

        public static bool IsWellFormed(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return false;
            }

            Dictionary<char, char> matchingBraces = new Dictionary<char, char>()
            {
                {'}', '{'},
                {']', '['},
                {')', '('},
            };

            Stack<char> stack = new Stack<char>();
            foreach (var c in s.ToCharArray())
            {
                if (matchingBraces.ContainsKey(c))
                {
                    if (stack.Count == 0 || stack.Peek() != matchingBraces[c])
                    {
                        return false;
                    }

                    stack.Pop();
                }
                else
                {
                    stack.Push(c);
                }
            }

            return stack.Count == 0;
        }

        #endregion

        #region 12 Implement a queue with stacks

        public class QueueWithStacks
        {
            private Stack<int> _enqueue = new Stack<int>(100);
            private Stack<int> _dequeue = new Stack<int>(100);

            public void Enqueue(int value)
            {
                _enqueue.Push(value);
            }

            public int Dequeue()
            {
                if (_dequeue.Count == 0)
                {
                    while (_enqueue.Count > 0)
                    {
                        _dequeue.Push(_enqueue.Pop());
                    }
                }
            }
        }

        #endregion
    }

    [TestFixture]
    public class Chapter09TestFixture
    {
        #region 03 Test a string for wellformedness

        [Test]
        public void When_WellFormed_Then_TrueReturned()
        {
            Assert.IsTrue(Chapter09.IsWellFormed("{}()[]"));
            Assert.IsTrue(Chapter09.IsWellFormed("{[]}()[]"));
        }

        [Test]
        public void When_NotWellFormed_ThenFalseReturned()
        {
            Assert.IsFalse(Chapter09.IsWellFormed(null));
            Assert.IsFalse(Chapter09.IsWellFormed(string.Empty));
            Assert.IsFalse(Chapter09.IsWellFormed("{}()["));
            Assert.IsFalse(Chapter09.IsWellFormed("{}()[]]"));
        }

        #endregion
    }
}