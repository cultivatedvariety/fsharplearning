using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace ElementsOfProgrammerInterviewsCSharp
{
    public class Chapter08
    {
        #region 01 Merge Sorted Linked List

        public static MyLinkedList _01_MergeSortedLists(MyLinkedList l, MyLinkedList f)
        {
            if (l?.Head == null)
            {
                return f;
            }
            if (f?.Head == null)
            {
                return l;
            }

            MyLinkedList mergedList;
            MyNode tempHead;
            MyNode currentTail;
            if (l.Head.Data <= f.Head.Data)
            {
                mergedList = l;
                tempHead = f.Head;
                currentTail = l.Head;
            }
            else
            {
                mergedList = f;
                tempHead = l.Head;
                currentTail = f.Head;
            }
            
            while (tempHead != null)
            {
                if (currentTail.Next == null)
                {
                    currentTail.Next = tempHead;
                    break;
                }

                if (tempHead.Data <= currentTail.Next.Data)
                {
                    MyNode temp = currentTail.Next;
                    currentTail.Next = tempHead;
                    tempHead = temp;
                }
                currentTail = currentTail.Next;
            }

            return mergedList;
            
        }
        #endregion

        #region 02 Reverse single linked list

        public static void _02_ReverseSinglyLinkedList(MyLinkedList list)
        {
            if (list?.Head?.Next == null)
            {
                return;
            }
            MyNode previous = null;
            MyNode current = list.Head;

            while (current != null)
            {
                var next = current.Next;
                current.Next = previous;
                previous = current;
                current = next;
            }
            list.Head = previous;
        }

        #endregion

        #region 09 Remove kth last element from list

        public static void _09_RemoveKthLastElement(MyLinkedList list, int k)
        {
            if (list?.Head == null)
            {
                return;
            }
            int count = 0;
            MyNode current = list.Head;
            MyNode kthPreviousElement = null;
            while (current != null && count <= k)
            {
                current = current.Next;
                count++;
            }

            if (count < k)
            {
                return;
            }

            kthPreviousElement = list.Head;
            while (current != null)
            {
                current = current.Next;
                kthPreviousElement = kthPreviousElement.Next;
            }

            if (current == null)
            {
                //removing the head
                MyNode head = list.Head;
                list.Head = list.Head.Next;
                head.Next = null;
            }
            else
            {
                MyNode kthElement = kthPreviousElement.Next;
                kthPreviousElement.Next = kthElement.Next;
                kthElement.Next = null;
            }
        }

        #endregion

        #region simple linked list
        public class MyLinkedList
        {
            public MyNode Head;

            public int Count
            {
                get
                {
                    int count = 0;
                    MyNode node = Head;
                    while (node != null)
                    {
                        count++;
                        node = node.Next;
                    }
                    return count;
                }
            }
        }

        public class MyNode
        {
            public int Data;
            public MyNode Next;
        }
    #endregion
    }

    [TestFixture]
    public class Chapter08TestFixture
    {
        #region 01 Merge Sorted Linked List

        [Test]
        public void _01_When_FirstListLongerThanSecond_Then_SingleMergedSortedListReturned()
        {
            var l = CreateLinkedList(2, 5, 7);
            var f = CreateLinkedList(3,11);

            var merged = Chapter08._01_MergeSortedLists(l, f);

            Assert.AreEqual(5, merged.Count);
            int[] expected = new[] {2, 3, 5, 7, 11};
            Chapter08.MyNode actual = merged.Head;
            foreach (var i in expected)
            {
                Assert.AreEqual(i, actual.Data);
                actual = actual.Next;
            }
        }

        [Test]
        public void _01_When_SecondListLongerThanFirst_Then_SingleMergedSortedListReturned()
        {
            var l = CreateLinkedList(3, 11);
            var f = CreateLinkedList(2, 5, 7);

            var merged = Chapter08._01_MergeSortedLists(l, f);

            Assert.AreEqual(5, merged.Count);
            int[] expected = new[] { 2, 3, 5, 7, 11 };
            Chapter08.MyNode actual = merged.Head;
            foreach (var i in expected)
            {
                Assert.AreEqual(i, actual.Data);
                actual = actual.Next;
            }
        }

        [Test]
        public void _01_When_ListIsNull_ThenMerged()
        {
            var l = CreateLinkedList(3, 11);

            var merged = Chapter08._01_MergeSortedLists(l, null);
            Assert.AreEqual(l, merged);

            merged = Chapter08._01_MergeSortedLists(null, l);
            Assert.AreEqual(l, merged);
        }

        [Test]
        public void _01_WhenRandom_Then_ListsAreMerged()
        {
            Chapter08.MyLinkedList l;
            Chapter08.MyLinkedList f;
            Random random = new Random();
            List<int> list1 = new List<int>();
            for (int i = 0; i < 10000; i++)
            {
                list1.Add(random.Next(0, 50000));
            }
            l = CreateLinkedList(list1.OrderBy(i => i).ToArray());

            List<int> list2 = new List<int>();
            for (int i = 0; i < 10000; i++)
            {
                list2.Add(random.Next(0, 50000));
            }
            f = CreateLinkedList(list2.OrderBy(i => i).ToArray());

            var mergeSortedLists = Chapter08._01_MergeSortedLists(l, f);
            Assert.AreEqual(list1.Count + list2.Count, mergeSortedLists.Count);


            list1.AddRange(list2);
            list1.Sort();
            Chapter08.MyNode current = mergeSortedLists.Head;
            for (int i = 0; i < list1.Count; i++)
            {
                Assert.AreEqual(list1[i], current.Data);
                current = current.Next;
            }
        }

        #endregion

        #region 02 Reverse single linked list

        [Test]
        public void When_Reversed_Then_InDescendingOrder()
        {
            var values = Enumerable.Range(15, 300).ToArray();
            var linkedList = CreateLinkedList(values);
            Chapter08._02_ReverseSinglyLinkedList(linkedList);

            Assert.AreEqual(values.Length, linkedList.Count);
            Chapter08.MyNode current = linkedList.Head;
            foreach (var val in values.OrderByDescending(i => i))
            {
                Assert.AreEqual(val, current.Data);
                current = current.Next;
            }
        }

        #endregion

        #region Remove kth last element from list

        [Test]
        public void When_KthElementRemoved_Then_ListStillValid()
        {
            List<int> values = new List<int>(){1, 2, 3, 4, 5, 6, 7, 8};
            var linkedList = CreateLinkedList(values.ToArray());

            int k = 4;
            Chapter08._09_RemoveKthLastElement(linkedList, k);
            values.RemoveAt(values.Count - k);

            Assert.AreEqual(values.Count, linkedList.Count);
            Chapter08.MyNode current = linkedList.Head;
            foreach (var value in values)
            {
                Assert.AreEqual(value, current.Data);
                current = current.Next;
            }
        }

        [Test]
        public void When_HeadRemoved_Then_ListStillValid()
        {
            List<int> values = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8 };
            var linkedList = CreateLinkedList(values.ToArray());

            int k = values.Count - 1;
            Chapter08._09_RemoveKthLastElement(linkedList, k);
            values.RemoveAt(0);

            Assert.AreEqual(values.Count, linkedList.Count);
            Chapter08.MyNode current = linkedList.Head;
            foreach (var value in values)
            {
                Assert.AreEqual(value, current.Data);
                current = current.Next;
            }
        }

        [Test]
        public void When_KthElementDoesNotExist_Then_ListStillValid()
        {
            List<int> values = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8 };
            var linkedList = CreateLinkedList(values.ToArray());

            int k = 28;
            Chapter08._09_RemoveKthLastElement(linkedList, k);

            Assert.AreEqual(values.Count, linkedList.Count);
            Chapter08.MyNode current = linkedList.Head;
            foreach (var value in values)
            {
                Assert.AreEqual(value, current.Data);
                current = current.Next;
            }
        }

        #endregion

        private Chapter08.MyLinkedList CreateLinkedList(params int[] arr)
        {
            Chapter08.MyLinkedList linkedList = new Chapter08.MyLinkedList()
            {
                Head = new Chapter08.MyNode() {Data = arr[0]}
            };
            Chapter08.MyNode previous = linkedList.Head;
            for (int i = 1; i < arr.Length; i++)
            {
                Chapter08.MyNode node = new Chapter08.MyNode() {Data = arr[i]};
                previous.Next = node;
                previous = node;
            }
            return linkedList;
        }
    }
}